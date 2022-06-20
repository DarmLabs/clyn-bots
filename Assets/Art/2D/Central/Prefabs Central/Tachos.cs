using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tachos : MonoBehaviour
{
    private GameObject globalaux;
    private GlobalVariables gv;
    //private GameObject saveaux;
    //private SaveLoadSystem saveSystem;
    private int errores = 0;

    void Start()
    {
        globalaux = GameObject.Find("GlobalVariables");
        gv = globalaux.GetComponent<GlobalVariables>();
        //saveaux = GameObject.Find ("SaveLoadGameObject");
        //saveSystem = saveaux.GetComponent<SaveLoadSystem>();
        
    }

    void Update()
    {
        if (errores == 5)
        {
            Debug.Log ("Perdiste niño bobo");
        }    
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (this.gameObject.tag == other.gameObject.tag)
        {
            switch (other.gameObject.tag)
            {
                case "Recuperable":
                    Destroy(other.gameObject);
                    Debug.Log("Se Destruyó un recuperable");
                    gv.recTrash -= 1;
                    break;
                case "NoRecuperable":
                    Destroy(other.gameObject);
                    Debug.Log("Se Destruyó un no recuperable");
                    gv.noRecTrash -=1;                    
                    break;
                case "Organico":
                    Destroy(other.gameObject);
                    Debug.Log("Se Destruyó un orgánico");
                    gv.organicTrash -=1;
                    break;
            } 
            //saveSystem.Save();           
        }
        else
        {
            errores += 1;
            Debug.Log("ERROR EN RECOLECCION");
        }
    }
    
}
