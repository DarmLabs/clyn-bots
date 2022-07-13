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
    public Text residuosNoRecuperables;
    public Text residuosRecuperables;
    public Text residuosOrganicos;
    public Text erroresText;

    [SerializeField] private GameObject PanelVictoria;
    [SerializeField] private GameObject PanelDerrota;     

    void Start()
    {       
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
        if (errores == 5)
        {
            Debug.Log ("Perdiste niño bobo");
            PanelDerrota.SetActive(true);
            //Time.timeScale = 0f;
            saveSystem.Save();  
        }
        if (Generador.Terminaste && errores < 5)
        {
            Debug.Log ("Ganaste niño inteligente");
            PanelVictoria.SetActive(true);
            //Time.timeScale = 0f;
            saveSystem.Save();
            Generador.Terminaste = false;
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
            //Generador.bloqueaMovimiento = false;
            erroresText.text = "Errores: "+errores.ToString(); 
            saveSystem.Save(); 
        }       
    }
    
}
