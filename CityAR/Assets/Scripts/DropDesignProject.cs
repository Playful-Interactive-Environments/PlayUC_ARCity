using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropDesignProject : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

    public ProjectDesign DesignScript;
    public DragZoneType CurrentType;
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
        Debug.Log(eventData.pointerDrag.name + " was dropped on " + gameObject.name);
        DragDesignProject d = eventData.pointerDrag.GetComponent<DragDesignProject>();
        if(CurrentType == DragZoneType.AddValue)
            DesignScript.AddValue(d.CurrentType);
        if (CurrentType == DragZoneType.SubtractValue)
            DesignScript.SubtractValue(d.CurrentType);
        d.StartCoroutine("ResetPos");
    }
}
