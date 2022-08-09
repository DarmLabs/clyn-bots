using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePipe : MonoBehaviour
{
   [SerializeField] private Vector3[] ArrayRotaciones;   
   private int randomIndex = 0;
   

   void Start()
   {
        randomIndex = Random.Range(0,3);
        transform.Rotate(ArrayRotaciones[randomIndex]);
        Debug.Log("pieza: "+this.gameObject.name+"  array:"+ArrayRotaciones[randomIndex]);
   }

   void Update() 
   {
        if (transform.rotation == Quaternion.identity)
        {
            transform.localScale = new Vector3(1.09f,1.09f,1.09f);
        }            
        else
        {
            transform.localScale = new Vector3(0.91f,0.91f,0.91f);
        }
   }

   void OnMouseDown() 
   {
        if(!PipeController.gano)
        {
            if(transform.rotation.z != 0)
            {
                transform.Rotate(0f,0f,90f);
            }
                        
        }
   }
}
