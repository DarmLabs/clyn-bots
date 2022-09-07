using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DropSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] FilterManager filterManager;
    public void OnDrop(PointerEventData data)
    {
        if (data.pointerDrag != null)
        {
            DragDrop currentDragDrop = data.pointerDrag.gameObject.GetComponent<DragDrop>();
            if (!currentDragDrop.isLocked)
            {
                currentDragDrop.Reset();
                filterManager.SaveFilterValues(currentDragDrop.gameObject.name, currentDragDrop.movingValue / 10);
            }
        }
    }
}
