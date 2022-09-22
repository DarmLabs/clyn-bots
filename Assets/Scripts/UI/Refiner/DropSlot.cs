using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DropSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] FilterManager filterManager;
    [SerializeField] RefinerPanelUI refinerPanelUI;
    public void OnDrop(PointerEventData data)
    {
        Debug.Log("drop");
        if (data.pointerDrag != null)
        {
            DragDrop currentDragDrop = data.pointerDrag.gameObject.GetComponent<DragDrop>();
            if (!currentDragDrop.isLocked)
            {
                Debug.Log("entro");
                currentDragDrop.Reset();
                refinerPanelUI.RefineButtonsSwitcher(true);
                filterManager.SaveFilterValues(currentDragDrop.gameObject.name, currentDragDrop.movingValue / 10);
            }
        }
    }
}
