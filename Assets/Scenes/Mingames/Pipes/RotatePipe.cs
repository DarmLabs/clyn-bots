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
        }
   }
}
