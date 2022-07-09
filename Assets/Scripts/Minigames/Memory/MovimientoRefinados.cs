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
    private float fraction = 0.2f;
    private float currentTime = 0f;
    private float startingTime = 3f;


    void Start() 
    {
        currentTime = startingTime;
        Debug.Log("UBICACIONES Vidrio: "+vidrioRefinadoUbicacion.transform.position);
        Debug.Log("UBICACIONES Metal: "+metalRefinadoUbicacion.transform.position);
        Debug.Log("UBICACIONES Carton: "+cartonRefinadoUbicacion.transform.position);
        Debug.Log("UBICACIONES Plastico: "+plasticoRefinadoUbicacion.transform.position);
        Debug.Log("UBICACIONES Compost: "+compostRefinadoUbicacion.transform.position);
    }
    void Update()
    {
        currentTime -= 1*Time.deltaTime;
        if (currentTime <= 0)
        {
            this.gameObject.transform.position = Vector3.Slerp(this.gameObject.transform.position, vidrioRefinadoUbicacion.transform.position, fraction*Time.deltaTime); 
            this.gameObject.transform.position = Vector3.Slerp(this.gameObject.transform.position, metalRefinadoUbicacion.transform.position, fraction*Time.deltaTime);
            this.gameObject.transform.position = Vector3.Slerp(this.gameObject.transform.position, cartonRefinadoUbicacion.transform.position, fraction*Time.deltaTime);
            this.gameObject.transform.position = Vector3.Slerp(this.gameObject.transform.position, plasticoRefinadoUbicacion.transform.position, fraction*Time.deltaTime);
            this.gameObject.transform.position = Vector3.Slerp(this.gameObject.transform.position, compostRefinadoUbicacion.transform.position, fraction*Time.deltaTime);
            Debug.Log("UBICACIONES OBJETO EN TIEMPO: "+this.gameObject.transform.position);
        }

        
    }
}
