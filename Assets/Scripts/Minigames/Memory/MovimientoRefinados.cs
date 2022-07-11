using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoRefinados : MonoBehaviour
{
    [SerializeField] private GameObject vidrioRefinadoUbicacion;
    [SerializeField] private GameObject metalRefinadoUbicacion;
    [SerializeField] private GameObject cartonRefinadoUbicacion;
    [SerializeField] private GameObject plasticoRefinadoUbicacion;
    [SerializeField] private GameObject compostRefinadoUbicacion;
    private float fraction = 3f;
    private float currentTime = 0f;
    private float startingTime = 1.5f;
    public static bool destruyoRefinado = false;    

    void Start() 
    {
        currentTime = startingTime;
        destruyoRefinado = false;        
    }

    void Update()
    {
        currentTime -= 1*Time.deltaTime;
        if (currentTime <= 0)
        {
            switch (this.gameObject.tag)
            {
                case "VidrioRefinado":
                    this.gameObject.transform.position = Vector3.Slerp(this.gameObject.transform.position, vidrioRefinadoUbicacion.transform.position, fraction*Time.deltaTime); 
                    if (this.gameObject.transform.position == vidrioRefinadoUbicacion.transform.position)
                    {
                        //Destroy(this.gameObject);
                        destruyoRefinado = true; 
                    }
                    break;
                    
                case "MetalRefinado":
                    this.gameObject.transform.position = Vector3.Slerp(this.gameObject.transform.position, metalRefinadoUbicacion.transform.position, fraction*Time.deltaTime);
                    if (this.gameObject.transform.position == metalRefinadoUbicacion.transform.position)
                    {
                        //Destroy(this.gameObject);
                        destruyoRefinado = true; 
                    }
                    break;

                case "CartonRefinado":
                    this.gameObject.transform.position = Vector3.Slerp(this.gameObject.transform.position, cartonRefinadoUbicacion.transform.position, fraction*Time.deltaTime);
                    if (this.gameObject.transform.position == cartonRefinadoUbicacion.transform.position)
                    {
                        //Destroy(this.gameObject);
                        destruyoRefinado = true; 
                    }
                    break;

                case "PlasticoRefinado":                
                    this.gameObject.transform.position = Vector3.Slerp(this.gameObject.transform.position, plasticoRefinadoUbicacion.transform.position, fraction*Time.deltaTime);
                    if (this.gameObject.transform.position == plasticoRefinadoUbicacion.transform.position)
                    {
                        //Destroy(this.gameObject);
                        destruyoRefinado = true; 
                    }
                    break;

                case "CompostRefinado":
                    this.gameObject.transform.position = Vector3.Slerp(this.gameObject.transform.position, compostRefinadoUbicacion.transform.position, fraction*Time.deltaTime);
                    if (this.gameObject.transform.position == compostRefinadoUbicacion.transform.position)
                    {
                        //Destroy(this.gameObject);
                        destruyoRefinado = true; 
                    }
                    break;      
            } 
                       
        }

        
    }    
    

    void OnTriggerEnter(Collider other) 
    {
        Debug.Log("this.gameObject.tag:  "+this.gameObject.tag);
        Debug.Log("other.gameObject.tag:  "+other.gameObject.tag);
        if(this.gameObject.tag == other.gameObject.tag)
        {
            Destroy(this.gameObject);
            destruyoRefinado = true; 
        }        
    }
}
