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
    public static int CantidadGrilla = 0;
    private int ContadorTotal = 0;
    public Text residuosNoRecuperables;
    public Text residuosRecuperables;
    public Text residuosOrganicos;
    public Text erroresText;

    [SerializeField] private GameObject PanelVictoria;
    [SerializeField] private GameObject PanelDerrota;     

    void Start()
    {       
        Generador.contadorBasura = 0;
        CantidadGrilla = 0;
        globalaux = GameObject.Find("GlobalVariables");
        gv = globalaux.GetComponent<GlobalVariables>();
        saveaux = GameObject.Find ("SaveLoadSystem");
        saveSystem = saveaux.GetComponent<SaveLoadSystem>();
        gv.divisionNoRec = 0;
        gv.divisionOrganic = 0;
        gv.divisionRec = 0;
        errores = 0;  
        
    }

    void Update()
    {
        ContadorTotal = gv.divisionRec+gv.divisionNoRec+gv.divisionOrganic+errores;
        if (errores == 6)
        {
            Debug.Log ("Perdiste niño bobo");
            CantidadGrilla = 12;
            PanelDerrota.SetActive(true);
            gv.recTrash = 0;
            gv.noRecTrash = 0;
            gv.organicTrash = 0;
            Time.timeScale = 0f;
            saveSystem.Save();  
        }
        if (Generador.Terminaste && errores < 7 && ContadorTotal == Generador.contadorBasura)
        {
            Debug.Log ("Ganaste niño inteligente");
            switch (errores)
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

            
            }
            PanelVictoria.SetActive(true);
            gv.recTrash = 0;
            gv.noRecTrash = 0;
            gv.organicTrash = 0;
            Time.timeScale = 0f;            
            Generador.Terminaste = false;
            gv.memoriaAccesible = true;
            saveSystem.Save();
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
                    //Destroy(other.gameObject);
                    //Debug.Log("Se separó un recuperable");
                    gv.recTrash -= 1;
                    gv.divisionRec +=1;
                    Debug.Log("DIVISION RECUPERABLES: "+gv.divisionRec);
                    //Generador.bloqueaMovimiento = false; 
                    residuosRecuperables.text = "Residuos Recuperables:"+gv.divisionRec.ToString();
                    break;
                case "NoRecuperable":
                    //Destroy(other.gameObject);
                    //Debug.Log("Se separó un no recuperable");
                    gv.noRecTrash -=1;  
                    gv.divisionNoRec +=1;  
                    Debug.Log("DIVISION NO RECUPERABLES: "+gv.divisionNoRec);
                    //Generador.bloqueaMovimiento = false; 
                    residuosNoRecuperables.text = "Residuos No Recuperables:"+gv.divisionNoRec.ToString();
                    break;
                case "Organico":
                    //Destroy(other.gameObject);
                    //Debug.Log("Se separó un orgánico");
                    gv.organicTrash -=1;
                    gv.divisionOrganic+=1;
                    Debug.Log("DIVISION ORGANICOS: "+gv.divisionOrganic);
                    //Generador.bloqueaMovimiento = false; 
                    residuosOrganicos.text = "Residuos Organicos:"+gv.divisionOrganic.ToString();
                    break;
            } 
             
            saveSystem.Save();           
        }
        else
        {
            errores = errores + 1;
            Debug.Log("ERROR EN RECOLECCION: "+errores);
            switch (other.gameObject.tag)
            {
                case "Recuperable":                 
                    gv.recTrash -= 1;                                      
                    break;
                case "NoRecuperable":
                    gv.noRecTrash -=1;                    
                    break;
                case "Organico":                    
                    gv.organicTrash -=1;                   
                    break;
            } 
            //Generador.bloqueaMovimiento = false;
            erroresText.text = "Errores: "+errores.ToString(); 
            saveSystem.Save(); 
        }       
    }
    
}
