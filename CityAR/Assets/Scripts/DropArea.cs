using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

    public ProjectDesign DesignScript;
    public DragZoneType CurrentType;
    public DragZoneUse CurrentUse;

    public enum DragZoneUse
    {
        ProjectDesign, MiniGame
    }
    public enum DragZoneType
    {
        AddValue, SubtractValue
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    void Update()
    {
    }

    public void OnDrop(PointerEventData eventData)
    {
        switch (CurrentUse)
        {
            case DragZoneUse.ProjectDesign:
                //Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);
                Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
                if (CurrentType == DragZoneType.AddValue)
                    DesignScript.AddValue(d.CurrentType);
                if (CurrentType == DragZoneType.SubtractValue)
                    DesignScript.SubtractValue(d.CurrentType);
                d.StartCoroutine("ResetPos");
                break;
            case DragZoneUse.MiniGame:

                break;
        }

    }
}
