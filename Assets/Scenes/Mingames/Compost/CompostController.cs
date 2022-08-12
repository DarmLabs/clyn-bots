using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompostController : MonoBehaviour
{
    [SerializeField] Transform ProgresoHumedo;
    [SerializeField] Transform ProgresoOlor;
    [SerializeField] Transform ProgresoSeco;
    private float EscalaMax = 2.65f;
    private float EscalaMin = 0f;
    [SerializeField] Transform BotonSecar;
    [SerializeField] Transform BotonHumedecer;
    [SerializeField] Transform BotonRemover;
    private Vector3 escalaProgreso_Humedo;
    private Vector3 escalaProgreso_Olor;
    private Vector3 escalaProgreso_Seco;
    //0 Humeda - 1 Seca - 3 Olor - 4 Correcta
    [SerializeField] Transform Compostera;
    private int randomIndex;  

    void Start()
    {
        randomIndex = Random.Range(0,3);
        Compostera.GetChild(randomIndex).gameObject.SetActive(true);
        escalaProgreso_Humedo = ProgresoHumedo.localScale;
        escalaProgreso_Olor = ProgresoOlor.localScale;
        escalaProgreso_Seco = ProgresoSeco.localScale; 
        switch (randomIndex)
        {
            case 0://Humeda
            escalaProgreso_Humedo.x = 1.9875f;
            escalaProgreso_Olor.x = 1.325f;
            escalaProgreso_Seco.x = 1.325f;
            ProgresoHumedo.localScale = escalaProgreso_Humedo;
            ProgresoOlor.localScale = escalaProgreso_Olor;
            ProgresoSeco.localScale = escalaProgreso_Seco;            
            break;
            case 1://Seca
            escalaProgreso_Humedo.x =1.325f;
            escalaProgreso_Olor.x = 1.325f;
            escalaProgreso_Seco.x = 1.9875f;
            ProgresoHumedo.localScale = escalaProgreso_Humedo;
            ProgresoOlor.localScale = escalaProgreso_Olor;
            ProgresoSeco.localScale = escalaProgreso_Seco;
            break;
            case 2://Olor
            escalaProgreso_Humedo.x = 1.325f;
            escalaProgreso_Olor.x = 1.9875f;
            escalaProgreso_Seco.x = 1.325f;
            ProgresoHumedo.localScale = escalaProgreso_Humedo;
            ProgresoOlor.localScale = escalaProgreso_Olor;
            ProgresoSeco.localScale = escalaProgreso_Seco;
            break;
        }        
    }

    void Update()
    {
        if (escalaProgreso_Seco.x >= EscalaMax)
        {
            Debug.Log("PERDISTE NIÑITO :(");
        }
        if (escalaProgreso_Seco.x <= EscalaMin)
        {
            Debug.Log("PERDISTE NIÑITO :(");
        }
    }

    public void Boton_Secar()
    {
        //escalaProgreso_Humedo.x = 2f;
        //escalaProgreso_Olor.x = 1.325f;
        escalaProgreso_Seco.x = escalaProgreso_Seco.x + (EscalaMax/5);
        //ProgresoHumedo.localScale = escalaProgreso_Humedo;
        //ProgresoOlor.localScale = escalaProgreso_Olor;
        ProgresoSeco.localScale = escalaProgreso_Seco; 
    }

    public void Boton_Humedecer()
    {
        
    }

    public void Boton_Remover()
    {
        
    }


    
    


}
