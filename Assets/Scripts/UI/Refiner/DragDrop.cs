using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] GameObject OnDragContainer;
    [SerializeField] Canvas canvas;
    CanvasGroup canvasGroup;
    RectTransform rectTransform;
    Transform originalParent;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalParent = transform.parent;
    }
    public void OnPointerDown(PointerEventData data)
    {

    }
    public void OnDrag(PointerEventData data)
    {
        gameObject.transform.SetParent(OnDragContainer.transform);
        rectTransform.anchoredPosition += data.delta / canvas.scaleFactor;
    }
    public void OnBeginDrag(PointerEventData data)
    {
        canvasGroup.blocksRaycasts = false;
    }
    public void OnEndDrag(PointerEventData data)
    {
        canvasGroup.blocksRaycasts = true;
        if (gameObject.transform.parent == OnDragContainer.transform)
        {
            gameObject.transform.SetParent(originalParent);
            rectTransform.anchoredPosition = new Vector2(0, 0);
        }
    }
}
