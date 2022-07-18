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
    public void BackTo3D()
    {
        SceneManager.LoadScene("Main_Stage");   
        gv.recTrash = 0;  
        gv.noRecTrash =0;
        gv.organicTrash =0;
        saveSystem.Save();   
    }

    public void Reintentar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RetomarTiempo()
    {
        //Time.timeScale = 1f;
        //Debug.Log("TIEMPO: "+Time.timeScale);
    }
}
