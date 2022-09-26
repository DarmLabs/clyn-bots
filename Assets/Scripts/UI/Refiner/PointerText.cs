using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PointerText : MonoBehaviour
{
    TextMeshProUGUI pointerText;
    void Awake()
    {
        pointerText = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        gameObject.transform.position = mousePosition + new Vector3(60, -60);
    }
    public void FillText(string text)
    {
        pointerText.text = text;
    }
}
