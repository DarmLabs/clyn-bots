using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TutorialManager : MonoBehaviour
{
    General_UI general_UI;
    void OnEnable()
    {
        SpawnTutorialWindow("Intro");
    }
    public void SpawnTutorialWindow(string name)
    {
        if (name != "")
        {
            GameObject prefab = Resources.Load<GameObject>("Tutorials/" + name);
            Instantiate(prefab, this.transform);
        }
        else
        {
            gameObject.SetActive(false);
        }

    }
}
