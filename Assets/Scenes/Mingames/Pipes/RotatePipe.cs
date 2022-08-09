using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePipe : MonoBehaviour
{
   void OnMouseDown() 
   {
        if(!PipeController.gano)
        {
            transform.Rotate(0f,0f,90f);
            if (transform.rotation.z == 0)
            {
                transform.localScale = new Vector3(1.09f,1.09f,1.09f);
            }
            else
            {
                transform.localScale = new Vector3(0.91f,0.91f,0.91f);
            }
        }
   }
}
