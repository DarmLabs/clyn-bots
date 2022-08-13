using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompostController : MonoBehaviour
{
    [SerializeField] private GameObject PanelVictoria;
    [SerializeField] private GameObject PanelDerrota;
    [SerializeField] private GameObject UI_Desactivar;
    [SerializeField] Transform BotonSecar;
    [SerializeField] Transform BotonHumedecer;
    [SerializeField] Transform BotonRemover;
    
    [SerializeField] Transform ProgresoHumedo;    
    [SerializeField] Transform ProgresoSeco;
    private float EscalaMax = 1f;
    private float EscalaMin = 0f;
    
    private Vector3 escalaProgreso_Humedo;   
    private Vector3 escalaProgreso_Seco;

    //0 Humeda - 1 Seca - 2 Olor - 3 Correcta
    [SerializeField] Transform Compostera;  
    [SerializeField] Transform Compostera_Cerrada;  
    private int randomIndex;
    private bool gano = false;
    private bool abierto = false;

    [SerializeField] private Image BarraHumedo;    
    [SerializeField] private Image BarraSeco;    

    void Start()
    {        
        gano = false;
        abierto = false;
        randomIndex = Random.Range(0,2);                       
        escalaProgreso_Humedo = ProgresoHumedo.localScale;        
        escalaProgreso_Seco = ProgresoSeco.localScale; 
        switch (randomIndex)
        {
            case 0://Humeda
            escalaProgreso_Humedo.x = 0.75f;            
            escalaProgreso_Seco.x = 0.5f;
            ProgresoHumedo.localScale = escalaProgreso_Humedo;
            ProgresoSeco.localScale = escalaProgreso_Seco;            
            break;
            case 1://Seca
            escalaProgreso_Humedo.x =0.5f;
            escalaProgreso_Seco.x = 0.75f;
            ProgresoHumedo.localScale = escalaProgreso_Humedo;
            ProgresoSeco.localScale = escalaProgreso_Seco;
            break;
        }        
    }

    void Update()
    {
        VictoriaDerrota();
        CambiarCompostActiva();
        ActualizarColores();       
    }

    public void Abrir()
    {
        abierto = true;
        Compostera.gameObject.SetActive(true);
        Compostera.GetChild(randomIndex).gameObject.SetActive(true);
        Compostera_Cerrada.gameObject.SetActive(false);
    }

    public void Cerrar()
    {
        abierto = false;
        Compostera.gameObject.SetActive(false);
        Compostera.GetChild(randomIndex).gameObject.SetActive(false);
        Compostera_Cerrada.gameObject.SetActive(true);
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
    }
    public void Boton_Remover()
    {
        ProgresoSeco.localScale = escalaProgreso_Seco; 
        ProgresoHumedo.localScale = escalaProgreso_Humedo;        
    }

    void VictoriaDerrota()
    {
        if (gano) 
        {
                Debug.Log("GANASTE NIÑITO, ERES UN CAMPEÓN");
                Compostera.gameObject.SetActive(true);
                UI_Desactivar.SetActive(false);
                BotonHumedecer.gameObject.SetActive(false);
                BotonSecar.gameObject.SetActive(false);
                BotonRemover.gameObject.SetActive(false);
                gano = true;
                Compostera.GetChild(0).gameObject.SetActive(false);
                Compostera.GetChild(1).gameObject.SetActive(false);
                Compostera.GetChild(2).gameObject.SetActive(true);
        } 

        if (escalaProgreso_Seco.x >= EscalaMax)
        {
            Debug.Log("PERDISTE NIÑITO :(");
            Compostera.gameObject.SetActive(false);
            PanelDerrota.SetActive(true);
            UI_Desactivar.SetActive(false);
            BotonHumedecer.gameObject.SetActive(false);
            BotonSecar.gameObject.SetActive(false);
            BotonRemover.gameObject.SetActive(false);
        }
        if (escalaProgreso_Seco.x <= EscalaMin)
        {
            Debug.Log("PERDISTE NIÑITO :(");
            Compostera.gameObject.SetActive(false);
            PanelDerrota.SetActive(true);
            UI_Desactivar.SetActive(false);
            BotonHumedecer.gameObject.SetActive(false);
            BotonSecar.gameObject.SetActive(false);
            BotonRemover.gameObject.SetActive(false);
        }
        if (escalaProgreso_Humedo.x >= EscalaMax)
        {
            Debug.Log("PERDISTE NIÑITO :(");
            Compostera.gameObject.SetActive(false);
            PanelDerrota.SetActive(true);
            UI_Desactivar.SetActive(false);
            BotonHumedecer.gameObject.SetActive(false);
            BotonSecar.gameObject.SetActive(false);
            BotonRemover.gameObject.SetActive(false);
        }
        if (escalaProgreso_Humedo.x <= EscalaMin)
        {
            Debug.Log("PERDISTE NIÑITO :(");
            Compostera.gameObject.SetActive(false);
            PanelDerrota.SetActive(true);
            UI_Desactivar.SetActive(false);
            BotonHumedecer.gameObject.SetActive(false);
            BotonSecar.gameObject.SetActive(false);
            BotonRemover.gameObject.SetActive(false);
        }       
             
    } 

    void CambiarCompostActiva()
    {
        //0 Humeda - 1 Seca - 2 Olor - 3 Correcta
        if(escalaProgreso_Humedo.x > escalaProgreso_Seco.x )
        {
            if (!gano)
            {
                Compostera.GetChild(1).gameObject.SetActive(false);
                Compostera.GetChild(0).gameObject.SetActive(true);
            }
        }
        if(escalaProgreso_Seco.x > escalaProgreso_Humedo.x)
        {
            if (!gano)
            {
                Compostera.GetChild(0).gameObject.SetActive(false);
                Compostera.GetChild(1).gameObject.SetActive(true);
            }
        }
    } 
    
    void ActualizarColores()
    {
        if(escalaProgreso_Seco.x == escalaProgreso_Humedo.x)
        {
            BarraSeco.color = Color.green;
            BarraHumedo.color = Color.green;
            gano = true;
        }

        if(escalaProgreso_Seco.x == 0.5f)
        {
            BarraSeco.color = Color.green;
        }
        if(escalaProgreso_Humedo.x == 0.5f)
        {
            BarraHumedo.color = Color.green;
        }

        if(escalaProgreso_Seco.x < 0.5f)
        {
            BarraSeco.color = Color.yellow;
        }
        if(escalaProgreso_Humedo.x < 0.5f)
        {
            BarraHumedo.color = Color.yellow;
        }

        if(escalaProgreso_Seco.x < 0.3f)
        {
            BarraSeco.color = Color.red;
        }
        if(escalaProgreso_Humedo.x < 0.3f)
        {
            BarraHumedo.color = Color.red;
        } 

        if(escalaProgreso_Seco.x > 0.5f)
        {
            BarraSeco.color = Color.yellow;
        }
        if(escalaProgreso_Humedo.x > 0.5f)
        {
            BarraHumedo.color = Color.yellow;
        }     

        if(escalaProgreso_Seco.x > 0.8f)
        {
            BarraSeco.color = Color.red;
        }
        if(escalaProgreso_Humedo.x > 0.8f)
        {
            BarraHumedo.color = Color.red;    
        }


    }


}
