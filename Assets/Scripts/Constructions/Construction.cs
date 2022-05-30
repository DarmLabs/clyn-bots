using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construction : MonoBehaviour
{
    public int reqRec, reqOrg;
    void Start()
    {
        reqRec = Random.Range(3,7);
        reqOrg = Random.Range(3,7);
        ChangeAlpha(GetComponent<Renderer>().material, 0.5f);
    }
    void ChangeAlpha(Material mat, float alphaVal){
        Color oldColor = mat.color;
        Color newColor = new Color(oldColor.r,oldColor.g,oldColor.b, alphaVal);
        mat.SetColor("_Color", newColor);
    }
    public void Constructed(){
        ChangeAlpha(GetComponent<Renderer>().material, 1);
        Destroy(GetComponent<BoxCollider>());
        gameObject.AddComponent(typeof(BoxCollider));
    }
}
