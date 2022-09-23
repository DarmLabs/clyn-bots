using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TakeBools : MonoBehaviour
{
    public List<bool> destoyedList = new List<bool>();
    [SerializeField] AspireInteraction[] trashScripts;
    int index = 0;
    void Start()
    {
        destoyedList = FileHandler.ReadListFromJSON<bool>("saveTrash.txt");
        if (destoyedList.Count != 0)
        {
            SetDestroyed();
            destoyedList = new List<bool>();
        }
    }
    public void TakeDestoyed()
    {
        trashScripts = GameObject.FindObjectsOfType<AspireInteraction>(true);
        foreach (var trashScript in trashScripts)
        {
            destoyedList.Add(trashScript.destroyed);
        }
    }
    public void SetDestroyed()
    {
        trashScripts = GameObject.FindObjectsOfType<AspireInteraction>(true);
        foreach (var trashScript in trashScripts)
        {
            trashScript.destroyed = destoyedList[index];
            trashScript.CheckDestroyed();
            index++;
        }
    }

}
