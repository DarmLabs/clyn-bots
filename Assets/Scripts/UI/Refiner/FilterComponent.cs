using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FilterComponent : MonoBehaviour
{
    [SerializeField] Sprite selectedSpirte;
    Sprite originalSprite;
    FilterManager filterManager;
    public int refinerCount;
    void Awake()
    {
        originalSprite = gameObject.GetComponent<Image>().sprite;
        filterManager = GetComponentInParent<FilterManager>();
    }
    public void OnSelected(bool fromButton)
    {
        if (fromButton)
        {
            filterManager.ReciveActiveFilter(gameObject.name);
        }
        gameObject.GetComponent<Image>().sprite = selectedSpirte;
    }
    public void OnDeselect()
    {
        gameObject.GetComponent<Image>().sprite = originalSprite;
    }
    public void AssignQuantity()
    {
        filterManager.RefreshNumPad(gameObject.name);
    }
}