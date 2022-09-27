using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorNormal;
    [SerializeField] private Texture2D cursorClic;
    [SerializeField] private Texture2D cursorSobre;
    [SerializeField] private Texture2D cursorClicErrado;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);  
        Cursor.SetCursor(cursorNormal,Vector2.zero,CursorMode.ForceSoftware);
        //Cursor.visible = false;
    }

    
    
    
}
