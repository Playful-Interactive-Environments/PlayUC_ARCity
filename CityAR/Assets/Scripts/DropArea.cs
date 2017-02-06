using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

    public ProjectDesign DesignScript;
    public DragZoneType CurrentType;

    public enum DragZoneType
    {
        AddValue, SubtractValue, Finance, Social, Environment
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
        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

        switch (CurrentType)
        {
            case DragZoneType.AddValue:
                    DesignScript.AddValue(d.CurrentType);
                break;
            case DragZoneType.SubtractValue:

                    DesignScript.SubtractValue(d.CurrentType);
                    d.StartCoroutine("ResetPos");
                break;
        }
    }
}
