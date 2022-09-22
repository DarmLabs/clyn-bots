using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] GameObject OnDragContainer;
    [SerializeField] Canvas canvas;
    [SerializeField] FilterManager filterManager;
    [SerializeField] RefinerPanelUI refinerPanelUI;
    [SerializeField] GameObject pointerText;
    CanvasGroup canvasGroup;
    RectTransform rectTransform;
    Transform originalParent;
    [HideInInspector] public int movingValue;
    [HideInInspector] public bool isLocked;
    int correspondetValue;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalParent = transform.parent;
    }
    public void OnBeginDrag(PointerEventData data)
    {
        TakeSelectedValue();
        if (!isLocked)
        {
            switch (gameObject.name)
            {
                case "Plastico":
                    correspondetValue = refinerPanelUI.plasticoDiv;
                    break;
                case "Vidrio":
                    correspondetValue = refinerPanelUI.vidrioDiv;
                    break;
                case "Carton":
                    correspondetValue = refinerPanelUI.cartonDiv;
                    break;
                case "Metal":
                    correspondetValue = refinerPanelUI.metalDiv;
                    break;
            }
            if (correspondetValue < movingValue)
            {
                isLocked = true;
                refinerPanelUI.Locked();
                return;
            }
            else
            {
                canvasGroup.blocksRaycasts = false;
                pointerText.SetActive(true);
                pointerText.GetComponent<PointerText>().FillText(movingValue.ToString());
            }
        }
    }
    public void OnDrag(PointerEventData data)
    {
        if (!isLocked)
        {
            gameObject.transform.SetParent(OnDragContainer.transform);
            rectTransform.anchoredPosition += data.delta / canvas.scaleFactor;
        }
    }
    public void OnEndDrag(PointerEventData data)
    {
        if (!isLocked)
        {
            canvasGroup.blocksRaycasts = true;
            if (gameObject.transform.parent == OnDragContainer.transform)
            {
                gameObject.transform.SetParent(originalParent);
                rectTransform.anchoredPosition = new Vector2(0, 0);
            }
            pointerText.SetActive(false);
        }
        else
        {
            isLocked = false;
        }
    }
    public void Reset()
    {
        gameObject.transform.SetParent(originalParent);
        rectTransform.anchoredPosition = new Vector2(0, 0);
        filterManager.ReciveActiveFilter(gameObject.name);
        pointerText.SetActive(false);
    }
    public void TakeSelectedValue()
    {
        movingValue = refinerPanelUI.selectorValue;
        refinerPanelUI.ResetSelectorValue();
    }
}
