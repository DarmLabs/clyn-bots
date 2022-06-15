using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPile : MonoBehaviour
{
    Vector3 scaleChange;
    public void RecudeHeight()
    {
        scaleChange = new Vector3(0,0,0.2f);
        transform.localScale -= scaleChange;
        if(transform.localScale.z <= 0.1f)
        {
            gameObject.SetActive(false);
        }
    } 
}
