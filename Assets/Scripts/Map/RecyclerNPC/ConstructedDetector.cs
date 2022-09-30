using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructedDetector : MonoBehaviour
{
    [SerializeField] GameObject[] targetContructions;
    int currentConstructions;
    [SerializeField] int maxConstructions;
    void Start()
    {
        CheckConstruction();
    }
    public void CheckConstruction()
    {
        currentConstructions = 0;
        foreach (var item in targetContructions)
        {
            if (item.tag == "Untagged")
            {
                currentConstructions += 1;
            }
        }
        if (currentConstructions == maxConstructions)
        {
            gameObject.name = "RecyclerAllConstructed";
        }
    }
}
