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
    [SerializeField] Transform ProgresoTemperatura;
    private float EscalaMax = 1f;
    private float EscalaMin = 0f;
    
    private Vector3 escalaProgreso_Humedo;   
    private Vector3 escalaProgreso_Seco;
    private Vector3 escalaProgreso_Temperatura;

    //0 Humeda - 1 Seca - 2 Olor - 3 Correcta
    [SerializeField] Transform Compostera;  
    [SerializeField] Transform Compostera_Cerrada;  
    private int randomIndex;
    private bool gano = false;
    private bool abierto = false;
    private bool temperaturaCorrecta = false;
    private bool humedadCorrecta = false;

    [SerializeField] private Image BarraHumedo;    
    [SerializeField] private Image BarraSeco; 
    [SerializeField] private Image BarraTemperatura; 

    [SerializeField] private Button Button_Abrir;
    [SerializeField] private Button Button_Cerrar;

    [SerializeField] private Button Button_Secar;
    [SerializeField] private Button Button_Humedecer;
    [SerializeField] private Button Button_Mezclar;
    [SerializeField] Image Imagen_Secar;
    [SerializeField] Image Imagen_Humedecer;
    [SerializeField] Image Imagen_Mezclar;   

    //húmedo: -seco +húmedo || temperatura: +humedad -grados 
    //seco: -humedad +seco || temperatura: +humedad -grados
    //temperatura: +mezclar +grados   
    // 
    
    void Start()
    {        
        gano = false;
        abierto = false;
        humedadCorrecta = false;
        temperaturaCorrecta = false;
        randomIndex = Random.Range(0,2);                       
        escalaProgreso_Humedo = ProgresoHumedo.localScale;        
        escalaProgreso_Seco = ProgresoSeco.localScale; 
        escalaProgreso_Temperatura = ProgresoTemperatura.localScale;
        switch (randomIndex)
        {
            case 0://Humeda
            escalaProgreso_Humedo.x = 0.75f;            
            escalaProgreso_Seco.x = 0.5f;
            escalaProgreso_Temperatura.x= 0.25f;
            ProgresoHumedo.localScale = escalaProgreso_Humedo;
            ProgresoSeco.localScale = escalaProgreso_Seco;  
            ProgresoTemperatura.localScale = escalaProgreso_Temperatura;          
            break;
            case 1://Seca
            escalaProgreso_Humedo.x =0.5f;
            escalaProgreso_Seco.x = 0.75f;
            escalaProgreso_Temperatura.x= 0.75f;
            ProgresoHumedo.localScale = escalaProgreso_Humedo;
            ProgresoSeco.localScale = escalaProgreso_Seco;
            ProgresoTemperatura.localScale = escalaProgreso_Temperatura; 
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
        Button_Abrir.gameObject.SetActive(false);
        Button_Cerrar.gameObject.SetActive(true);
        Button_Secar.interactable = true;
        Button_Humedecer.interactable = true;
        Button_Mezclar.interactable = true;
        Imagen_Secar.color = Color.white;
        Imagen_Humedecer.color = Color.white;
        Imagen_Mezclar.color = Color.white;
    }

    public void Cerrar()
    {
        abierto = false;
        Compostera.gameObject.SetActive(false);
        Compostera.GetChild(randomIndex).gameObject.SetActive(false);
        Compostera_Cerrada.gameObject.SetActive(true);
        Button_Abrir.gameObject.SetActive(true);
        Button_Cerrar.gameObject.SetActive(false);
        Button_Secar.interactable = false;
        Button_Humedecer.interactable = false;
        Button_Mezclar.interactable = false;
        Imagen_Secar.color = Button_Secar.colors.disabledColor;
        Imagen_Humedecer.color = Button_Humedecer.colors.disabledColor;
        Imagen_Mezclar.color = Button_Mezclar.colors.disabledColor;
    }
    //húmedo: -seco +húmedo
    //seco: -humedad +seco
    //temperatura: +humedad -grados || +mezclar +grados 
    public void Boton_Secar()
    {
        if (!humedadCorrecta)
        {
            escalaProgreso_Humedo.x = escalaProgreso_Humedo.x - (EscalaMax/8);        
            if(escalaProgreso_Humedo.x < 0.5f)
            {
                escalaProgreso_Seco.x = escalaProgreso_Seco.x + (EscalaMax/8);
            }        
            ProgresoSeco.localScale = escalaProgreso_Seco; 
            ProgresoHumedo.localScale = escalaProgreso_Humedo;
            ProgresoTemperatura.localScale = escalaProgreso_Temperatura;
        }        
    }
    public void Boton_Humedecer()
    {
        if (!humedadCorrecta)
        {
            escalaProgreso_Seco.x = escalaProgreso_Seco.x - (EscalaMax/8);        
            if(escalaProgreso_Seco.x < 0.5f)
            {
                escalaProgreso_Humedo.x = escalaProgreso_Humedo.x + (EscalaMax/8);
                if(!temperaturaCorrecta)
                {
                    escalaProgreso_Temperatura.x = escalaProgreso_Temperatura.x - (EscalaMax/8);
                }
                
            }        
            ProgresoSeco.localScale = escalaProgreso_Seco; 
            ProgresoHumedo.localScale = escalaProgreso_Humedo;
            ProgresoTemperatura.localScale = escalaProgreso_Temperatura; 
        }              
    }
    public void Boton_Remover()
    {
        if (escalaProgreso_Temperatura.x <= (1f-(EscalaMax/10)))
        {
            escalaProgreso_Temperatura.x = escalaProgreso_Temperatura.x + (EscalaMax/10);
        }
        ProgresoSeco.localScale = escalaProgreso_Seco; 
        ProgresoHumedo.localScale = escalaProgreso_Humedo;
        ProgresoTemperatura.localScale = escalaProgreso_Temperatura;        
    }

    void VictoriaDerrota()
    {
        if (gano) 
        {
                if (!abierto)
                {
                    Debug.Log("GANASTE NIÑITO, ERES UN CAMPEÓN");
                    Compostera.gameObject.SetActive(true);
                    UI_Desactivar.SetActive(false);
                    BotonHumedecer.gameObject.SetActive(false);
                    BotonSecar.gameObject.SetActive(false);
                    BotonRemover.gameObject.SetActive(false);
                    //gano = true;
                    Compostera.GetChild(0).gameObject.SetActive(false);
                    Compostera.GetChild(1).gameObject.SetActive(false);
                    Compostera.GetChild(2).gameObject.SetActive(true);
                }
                
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
            Debug.Log("Se equilibraron");
            humedadCorrecta = true;
            Button_Secar.interactable = false;
            Button_Humedecer.interactable = false;
            Imagen_Secar.color = Button_Secar.colors.disabledColor;
            Imagen_Humedecer.color = Button_Humedecer.colors.disabledColor;
            BarraSeco.color = Color.green;
            BarraHumedo.color = Color.green;
            if(temperaturaCorrecta)
            {
                gano = true;
            }            
        }

        if(escalaProgreso_Temperatura.x >= 0.35f)
        {
           if(escalaProgreso_Temperatura.x <= 0.65f)
           {
                BarraTemperatura.color = Color.green;
                Button_Mezclar.interactable = false;
                Imagen_Mezclar.color = Button_Mezclar.colors.disabledColor;
                temperaturaCorrecta = true;
           }                       
        }  
        if(escalaProgreso_Temperatura.x >= 0.25f)
        {
           if(escalaProgreso_Temperatura.x <= 0.75f)
           {
                if(!temperaturaCorrecta)
                {
                    BarraTemperatura.color = Color.yellow;
                    temperaturaCorrecta = false;
                }
                
           }            
        } 
        if(escalaProgreso_Temperatura.x >= 0.20f)
        {
           if(escalaProgreso_Temperatura.x <= 0.80)
           {
                if(!temperaturaCorrecta)
                {
                    BarraTemperatura.color = Color.yellow;
                    temperaturaCorrecta = false;
                }
           }            
        }         

        if(escalaProgreso_Seco.x == 0.5f)
        {
            BarraSeco.color = Color.green;
        }
        if(escalaProgreso_Humedo.x == 0.5f)
        {
            BarraHumedo.color = Color.green;
        }

        if(escalaProgreso_Seco.x < 0.5f && !humedadCorrecta) 
        {
            BarraSeco.color = Color.yellow;
            humedadCorrecta = false;
        }
        if(escalaProgreso_Humedo.x < 0.5f && !humedadCorrecta)
        {
            BarraHumedo.color = Color.yellow;
            humedadCorrecta = false;
        }

        if(escalaProgreso_Seco.x < 0.3f && !humedadCorrecta)
        {
            BarraSeco.color = Color.red;
            humedadCorrecta = false;
        }
        if(escalaProgreso_Humedo.x < 0.3f && !humedadCorrecta)
        {
            BarraHumedo.color = Color.red;
            humedadCorrecta = false;
        } 

        if(escalaProgreso_Seco.x > 0.5f && !humedadCorrecta)
        {
            BarraSeco.color = Color.yellow;
            humedadCorrecta = false;
        }
        if(escalaProgreso_Humedo.x > 0.5f && !humedadCorrecta)
        {
            BarraHumedo.color = Color.yellow;
            humedadCorrecta = false;
        }     

        if(escalaProgreso_Seco.x > 0.8f && !humedadCorrecta)
        {
            BarraSeco.color = Color.red;
            humedadCorrecta = false;
        }
        if(escalaProgreso_Humedo.x > 0.8f && !humedadCorrecta)
        {
            BarraHumedo.color = Color.red;
            humedadCorrecta = false;    
        }


    }


}
