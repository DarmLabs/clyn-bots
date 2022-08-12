using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompostController : MonoBehaviour
{
    [SerializeField] Transform ProgresoHumedo;
    [SerializeField] Transform ProgresoOlor;
    [SerializeField] Transform ProgresoSeco;
    private float EscalaMax = 1f;
    private float EscalaMin = 0f;
    [SerializeField] Transform BotonSecar;
    [SerializeField] Transform BotonHumedecer;
    [SerializeField] Transform BotonRemover;
    private Vector3 escalaProgreso_Humedo;
    private Vector3 escalaProgreso_Olor;
    private Vector3 escalaProgreso_Seco;
    //0 Humeda - 1 Seca - 2 Olor - 3 Correcta
    [SerializeField] Transform Compostera;
    private int randomIndex;  
    private bool gano = false;

    void Start()
    {
        gano = false;
        randomIndex = Random.Range(0,3);
        Compostera.GetChild(randomIndex).gameObject.SetActive(true);        
        escalaProgreso_Humedo = ProgresoHumedo.localScale;
        escalaProgreso_Olor = ProgresoOlor.localScale;
        escalaProgreso_Seco = ProgresoSeco.localScale; 
        switch (randomIndex)
        {
            case 0://Humeda
            escalaProgreso_Humedo.x = 0.75f;
            escalaProgreso_Olor.x = 0.5f;
            escalaProgreso_Seco.x = 0.5f;
            ProgresoHumedo.localScale = escalaProgreso_Humedo;
            ProgresoOlor.localScale = escalaProgreso_Olor;
            ProgresoSeco.localScale = escalaProgreso_Seco;            
            break;
            case 1://Seca
            escalaProgreso_Humedo.x =0.5f;
            escalaProgreso_Olor.x = 0.5f;
            escalaProgreso_Seco.x = 0.75f;
            ProgresoHumedo.localScale = escalaProgreso_Humedo;
            ProgresoOlor.localScale = escalaProgreso_Olor;
            ProgresoSeco.localScale = escalaProgreso_Seco;
            break;
            case 2://Olor
            escalaProgreso_Humedo.x = 0.5f;
            escalaProgreso_Olor.x = 0.75f;
            escalaProgreso_Seco.x = 0.5f;
            ProgresoHumedo.localScale = escalaProgreso_Humedo;
            ProgresoOlor.localScale = escalaProgreso_Olor;
            ProgresoSeco.localScale = escalaProgreso_Seco;
            break;
        }        
    }

    void Update()
    {
        VictoriaDerrota();
    }

    public void Boton_Secar()
    {
        escalaProgreso_Humedo.x = escalaProgreso_Humedo.x - (EscalaMax/8);        
        if(escalaProgreso_Humedo.x < 0.5f)
        {
            escalaProgreso_Seco.x = escalaProgreso_Seco.x + (EscalaMax/8);
        }        
        ProgresoSeco.localScale = escalaProgreso_Seco; 
        ProgresoHumedo.localScale = escalaProgreso_Humedo;
        ProgresoOlor.localScale = escalaProgreso_Olor;
    }
    public void Boton_Humedecer()
    {
        escalaProgreso_Seco.x = escalaProgreso_Seco.x - (EscalaMax/8);        
        if(escalaProgreso_Seco.x < 0.5f)
        {
            escalaProgreso_Humedo.x = escalaProgreso_Humedo.x + (EscalaMax/8);
        }        
        ProgresoSeco.localScale = escalaProgreso_Seco; 
        ProgresoHumedo.localScale = escalaProgreso_Humedo;
        ProgresoOlor.localScale = escalaProgreso_Olor;        
    }
    public void Boton_Remover()
    {
        escalaProgreso_Olor.x = escalaProgreso_Olor.x - (EscalaMax/8);        
        if(escalaProgreso_Olor.x < 0.5f)
        {
            escalaProgreso_Seco.x = escalaProgreso_Seco.x + (EscalaMax/8);
        }        
        ProgresoSeco.localScale = escalaProgreso_Seco; 
        ProgresoHumedo.localScale = escalaProgreso_Humedo;
        ProgresoOlor.localScale = escalaProgreso_Olor;        
    }

    void VictoriaDerrota()
    {
        if (escalaProgreso_Seco.x >= EscalaMax)
        {
            Debug.Log("PERDISTE NIÑITO :(");
        }
        if (escalaProgreso_Seco.x <= EscalaMin)
        {
            Debug.Log("PERDISTE NIÑITO :(");
        }
        if (escalaProgreso_Humedo.x >= EscalaMax)
        {
            Debug.Log("PERDISTE NIÑITO :(");
        }
        if (escalaProgreso_Humedo.x <= EscalaMin)
        {
            Debug.Log("PERDISTE NIÑITO :(");
        }
        if (escalaProgreso_Olor.x >= EscalaMax)
        {
            Debug.Log("PERDISTE NIÑITO :(");
        }
        if (escalaProgreso_Olor.x <= EscalaMin)
        {
            Debug.Log("PERDISTE NIÑITO :(");
        }

        if (escalaProgreso_Humedo.x == 0.5f)
        {
            if(escalaProgreso_Olor.x == 0.5f)
            {
                if(escalaProgreso_Seco.x == 0.5f)
                {
                    Debug.Log("GANASTE NIÑITO, ERES UN CAMPEÓN");
                    gano = true;
                    Compostera.GetChild(randomIndex).gameObject.SetActive(false);
                    Compostera.GetChild(3).gameObject.SetActive(true);
                }
            }           
        }
        //0 Humeda - 1 Seca - 2 Olor - 3 Correcta
        if(escalaProgreso_Humedo.x > 0.5)
        {
            if (randomIndex != 0 && !gano)
            {
                Compostera.GetChild(randomIndex).gameObject.SetActive(false);
                Compostera.GetChild(0).gameObject.SetActive(true);
            }
        }
         if(escalaProgreso_Seco.x > 0.5)
        {
            if (randomIndex != 1 && !gano)
            {
                Compostera.GetChild(randomIndex).gameObject.SetActive(false);
                Compostera.GetChild(1).gameObject.SetActive(true);
            }
        }
         if(escalaProgreso_Olor.x > 0.5)
        {
            if (randomIndex != 2 && !gano)
            {
                Compostera.GetChild(randomIndex).gameObject.SetActive(false);
                Compostera.GetChild(2).gameObject.SetActive(true);
            }
        }
        
    }  
    


}
