using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelHelper : MonoBehaviour
{
    public void DestroyThisPanel()
    {
        if (transform.parent.name == "Guide01(Clone)")
        {
            transform.parent.gameObject.GetComponentInParent<PanelManager>().EnableNextPanel();
            Destroy(transform.parent.gameObject);
        }
        else
        {
            Destroy(transform.parent.gameObject);
            transform.parent.gameObject.GetComponentInParent<PanelManager>().CloseTutoPanel();
            Time.timeScale = 1;
        }
    }
}
