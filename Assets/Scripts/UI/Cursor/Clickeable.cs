using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Clickeable : MonoBehaviour
{
    private GameObject cursorAux;
    private CursorManager cursorM;
    private bool banderaSobre = false;
    private string nombreEscena;
    
    public void Start() 
    {
        banderaSobre = false;
        Scene scene = SceneManager.GetActiveScene();
        nombreEscena = scene.name;
        cursorAux = GameObject.Find("CursorManager");
        cursorM = cursorAux.GetComponent<CursorManager>();
    }

    public void Update() 
    {
        if(nombreEscena=="Central")
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(banderaSobre)
                {
                    if(!Generador.bloqueaMovimiento)
                    {
                        Cursor.SetCursor(cursorM.cursorClic,Vector2.zero,CursorMode.ForceSoftware); 
                    }
                    else
                    {
                        Cursor.SetCursor(cursorM.cursorClicErrado,Vector2.zero,CursorMode.ForceSoftware);
                    }
                    
                }            
            }
            if(Input.GetMouseButtonUp(0))
            {
                if(banderaSobre)
                {
                    Cursor.SetCursor(cursorM.cursorNormal,Vector2.zero,CursorMode.ForceSoftware); 
                }            
            }
        }
        if(nombreEscena=="Memoria")
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(banderaSobre)
                {
                    
                        Cursor.SetCursor(cursorM.cursorClic,Vector2.zero,CursorMode.ForceSoftware); 
                   
                        //Cursor.SetCursor(cursorM.cursorClicErrado,Vector2.zero,CursorMode.ForceSoftware);
                    
                    
                }            
            }
            if(Input.GetMouseButtonUp(0))
            {
                if(banderaSobre)
                {
                    Cursor.SetCursor(cursorM.cursorNormal,Vector2.zero,CursorMode.ForceSoftware); 
                }            
            }
        }


        
    }

    public void OnMouseEnter()
    {
        Cursor.SetCursor(cursorM.cursorSobre,Vector2.zero,CursorMode.ForceSoftware);
        banderaSobre = true;
    }
    
    public void OnMouseExit()
    {
        Cursor.SetCursor(cursorM.cursorNormal,Vector2.zero,CursorMode.ForceSoftware);
        banderaSobre = false;
    }
}
