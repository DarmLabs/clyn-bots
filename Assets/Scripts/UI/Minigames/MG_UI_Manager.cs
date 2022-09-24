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
    AudioManager audioManager;
    void Start()
    {
        globalaux = GameObject.Find("GlobalVariables");
        gv = globalaux.GetComponent<GlobalVariables>();
        saveaux = GameObject.Find("SaveLoadSystem");
        saveSystem = saveaux.GetComponent<SaveLoadSystem>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        audioManager.StopMusic();
        audioManager.PlayMusic("Minigame_Theme");
    }    

    public void BackToOutside()
    {
        SceneManager.LoadScene("Outside");               
        Grilla.vidas = 20;        
        Generador.contadorBasura = 0;
        for (int i = 0; i < 26; i++)
        {
            PipeController.banderaTubo[i]=false; 
        }
        PipeController.gano = false;
        PipeController.contadorCorrectas = 0; 
        RotatePipe.IndiceFlecha = 0;       
        //saveSystem.Save();
    }

    public void BackToInside()
    {
        SceneManager.LoadScene("Inside");
        gv.recTrash = 0;
        gv.noRecTrash = 0;
        gv.organicTrash = 0;        
        Generador.contadorBasura = 0;
        Grilla.vidas = 20;
        saveSystem.Save();
    }    

    public void completarTutorialCentral()
    {
        gv.tutorialCentral = true;
        Debug.Log("Completaste el tutorial amiguito");
        saveSystem.Save();
    }

    public void Reintentar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Grilla.vidas = 20;
        Time.timeScale = 1f;
        Debug.Log("TIEMPO EN   "+Time.timeScale);        
    }

    public void RetomarTiempo()
    {
        Time.timeScale = 1f;
        Debug.Log("TIEMPO EN   "+Time.timeScale);        
    }

    public void PausaTiempo()
    {
        Time.timeScale = 0f;
        Debug.Log("TIEMPO EN   "+Time.timeScale);  
    }
}
