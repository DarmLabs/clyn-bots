using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class OnPointerOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image image;
    public void OnPointerEnter(PointerEventData data)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
    }
    public void OnPointerExit(PointerEventData data)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
    }
}
