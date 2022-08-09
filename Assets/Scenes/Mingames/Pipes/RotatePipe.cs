using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePipe : MonoBehaviour
{
   private bool origen = false;
   private bool primeraVez = false;   

   void Start()
   {
        origen = false;
        primeraVez = false;
   }

   void Update() 
   {
        if (transform.rotation == Quaternion.identity || transform.rotation.z == 0)
        {
            transform.localScale = new Vector3(1.09f,1.09f,1.09f);
            origen = true;
            if(!primeraVez)
            {
                PipeController.contadorCorrectas +=1;
                Debug.Log("CONTADOR CORRECTAS: "+PipeController.contadorCorrectas);
                primeraVez = true;
            }           
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
           if (!origen)
           {
                transform.Rotate(0f,0f,+90f);
           }                 
                     
        }
   }
}
