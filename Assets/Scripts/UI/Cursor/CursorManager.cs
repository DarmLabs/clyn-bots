using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] public Texture2D cursorNormal;
    [SerializeField] public Texture2D cursorClic;
    [SerializeField] public Texture2D cursorSobre;
    [SerializeField] public Texture2D cursorClicErrado;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);  
        Cursor.SetCursor(cursorNormal,Vector2.zero,CursorMode.ForceSoftware);
        //Cursor.visible = false;
    }

    
    
    
}
