using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generador : MonoBehaviour
{
    #region
    [SerializeField] private GameObject[] Recuperables;
    [SerializeField] private GameObject[] NoRecuperables;
    [SerializeField] private GameObject[] Organicos;
    [SerializeField] private GameObject[] Residuos;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject Tachos;
    #endregion
    #region 
    [SerializeField] private GameObject[] PosicionesTachos;
    private int indexRecuperables = 10;
    private int indexNoRecuperables = 4;
    private int indexOrganicos = 4;

    private int cantidadResiduos = 0;

    private int cantidadRecuperables = 0;
    private int cantidadNoRecuperables = 0;
    private int cantidadOrganicos = 0;
    #endregion
    private float Tiempo = 0f;
    private float intervalo = 0; 
    private int contadorBasura = 0;

    private float fraction = 10f;
    //private float smoothTime = 1F;
    private Vector3 velocity = Vector3.zero;

    private bool Tacho1 = false;
    private bool Tacho2 = false;
    private bool Tacho3 = false;   

    private GameObject globalaux;
    private GlobalVariables gv; 

    
    void Start () 
    {       
        intervalo = 4;
        globalaux = GameObject.Find("GlobalVariables");
        gv = globalaux.GetComponent<GlobalVariables>();
        cantidadNoRecuperables = gv.noRecTrash;
        cantidadOrganicos = gv.organicTrash;
        cantidadRecuperables = gv.recTrash;
        cantidadResiduos = cantidadNoRecuperables + cantidadOrganicos + cantidadRecuperables;        
        Residuos = new GameObject[cantidadResiduos];  
        CreateResiduos();   
    }

    void Update()
    {
        Controls();
        Tiempo += Time.deltaTime;               
        InstanceIntervalo();        
    }  
    
  
    void CreateResiduos()
    {
        for (int i = 0; i < cantidadRecuperables; i++)
        {
            Residuos[i] = Recuperables[Random.Range(0,indexRecuperables+1)];            
        }
        for (int i = 0; i < cantidadNoRecuperables; i++)
        {
            Residuos[cantidadRecuperables+i]= NoRecuperables[Random.Range(0,indexNoRecuperables+1)];               
        }
        for (int i = 0; i < cantidadOrganicos; i++)
        {
            Residuos[cantidadRecuperables+cantidadNoRecuperables+i] = Organicos[Random.Range(0,indexOrganicos+1)];            
        }
        for (int t = 0; t < Residuos.Length; t++)
        {
            GameObject temporal = Residuos[t]; 
            int r = Random.Range(t, Residuos.Length);
            Residuos[t] = Residuos[r];
            Residuos[r] = temporal;
            //Debug.Log("TOTAL ARRAY"+Residuos.Length); 
        }
    }
    void Controls()
    {
        if(Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1) )
        {
            Tacho1 = true;
            Tacho2 = false;
            Tacho3 = false;                     
        }
        if(Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2) )
        {
            Tacho1 = false;
            Tacho2 = true;
            Tacho3 = false;         
        }
        if(Input.GetKeyDown(KeyCode.Keypad3)|| Input.GetKeyDown(KeyCode.Alpha3) )
        {
            Tacho1 = false;
            Tacho2 = false;
            Tacho3 = true;
        }  
        if (Tacho1)
        {
            Tachos.transform.GetChild(0).gameObject.transform.position = Vector3.Lerp(Tachos.transform.GetChild(0).gameObject.transform.position, PosicionesTachos[0].transform.position, fraction*Time.deltaTime); 
            Tachos.transform.GetChild(1).gameObject.transform.position = Vector3.Lerp(Tachos.transform.GetChild(1).gameObject.transform.position, PosicionesTachos[1].transform.position, fraction*Time.deltaTime); 
            Tachos.transform.GetChild(2).gameObject.transform.position = Vector3.Lerp(Tachos.transform.GetChild(2).gameObject.transform.position, PosicionesTachos[2].transform.position, fraction*Time.deltaTime); 
        }
        if (Tacho2)
        {
            Tachos.transform.GetChild(1).gameObject.transform.position = Vector3.Lerp(Tachos.transform.GetChild(1).gameObject.transform.position, PosicionesTachos[0].transform.position, fraction*Time.deltaTime); 
            Tachos.transform.GetChild(2).gameObject.transform.position = Vector3.Lerp(Tachos.transform.GetChild(2).gameObject.transform.position, PosicionesTachos[1].transform.position, fraction*Time.deltaTime); 
            Tachos.transform.GetChild(0).gameObject.transform.position = Vector3.Lerp(Tachos.transform.GetChild(0).gameObject.transform.position, PosicionesTachos[2].transform.position, fraction*Time.deltaTime); 
        }
        if (Tacho3)
        {
            Tachos.transform.GetChild(2).gameObject.transform.position = Vector3.Lerp(Tachos.transform.GetChild(2).gameObject.transform.position, PosicionesTachos[0].transform.position, fraction*Time.deltaTime); 
            Tachos.transform.GetChild(0).gameObject.transform.position = Vector3.Lerp(Tachos.transform.GetChild(0).gameObject.transform.position, PosicionesTachos[1].transform.position, fraction*Time.deltaTime); 
            Tachos.transform.GetChild(1).gameObject.transform.position = Vector3.Lerp(Tachos.transform.GetChild(1).gameObject.transform.position, PosicionesTachos[2].transform.position, fraction*Time.deltaTime);
        }      
        /*Tachos.transform.GetChild(0).gameObject.SetActive(true);
            Tachos.transform.GetChild(1).gameObject.SetActive(false);
            Tachos.transform.GetChild(2).gameObject.SetActive(false);*/
            //Tachos.transform.GetChild(0).gameObject.transform.position = Vector3.Lerp(Tachos.transform.GetChild(0).gameObject.transform.position, PosicionesTachos[0].transform.position, Time.deltaTime); 
            //Tachos.transform.GetChild(0).gameObject.transform.position = Vector3.SmoothDamp(Tachos.transform.GetChild(0).gameObject.transform.position, PosicionesTachos[0].transform.position, ref velocity, smoothTime);          
            /*Tachos.transform.GetChild(0).gameObject.transform.position = PosicionesTachos[0].transform.position;
            Tachos.transform.GetChild(1).gameObject.transform.position = PosicionesTachos[1].transform.position;
            Tachos.transform.GetChild(2).gameObject.transform.position = PosicionesTachos[2].transform.position;
            
            Debug.Log("TransformGetchild: "+Tachos.transform.GetChild(0).gameObject.transform);
            Debug.Log("PosicionTachos: "+PosicionesTachos[0].transform.position);*/
        
    }
    void InstanceIntervalo()    
    {
        if (Mathf.Round(Tiempo) == intervalo) //&& contadorBasura <= cantidadResiduos)
        {            
            //Debug.Log("Tiempo es igual a intervalo:"+Tiempo);
            if  (contadorBasura != cantidadResiduos)
            {
                Instantiate(Residuos[contadorBasura],spawnPoint.position,transform.rotation);
                contadorBasura += 1;
                Tiempo = 0f;
            }
            else
            {
                //Debug.Log("contadorBasura: "+contadorBasura+"  cantidadResiduos: "+cantidadResiduos);
            }
            
        }
        
    }
    

   
}
