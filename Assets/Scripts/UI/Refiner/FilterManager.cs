using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FilterManager : MonoBehaviour
{
    FilterComponent[] filterComponents;
    [SerializeField] GameObject[] resources;
    [SerializeField] GameObject numPad;
    [SerializeField] GameObject guideText;
    void Awake()
    {
        filterComponents = GetComponentsInChildren<FilterComponent>();
    }
    public void ReciveRefinerQuantity(int number)
    {
        numPad.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = number.ToString();
    }
    public void ReciveActiveFilter(string code)
    {
        guideText.SetActive(false);
        foreach (var button in filterComponents)
        {
            button.OnDeselect();
        }
        ActiveSelectedResource(code);
    }
    void ActiveSelectedResource(string code)
    {
        foreach (var resource in resources)
        {
            if (resource.name == code + "Refinado")
            {
                resource.SetActive(true);
            }
            else
            {
                resource.SetActive(false);
            }
        }
        numPad.SetActive(true);
    }
}
