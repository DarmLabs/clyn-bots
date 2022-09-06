using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPanel : MonoBehaviour
{
    public void DestroyThisPanel()
    {
        Destroy(gameObject);
    }
    public void GetParentScript(string name)
    {
        TutorialManager tutorialManager = GetComponentInParent<TutorialManager>();
        tutorialManager.SpawnTutorialWindow(name);
    }
}
