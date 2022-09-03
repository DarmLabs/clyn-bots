using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TutorialManager : MonoBehaviour
{
    General_UI general_UI;
    void Start()
    {
        SpawnTutorialWindow("Intro");
    }
    public void SpawnTutorialWindow(string name)
    {
        GameObject prefab = Resources.Load<GameObject>("Tutorials/" + name);
        Instantiate(prefab, this.transform);
    }
}
