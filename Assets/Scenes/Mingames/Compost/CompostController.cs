using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompostController : MonoBehaviour
{
    [SerializeField] Transform ProgresoHumedo;
    [SerializeField] Transform ProgresoOlor;
    [SerializeField] Transform ProgresoSeco;
    //private float EscalaMax = 2.65f;
    //private float EscalaMin = 0f;
    [SerializeField] Transform BotonSecar;
    [SerializeField] Transform BotonHumedecer;
    [SerializeField] Transform BotonRemover;

    //0 Humeda - 1 Seca - 3 Olor - 4 Correcta
    [SerializeField] Transform Compostera;
    private int randomIndex;  


    void Start()
    {
        randomIndex = Random.Range(0,3);
        Compostera.GetChild(randomIndex).gameObject.SetActive(true);
        Vector3 escalaProgreso_Humedo = ProgresoHumedo.localScale;
        Vector3 escalaProgreso_Olor = ProgresoOlor.localScale;
        Vector3 escalaProgreso_Seco = ProgresoSeco.localScale; 

        switch (randomIndex)
        {
            case 0://Humeda
            escalaProgreso_Humedo.x = 2f;
            escalaProgreso_Olor.x = 1.325f;
            escalaProgreso_Seco.x = 1.325f;
            ProgresoHumedo.localScale = escalaProgreso_Humedo;
            ProgresoOlor.localScale = escalaProgreso_Olor;
            ProgresoSeco.localScale = escalaProgreso_Seco;            
            break;

            case 1://Seca
            escalaProgreso_Humedo.x =1.325f;
            escalaProgreso_Olor.x = 1.325f;
            escalaProgreso_Seco.x = 2f;
            ProgresoHumedo.localScale = escalaProgreso_Humedo;
            ProgresoOlor.localScale = escalaProgreso_Olor;
            ProgresoSeco.localScale = escalaProgreso_Seco;
            break;

            case 2://Olor
            escalaProgreso_Humedo.x = 1.325f;
            escalaProgreso_Olor.x = 2f;
            escalaProgreso_Seco.x = 1.325f;
            ProgresoHumedo.localScale = escalaProgreso_Humedo;
            ProgresoOlor.localScale = escalaProgreso_Olor;
            ProgresoSeco.localScale = escalaProgreso_Seco;
            break;
        }        
    }

    public void Boton_Secar()
    {
        Debug.Log("SECAR");
    }
    public void Boton_Humedecer()
    {
        Debug.Log("HUMEDECER");
    }
    public void Boton_Remover()
    {
        Debug.Log("REMOVER");
    }


    
    void Update()
    {
        
    }


}
