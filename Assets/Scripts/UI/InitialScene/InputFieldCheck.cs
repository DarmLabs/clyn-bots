using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class InputFieldCheck : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] GameObject confirmButton;
    void Update()
    {
        CheckInputEmptiness();
    }
    void CheckInputEmptiness()
    {
        if (inputField.text != "" && !confirmButton.activeSelf)
        {
            confirmButton.SetActive(true);
        }
        else if (confirmButton.activeSelf && inputField.text == "")
        {
            confirmButton.SetActive(false);
        }
    }
}
