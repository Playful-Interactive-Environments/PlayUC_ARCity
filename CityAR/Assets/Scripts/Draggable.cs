using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public Vector3 startingPos;
    public DraggableType CurrentType;
    
    public enum DraggableType
    {
        Environment, Social, Finance, Rating, Budget, Advertisement, Pointer, Area, Word
    }

    void Start()
    {

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector3 calcPos = CameraControl.Instance.CurrentCam.ScreenToWorldPoint(eventData.position);
        switch (CurrentType)
        {
            case DraggableType.Environment:
                iTween.ScaleTo(gameObject, iTween.Hash("x", 1.5f, "y", 1.5f, "time", .3f));
                startingPos = GetComponent<RectTransform>().localPosition;
                break;
            case DraggableType.Social:
                iTween.ScaleTo(gameObject, iTween.Hash("x", 1.5f, "y", 1.5f, "time", .3f));
                startingPos = GetComponent<RectTransform>().localPosition;
                break;
            case DraggableType.Finance:
                iTween.ScaleTo(gameObject, iTween.Hash("x", 1.5f, "y", 1.5f, "time", .3f));
                startingPos = GetComponent<RectTransform>().localPosition;
                break;
            case DraggableType.Rating:
                iTween.ScaleTo(gameObject, iTween.Hash("x", 1.5f, "y", 1.5f, "time", .3f));
                startingPos = GetComponent<RectTransform>().localPosition;
                break;
            case DraggableType.Budget:
                iTween.ScaleTo(gameObject, iTween.Hash("x", 1.5f, "y", 1.5f, "time", .3f));
                startingPos = GetComponent<RectTransform>().localPosition;
                break;
            case DraggableType.Advertisement:
                GetComponent<Advertisement>().StartDragging();
                break;
            case DraggableType.Area:
               MG_3.Instance.BeginDrag(calcPos, gameObject);
                break;
            case DraggableType.Word:
                startingPos = transform.localPosition;
                GetComponent<Word>().Grab();
                break;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 calcPos = CameraControl.Instance.CurrentCam.ScreenToWorldPoint(eventData.position);
        switch (CurrentType)
        {
            case DraggableType.Environment:
                transform.position = eventData.position;
                break;
            case DraggableType.Social:
                transform.position = eventData.position;
                break;
            case DraggableType.Finance:
                transform.position = eventData.position;
                break;
            case DraggableType.Rating:
                transform.position = eventData.position;
                break;
            case DraggableType.Budget:
                transform.position = eventData.position;
                break;
            case DraggableType.Advertisement:
                transform.position = new Vector3(calcPos.x, calcPos.y, 0);
                break;
            case DraggableType.Area:
                MG_3.Instance.Drag(calcPos);
                break;
            case DraggableType.Word:
                transform.position = new Vector3(calcPos.x, calcPos.y, 0);
                break;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        switch (CurrentType)
        {
            case DraggableType.Environment:
                StartCoroutine(ResetPos());
                break;
            case DraggableType.Social:
                StartCoroutine(ResetPos());
                break;
            case DraggableType.Finance:
                StartCoroutine(ResetPos());
                break;
            case DraggableType.Rating:
                StartCoroutine(ResetPos());
                break;
            case DraggableType.Budget:
                StartCoroutine(ResetPos());
                break;
            case DraggableType.Advertisement:
                GetComponent<Advertisement>().Release();
                break;
            case DraggableType.Area:
                MG_3.Instance.EndDrag();
                break;
            case DraggableType.Word:
                GetComponent<Word>().Drop();
                break;
        }
    }

    public IEnumerator ResetPos()
    {
        iTween.ScaleTo(gameObject, iTween.Hash("x", 0f, "y", 0f, "time", .5f));
        yield return new WaitForSeconds(.5f);
        GetComponent<RectTransform>().localPosition = startingPos;
        iTween.ScaleTo(gameObject, iTween.Hash("x", 1f, "y", 1f, "time", .3f));
    }
}
