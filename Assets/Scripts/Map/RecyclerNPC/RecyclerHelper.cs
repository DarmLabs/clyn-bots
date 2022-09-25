using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclerHelper : MonoBehaviour
{
    [SerializeField] string infoPanel;
    [SerializeField] GameObject tutoPanel;
    public void ShowPanel()
    {
        GameObject prefab = Resources.Load<GameObject>("Tutorials/" + infoPanel);
        tutoPanel.SetActive(true);
        Instantiate(prefab, tutoPanel.transform);
        Time.timeScale = 0;
        Destroy(this);
    }
}
