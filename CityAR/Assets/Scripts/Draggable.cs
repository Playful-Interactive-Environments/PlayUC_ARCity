using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public Vector3 startingPos;
    public DraggableType CurrentType;
    void Start()
    {
        startingPos = GetComponent<RectTransform>().localPosition;
    }

    public enum DraggableType
    {
        Environment, Finance, Social, Rating, Budget
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        iTween.ScaleTo(gameObject, iTween.Hash("x", 1.5f, "y", 1.5f, "time", .3f));
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        StartCoroutine(ResetPos());
    }
    public IEnumerator ResetPos()
    {
        iTween.ScaleTo(gameObject, iTween.Hash("x", 0f, "y", 0f, "time", .5f));
        yield return new WaitForSeconds(.5f);
        GetComponent<RectTransform>().localPosition = startingPos;
        iTween.ScaleTo(gameObject, iTween.Hash("x", 1f, "y", 1f, "time", .3f));
    }
}
