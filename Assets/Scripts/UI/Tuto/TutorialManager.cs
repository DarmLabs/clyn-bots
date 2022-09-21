using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialManager : MonoBehaviour
{
    [SerializeField] General_UI general_UI;
    public bool fromMenu;
    public void EnableTutorial()
    {
        if (this.gameObject.transform.childCount == 0)
        {
            SpawnTutorialWindow("Intro");
            GetComponent<Image>().enabled = true;
        }
    }
    public void SpawnTutorialWindow(string name)
    {
        if (name != "")
        {
            GameObject prefab = Resources.Load<GameObject>("Tutorials/" + name);
            Instantiate(prefab, this.transform);
        }
        else if (!fromMenu)
        {
            general_UI.playerInteraction.gv.firstTime = true;
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
            fromMenu = false;
        }
    }
    public void SetFromMenu(bool state)
    {
        fromMenu = state;
    }
}
