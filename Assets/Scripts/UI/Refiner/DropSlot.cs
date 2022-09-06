using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DropSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] GameObject[] points;
    [SerializeField] GameObject panelText;
    public void OnDrop(PointerEventData data)
    {
        GameObject desiredPoint = new GameObject();
        bool pointTaken = false;
        foreach (var point in points)
        {
            if (point.transform.childCount == 0 && !pointTaken)
            {
                pointTaken = true;
                desiredPoint = point;
            }
        }
        if (data.pointerDrag != null)
        {
            data.pointerDrag.transform.SetParent(desiredPoint.transform);
            data.pointerDrag.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            panelText.SetActive(false);
        }
    }
}
