using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructedDetector : MonoBehaviour
{
    [SerializeField] GameObject[] targetContructions;
    public void CheckConstruction()
    {
        bool allConstructed = true;
        foreach (var item in targetContructions)
        {
            if (item.tag != "Untagged" || item.tag != "Pipes")
            {
                allConstructed = false;
            }
        }
        if (allConstructed)
        {
            gameObject.name = "RecyclerAllConstructed";
        }
    }
}
