using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public Vector3 startingPos;
    public DraggableType CurrentType;
    public DraggableUse CurrentUse;

    void Start()
    {
        {
            switch (CurrentUse)
            {
                case DraggableUse.ProjectDesign:
                    startingPos = GetComponent<RectTransform>().localPosition;
                    break;
                case DraggableUse.MiniGame:
                    break;
            }
        }
    }

    public enum DraggableUse
    {
        ProjectDesign, MiniGame
    }

    public enum DraggableType
    {
        Environment, Finance, Social, Rating, Budget, Advertisement, Pointer, 
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        switch (CurrentUse)
        {
            case DraggableUse.ProjectDesign:
                iTween.ScaleTo(gameObject, iTween.Hash("x", 1.5f, "y", 1.5f, "time", .3f));
                break;
            case DraggableUse.MiniGame:
                switch (CurrentType)
                {
                    case DraggableType.Advertisement:
                        GetComponent<Advertisement>().StartDragging();
                        break;
                }
                break;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        switch (CurrentUse)
        {
            case DraggableUse.ProjectDesign:
                transform.position = eventData.position;
                break;
            case DraggableUse.MiniGame:
                Vector3 calcPos = CameraControl.Instance.CurrentCam.ScreenToWorldPoint(eventData.position);
                transform.position = new Vector3(calcPos.x, calcPos.y, 0);
                break;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        switch (CurrentUse)
        {
            case DraggableUse.ProjectDesign:
                break;
            case DraggableUse.MiniGame:
                switch (CurrentType)
                {
                    case DraggableType.Advertisement:
                        GetComponent<Advertisement>().Release();
                        break;
                }
                break;
        }
        StartCoroutine(ResetPos());
    }

    public IEnumerator ResetPos()
    {
        switch (CurrentUse)
        {
            case DraggableUse.ProjectDesign:
                iTween.ScaleTo(gameObject, iTween.Hash("x", 0f, "y", 0f, "time", .5f));
                yield return new WaitForSeconds(.5f);
                GetComponent<RectTransform>().localPosition = startingPos;
                iTween.ScaleTo(gameObject, iTween.Hash("x", 1f, "y", 1f, "time", .3f));
                break;
            case DraggableUse.MiniGame:
                break;
        }
    }
}
