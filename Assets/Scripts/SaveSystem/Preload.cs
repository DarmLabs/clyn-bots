using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preload : MonoBehaviour
{
    public GameObject[] preloadObjects;
    void Start()
    {
        foreach (var item in preloadObjects)
        {
            item.SetActive(false);
        }
    }
}
