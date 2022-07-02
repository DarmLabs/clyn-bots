using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCinta : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform tr;
    private bool sleeping;
    private bool crece;
    private bool nocrece;
    private float fallTime = 0f;
    private Vector3 scaleChange;
    private Vector3 scaleChange1;
     
    void Awake()
    {
          rb = gameObject.GetComponent<Rigidbody2D>();
          tr = gameObject.GetComponent<Transform>();
    }
    void Start()
    {
        sleeping = false;
        fallTime = 0f;
        crece = false;     
        scaleChange = new Vector3(+0.12f, +0.12f, 0f);
        scaleChange1 = new Vector3(+0.08f, +0.08f, 0f);
    }    
    void Update()
    {       
        fallTime += Time.deltaTime;
        if (crece)
        {
            tr.localScale = tr.localScale + (scaleChange*Time.deltaTime);            
        }    
        if (nocrece)
        {
            tr.localScale = tr.localScale + (scaleChange1*Time.deltaTime);
        }    
    }
     
    void OnTriggerEnter2D(Collider2D other) 
    {        
        if (other.tag == "PuntoDireccion")
        {                
            tr.Rotate(0.0f, 0.0f, +7f, Space.Self);
            if(other.name == "base")
            {
                if (fallTime > 1f)
                {               
                    if (sleeping)
                    {
                        rb.WakeUp();                                               
                    }
                    else
                    {
                        rb.Sleep();                
                    }
                    sleeping = !sleeping;
            
                    fallTime = 0.00f;
                }
            }            
            if(other.name == "crece")
            {
                crece = true;
                nocrece = false;
            }
            if(other.name == "nocrece")
            {
                crece = false;
                nocrece=true;
                tr.Rotate(0.0f, 0.0f, +5f, Space.Self);
            }
        }
    }
}
