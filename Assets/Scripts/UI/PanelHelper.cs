using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelHelper : MonoBehaviour
{
    public void SetParentOff()
    {
        gameObject.transform.parent.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    public void DestroyThisPanel()
    {
        Destroy(this.gameObject);
    }
}
