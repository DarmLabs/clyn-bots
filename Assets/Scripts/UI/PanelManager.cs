using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    void Update()
    {
        if (gameObject.transform.childCount == 0)
        {
            gameObject.SetActive(false);
        }
    }
    public void EnableNextPanel()
    {
        GameObject prefab = Resources.Load<GameObject>("Tutorials/Guide02");
    }
}
