using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorAudios : MonoBehaviour
{
    AudioManager audioManager;
    private GameObject globalaux;
    private GlobalVariables gv;
    private GameObject saveaux;
    private SaveLoadSystem saveSystem;
    
    
    void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();        
        globalaux = GameObject.Find("GlobalVariables");
        gv = globalaux.GetComponent<GlobalVariables>();
        saveaux = GameObject.Find("SaveLoadSystem");
        saveSystem = saveaux.GetComponent<SaveLoadSystem>();
        
    }
    void Update() 
    {
        if(gv.sonidoOff)
        {
            AudioListener.volume = 0;
        }
        if(!gv.sonidoOff)
        {
            AudioListener.volume = 1;
        }
       
        
    }

    public void Mutear(bool muted)
    {
        if(muted)
        {
            AudioListener.volume = 0;
            gv.sonidoOff = true;
            saveSystem.Save();
        }
        if(!muted)
        {
            AudioListener.volume = 1;
            gv.sonidoOff = false;
            saveSystem.Save();
        }
    }
    public void PararMusica(bool musicstop)
    {
        if(musicstop)
        {
            audioManager.StopMusic();
            gv.musicaOff = true;
            saveSystem.Save();
        }
        if(!musicstop)
        {
            audioManager.PlayMusic("Minigame_Theme");
            gv.musicaOff = false;
            saveSystem.Save();
        }
    }
}
