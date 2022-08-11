using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePipe : MonoBehaviour
{
   private bool origen = false;
   private bool primeraVez = false;
   public bool coroutineAllowed;
   public string nombre;
   public int numero;
   private Renderer rend;
   [SerializeField] Color colorCorrecto = Color.white;   

   void Start()
   {
          coroutineAllowed =true;
          origen = false;
          primeraVez = false;
          rend = GetComponent<Renderer>();
          nombre = this.gameObject.name;
          numero = int.Parse(nombre);
          Debug.Log("numero:  "+numero);
   }

   void Update() 
   {
        if (transform.rotation == Quaternion.identity || transform.rotation.z == 0 || transform.rotation.z == 360)
        {
               transform.localScale = new Vector3(1.09f,1.09f,1.09f);
               origen = true;               
               if(numero > 0)
               {
                    if (PipeController.banderaTubo[numero-1])
                    {
                         if(!primeraVez)
                         {
                              PipeController.contadorCorrectas +=1;
                              Debug.Log("CONTADOR CORRECTAS: "+PipeController.contadorCorrectas);
                              primeraVez = true;
                              rend.material.color = colorCorrecto;
                         } 

                    }
               }
               else
               {
                    rend.material.color = colorCorrecto;
               } 
              
        }            
        else
        {
            transform.localScale = new Vector3(0.91f,0.91f,0.91f);
            rend.material.color = Color.white;
        }
   }

   void OnMouseDown() 
   {
        if(!PipeController.gano)
        {
           //if (!origen) //&& !PipeController.banderaTubo[numero-1])
           //{
               transform.Rotate(0f,0f,+90f);
               
           //}                 
                     
        }
   }

    /*public IEnumerator RotatePipes()
    {             
          coroutineAllowed=false;
          if(!origen)
          {                            
               for (float i = 0f; i < 90f; i+=10)
               {
               //transform.rotation = Quaternion.Euler(0f,0f,i);
               //transform.Rotate(0f,0f,+i);
               transform.rotation = transform.rotation * Quaternion.Euler(0f,0f,i);
               yield return new WaitForSeconds(1f* Time.deltaTime);
               }       
                              
          }          
          coroutineAllowed=true;                       
                    
    }*/
}
