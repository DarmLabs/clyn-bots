using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePipe : MonoBehaviour
{
   private bool primeraVez = false;
   public bool coroutineAllowed;
   public string nombre;
   public int numero;
   public static int IndiceFlecha = 0;
   private Renderer rend;
   [SerializeField] Color colorCorrecto = Color.white;   

   void Start()
   {
          coroutineAllowed =true;          
          primeraVez = false;
          rend = GetComponent<Renderer>();
          nombre = this.gameObject.name;
          numero = int.Parse(nombre);
          //Debug.Log("numero:  "+numero);
   }

   void Update() 
   {
        if (transform.rotation == Quaternion.identity) 
        {                                           
               if(numero > 0)
               {
                    if (PipeController.banderaTubo[numero-1])
                    {
                         transform.localScale = new Vector3(1.09f,1.09f,1.09f); 
                                               
                         if(!primeraVez)
                         {
                              PipeController.contadorCorrectas +=1;
                              IndiceFlecha = numero +1;
                              //Debug.Log("IndiceFlecha: "+RotatePipe.IndiceFlecha); 
                              Debug.Log("CONTADOR CORRECTAS: "+PipeController.contadorCorrectas);
                              primeraVez = true;
                              rend.material.color = colorCorrecto;                              
                         } 
                    }
               }
               else
               {
                    transform.localScale = new Vector3(1.09f,1.09f,1.09f);              
                    if(!primeraVez)
                    {
                         IndiceFlecha = numero +1;
                         //Debug.Log("IndiceFlecha: "+RotatePipe.IndiceFlecha); 
                         primeraVez =true;
                         rend.material.color = colorCorrecto;
                    }
                    
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
        if(!PipeController.gano && !PipeController.banderaTubo[numero])
        {
          if (numero == 0 || PipeController.banderaTubo[numero-1])
          {
               transform.Rotate(0f,0f,+90f);               
          }                     
        }      
       

   }
   
}
