using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterClick : MonoBehaviour
{
    Button thisBtn;
    void Start()
    {
        thisBtn = GetComponent<Button>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            thisBtn.onClick.Invoke();
        }
    }
}
