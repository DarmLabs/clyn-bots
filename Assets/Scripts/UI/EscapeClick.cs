using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscapeClick : MonoBehaviour
{
    Button thisBtn;
    void Start()
    {
        thisBtn = GetComponent<Button>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameObject.activeInHierarchy)
        {
            thisBtn.onClick.Invoke();
        }
    }
}
