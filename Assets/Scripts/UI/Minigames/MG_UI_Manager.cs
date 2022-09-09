using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MG_UI_Manager : MonoBehaviour
{
    private GameObject globalaux;
    private GlobalVariables gv;
    private GameObject saveaux;
    private SaveLoadSystem saveSystem;
    
    void Start() 
    {
        globalaux = GameObject.Find("GlobalVariables");
        gv = globalaux.GetComponent<GlobalVariables>();
        saveaux = GameObject.Find ("SaveLoadSystem");
        saveSystem = saveaux.GetComponent<SaveLoadSystem>();

    }
    /*
    public void BackTo3D()
    {
        SceneManager.LoadScene("Main_Stage");   
        gv.recTrash = 0;  
        gv.noRecTrash = 0;
        gv.organicTrash = 0;
        gv.compostActiva = true;
        Generador.contadorBasura = 0;
        saveSystem.Save();   
    }*/

    public void BackToOutside()
    {
        SceneManager.LoadScene("Outside");   
        gv.recTrash = 0;  
        gv.noRecTrash = 0;
        gv.organicTrash = 0;
        gv.compostActiva = true;
        Generador.contadorBasura = 0;
        saveSystem.Save();   
    }
    public void BackToInside()
    {
        SceneManager.LoadScene("Inside");   
        gv.recTrash = 0;  
        gv.noRecTrash = 0;
        gv.organicTrash = 0;
        gv.compostActiva = true;
        Generador.contadorBasura = 0;
        saveSystem.Save();   
    }


    public void Reintentar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);        
        Time.timeScale = 1f;
        switch (Tachos.CantidadGrilla)
        {
            case 24:            
            Grilla.vidas = 60; //30
            Cards.CantidadPares = 12;
            break;
            case 22:
            Grilla.vidas = 50; //25 
            Cards.CantidadPares = 11;
            break;
            case 20:
            Grilla.vidas = 40; //20  
            Cards.CantidadPares = 10;  
            break;
            case 18:
            Grilla.vidas = 30;  //15    
            Cards.CantidadPares = 9; 
            break;
            case 16:
            Grilla.vidas = 24; //12  
            Cards.CantidadPares = 8;   
            break;
            case 14:
            Grilla.vidas = 22; //11   
            Cards.CantidadPares = 7;  
            break;
            case 12:
            Grilla.vidas = 20; //10  
            Cards.CantidadPares = 6;    
            break;        
        }        
    }

    public void RetomarTiempo()
    {
        //Time.timeScale = 1f;
        //Debug.Log("TIEMPO: "+Time.timeScale);
    }
}
