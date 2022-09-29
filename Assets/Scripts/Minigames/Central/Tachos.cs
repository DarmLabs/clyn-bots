using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Tachos : MonoBehaviour
{
    private GameObject globalaux;
    private GlobalVariables gv;
    private GameObject saveaux;
    private SaveLoadSystem saveSystem;
    public static int errores = 0;
    public static int errorRecuperableVidrio = 0;
    public static int errorRecuperablePlastico = 0;
    public static int errorRecuperableCarton = 0;
    public static int errorRecuperableMetal = 0;
    public static int errorNoRecuperable = 0;
    public static int errorOrganico = 0;
    public static int errorRecuperable = 0;
    public static int cantidadAyudas = 3;
    //public int auxiliarGrilla = 0; 
    //public static int CantidadGrilla = 0;
    private int ContadorTotal = 0;
    private string nombreRecuperable;
    private string nombreAuxiliar;
    public Text residuosNoRecuperables;
    public Text residuosRecuperables;
    public Text residuosOrganicos;
    public Text erroresText;

    [SerializeField] private GameObject PanelVictoria;
    [SerializeField] private GameObject PanelDerrota;
    [SerializeField] private Button BotonAyudas;
    [SerializeField] private GameObject flechita;
    public Text CantidadAyudas_Text;
    AudioManager audioManager;     

    void Start()
    {       
        Generador.contadorBasura = 0;
        errores = 0;
        errorRecuperableVidrio = 0;
        errorRecuperablePlastico = 0;
        errorRecuperableCarton = 0;
        errorRecuperableMetal = 0;
        errorNoRecuperable = 0;
        errorOrganico = 0;
        errorRecuperable = 0;
        cantidadAyudas = 3;
        //CantidadGrilla = 0;
        globalaux = GameObject.Find("GlobalVariables");
        gv = globalaux.GetComponent<GlobalVariables>();
        saveaux = GameObject.Find ("SaveLoadSystem");
        saveSystem = saveaux.GetComponent<SaveLoadSystem>();
        audioManager = GameObject.FindObjectOfType<AudioManager>(); 
        gv.divisionNoRec = 0;
        gv.divisionCompostables = 0;
        gv.divisionRec = 0;
        errores = 0;  
        
    }

    void Update()
    {
        ContadorTotal = gv.divisionRec+gv.divisionNoRec+gv.divisionCompostables+errores;
        CantidadAyudas_Text.text = cantidadAyudas.ToString();
        if (errores == 6)
        {
            //Debug.Log ("Perdiste niño bobo");
            audioManager.PlayAudio("Lost");
            //CantidadGrilla = 12;
            PanelDerrota.SetActive(true);
            /*
            gv.recTrash = 0;
            gv.noRecTrash = 0;
            gv.organicTrash = 0;*/
            Time.timeScale = 0f;
            saveSystem.Save();  
        }
        if (Generador.Terminaste && errores < 7 && ContadorTotal == Generador.contadorBasura)
        {
            //Debug.Log ("Ganaste niño inteligente");
            audioManager.PlayAudio("Win");
            /*auxiliarGrilla = Random.Range(0,7);
            switch (auxiliarGrilla)
            {
                case 6:
                    CantidadGrilla = 12;
                break;
                case 5:
                    CantidadGrilla = 14;
                break;
                case 4:
                    CantidadGrilla = 16;
                break;
                case 3:
                    CantidadGrilla = 18;
                break;
                case 2:
                    CantidadGrilla = 20;
                break;
                case 1:
                    CantidadGrilla = 22;
                break;
                case 0:
                    CantidadGrilla = 24;
                break;

            
            }*/            
            PanelVictoria.SetActive(true);            
            gv.divisionCarton += gv.cartonTrash-(3*errorRecuperableCarton);
            gv.divisionMetal += gv.metalTrash-(3*errorRecuperableMetal);
            gv.divisionPlastico += gv.plasticoTrash-(3*errorRecuperablePlastico);
            gv.divisionVidrio +=  gv.vidrioTrash-(3*errorRecuperableVidrio);
            gv.divisionNoRec += gv.noRecTrash-(3*errorNoRecuperable);
            gv.divisionOrganic += gv.organicTrash-(3*errorOrganico);
            gv.noRecTrash=0;
            gv.organicTrash=0;
            gv.vidrioTrash=0;
            gv.plasticoTrash=0;
            gv.cartonTrash=0;
            gv.metalTrash=0;
            Time.timeScale = 0f;            
            Generador.Terminaste = false;
            gv.memoriaAccesible = true;
            saveSystem.Save();
        }
        if(cantidadAyudas<=0)
        {
            BotonAyudas.interactable = false;
            Destroy(flechita);
        }
            
    }
    void  OnMouseDown() 
    {        
        if (!Generador.bloqueaMovimiento)
        {
            if (this.gameObject.name == "NoRecuperable")
            {
                Generador.Tacho1 = true;
                Generador.Tacho2 = false;
                Generador.Tacho3 = false;
            }
            if (this.gameObject.name == "Recuperable")
            {
                Generador.Tacho1 = false;
                Generador.Tacho2 = true;
                Generador.Tacho3 = false; 
            }
            if (this.gameObject.name == "Organico")
            {
                Generador.Tacho1 = false;
                Generador.Tacho2 = false;
                Generador.Tacho3 = true;
            }
        }                  
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (this.gameObject.tag == other.gameObject.tag)
        {
            switch (other.gameObject.tag)
            {
                case "Recuperable":
                    gv.divisionRec +=1;
                    audioManager.PlayAudio("Acierto_Sound");
                    //Debug.Log("DIVISION RECUPERABLES: "+gv.divisionRec);
                    residuosRecuperables.text = gv.divisionRec.ToString();
                    switch (other.transform.GetChild(0).gameObject.tag)
                    {
                        case "Vidrio":
                            gv.divisionVidrio +=1;
                            Debug.Log("dividió vidrio"+gv.divisionVidrio);
                        break;
                        case "Plastico":
                            gv.divisionPlastico +=1; 
                            Debug.Log("dividió plastico"+gv.divisionPlastico);
                        break;
                        case "Metal":
                            gv.divisionMetal +=1;
                            Debug.Log("dividió metal"+gv.divisionMetal);
                        break;
                        case "Carton":
                            gv.divisionCarton +=1;
                            Debug.Log("dividió carton"+gv.divisionCarton);
                        break;
                    }
                    //Destroy(other.gameObject);
                    //Debug.Log("Se separó un recuperable");
                    //gv.recTrash -= 1;
                    
                    /*nombreRecuperable = this.gameObject.name;
                    if(nombreRecuperable.Contains("Vidrio"))
                    {
                        //gv.vidrioTrash -=1;
                        gv.divisionVidrio +=1;
                        Debug.Log("dividió vidrio"+gv.divisionVidrio);
                    }
                    if(nombreRecuperable.Contains("Plastico"))
                    {
                        //gv.plasticoTrash -=1;
                        gv.divisionPlastico +=1; 
                        Debug.Log("dividió plastico"+gv.divisionPlastico);
                    }
                    if(nombreRecuperable.Contains("Carton"))
                    {
                        //gv.cartonTrash -=1;
                        gv.divisionCarton +=1;
                        Debug.Log("dividió carton"+gv.divisionCarton);
                    }
                    if(nombreRecuperable.Contains("Metal"))
                    {
                        //gv.metalTrash -=1;
                        gv.divisionMetal +=1;
                        Debug.Log("dividió metal"+gv.divisionMetal);
                    }*/                                    
                    //Generador.bloqueaMovimiento = false; 
                    
                break;
                case "NoRecuperable":
                    //Destroy(other.gameObject);
                    //Debug.Log("Se separó un no recuperable");
                    //gv.noRecTrash -=1;  
                    gv.divisionNoRec +=1; 
                    audioManager.PlayAudio("Acierto_Sound"); 
                    //Debug.Log("DIVISION NO RECUPERABLES: "+gv.divisionNoRec);
                    //Generador.bloqueaMovimiento = false; 
                    residuosNoRecuperables.text = gv.divisionNoRec.ToString();
                    break;
                case "Organico":
                    //Destroy(other.gameObject);
                    //Debug.Log("Se separó un orgánico");
                    //gv.organicTrash -=1;
                    gv.divisionOrganic+=1;
                    gv.divisionCompostables+=1;
                    audioManager.PlayAudio("Acierto_Sound");
                    //Debug.Log("DIVISION ORGANICOS: "+gv.divisionCompostables);
                    //Generador.bloqueaMovimiento = false; 
                    residuosOrganicos.text = gv.divisionCompostables.ToString();
                    break;
            } 
             
            saveSystem.Save();           
        }
        else
        {
            errores = errores + 1;
            audioManager.PlayAudio("Error_Sound");
            erroresText.text = errores.ToString(); 
            //Debug.Log("ERROR EN RECOLECCION: "+errores);
            switch (other.gameObject.tag)
            {
                case "Recuperable":
                    switch (other.transform.GetChild(0).gameObject.tag)
                    {
                        case "Vidrio":
                            errorRecuperableVidrio +=1;                            
                        break;
                        case "Plastico":
                            errorRecuperablePlastico +=1;
                        break;
                        case "Metal":
                            errorRecuperableMetal +=1;
                        break;
                        case "Carton":
                            errorRecuperableCarton +=1;
                        break;
                    }
                    errorRecuperable+=1;
                    break;
                case "NoRecuperable":
                    //gv.noRecTrash -=1; 
                    errorNoRecuperable+=1;                   
                    break;
                case "Organico":                    
                    //gv.organicTrash -=1;  
                    errorOrganico+=1;                 
                    break;
            } 
            //Generador.bloqueaMovimiento = false;            
            saveSystem.Save(); 
        }       
    }
    
}
