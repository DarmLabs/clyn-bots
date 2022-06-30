using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCinta : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform tr;
    private bool sleeping;
    private bool crece;
    private float fallTime = 0f;
    private Vector3 scaleChange;
     
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
        scaleChange = new Vector3(+0.0015f, +0.0015f, 0f);
    }    
    void Update()
    {       
        fallTime += Time.deltaTime;
        if (crece)
        {
            tr.localScale = tr.localScale + scaleChange;
            
        }        
    }
     
    void OnTriggerEnter2D(Collider2D other) 
    {        
        if (other.tag == "PuntoDireccion")
        {                
            tr.Rotate(0.0f, 0.0f, +10f, Space.Self);
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
            if(other.name == "crece")
            {
                crece = true;
            }
        }
    }
}
