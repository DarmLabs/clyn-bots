using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public void EnableNextPanel()
    {
        GameObject prefab = Resources.Load<GameObject>("Tutorials/Guide02");
        Instantiate(prefab, this.transform);
    }
    public void CloseTutoPanel()
    {
        gameObject.SetActive(false);
    }
}
