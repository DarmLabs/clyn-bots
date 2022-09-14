using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generador : MonoBehaviour
{
    #region Arrays en uso
    //[SerializeField] private GameObject[] Recuperables;
    [SerializeField] private GameObject[] Residuos;
    [SerializeField] private GameObject[] NoRecuperables;
    [SerializeField] private GameObject[] Organicos;
    [SerializeField] private GameObject[] Vidrios;
    [SerializeField] private GameObject[] Plasticos;
    [SerializeField] private GameObject[] Cartones;
    [SerializeField] private GameObject[] Metales; 

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject Tachos;  
    [SerializeField] private GameObject[] PosicionesTachos;
    #endregion
    #region Indices de arrays
    //private int indexRecuperables = 0;    
    private int indexNoRecuperables = 4;
    private int indexOrganicos = 4;
    private int indexVidrios = 2;
    private int indexPlasticos = 2;
    private int indexCartones = 2;
    private int indexMetales = 1;
    #endregion
    #region Contadores
    //private int cantidadRecuperables = 0;
    private int cantidadResiduos = 0;    
    private int cantidadNoRecuperables = 0;
    private int cantidadOrganicos = 0;
    private int cantidadVidrios = 0;
    private int cantidadPlasticos = 0;
    private int cantidadCartones = 0;
    private int cantidadMetales = 0;
    public static int contadorBasura = 0;
    private float Tiempo = 0f;
    private float intervalo = 0;    
    private float fraction = 10f;
    #endregion
    #region Sorting Layer Config
    public const string PrincipalLayer = "Principal";
    public const string SecundarioLayer = "Secundario";
    public int sortingOrder = 0;
    private SpriteRenderer TachoNoRecSprite;
    private SpriteRenderer TachoRecSprite;
    private SpriteRenderer TachoOrgSprite;
    #endregion   
    #region Banderas
    public static bool Tacho1 = false;
    public static bool Tacho2 = false;
    public static bool Tacho3 = false;
    public static bool Terminaste = false;
    private bool PrimeraVuelta = true;
    #endregion
    public static bool bloqueaMovimiento = false; 
    private Vector3 velocity = Vector3.zero;        
    private GameObject globalaux;
    private GlobalVariables gv; 
    
    void Start () 
    {       
        Tacho1 = false;
        Tacho2 = false;
        Tacho3 = false;
        Terminaste = false;
        bloqueaMovimiento = false;
        PrimeraVuelta = true;
        contadorBasura = 0;
        intervalo = 4;
        globalaux = GameObject.Find("GlobalVariables");
        gv = globalaux.GetComponent<GlobalVariables>();
        TachoNoRecSprite = Tachos.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        TachoRecSprite = Tachos.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();
        TachoOrgSprite = Tachos.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>();        
        //cantidadNoRecuperables = 10;//gv.noRecTrash;
        //cantidadOrganicos = 10;//gv.organicTrash;
        //cantidadRecuperables = 10;//gv.recTrash;           
        cantidadNoRecuperables = Mathf.FloorToInt(Mathf.Round(gv.noRecTrash/3));
        cantidadOrganicos = Mathf.FloorToInt(Mathf.Round(gv.organicTrash/3));
        cantidadVidrios = Mathf.FloorToInt(Mathf.Round(gv.vidrioTrash/3));
        cantidadPlasticos = Mathf.FloorToInt(Mathf.Round(gv.plasticoTrash/3));
        cantidadCartones = Mathf.FloorToInt(Mathf.Round(gv.cartonTrash/3));
        cantidadMetales = Mathf.FloorToInt(Mathf.Round(gv.metalTrash/3));
        cantidadResiduos = cantidadNoRecuperables+cantidadOrganicos+cantidadVidrios+cantidadPlasticos+cantidadCartones+cantidadMetales;              
        Residuos = new GameObject[cantidadResiduos];
        Debug.Log("TOTAL ARRAY RESIDUOS START"+cantidadResiduos);   
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
        /*for (int i = 0; i < (cantidadRecuperables); i++)
        {
            Residuos[i] = Recuperables[Random.Range(0,indexRecuperables+1)];            
        }
        for (int i = 0; i < (cantidadNoRecuperables); i++)
        {
            Residuos[cantidadRecuperables+i]= NoRecuperables[Random.Range(0,indexNoRecuperables+1)];               
        }
        for (int i = 0; i < (cantidadOrganicos); i++)
        {
            Residuos[cantidadRecuperables+cantidadNoRecuperables+i] = Organicos[Random.Range(0,indexOrganicos+1)];            
        }*/
        for (int i = 0; i < (cantidadNoRecuperables); i++)
        {
            Residuos[i] = NoRecuperables[Random.Range(0,indexNoRecuperables+1)];            
        }
        for (int i = 0; i < (cantidadOrganicos); i++)
        {
            Residuos[cantidadOrganicos+i]= Organicos[Random.Range(0,indexOrganicos+1)];               
        }
        for (int i = 0; i < (cantidadVidrios); i++)
        {
            Residuos[cantidadNoRecuperables+cantidadOrganicos+i] = Vidrios[Random.Range(0,indexVidrios+1)];            
        }
        for (int i = 0; i < (cantidadPlasticos); i++)
        {
            Residuos[cantidadNoRecuperables+cantidadOrganicos+cantidadVidrios+i] = Plasticos[Random.Range(0,indexPlasticos+1)];            
        }
        for (int i = 0; i < (cantidadCartones); i++)
        {
            Residuos[cantidadNoRecuperables+cantidadOrganicos+cantidadVidrios+cantidadPlasticos+i]= Cartones[Random.Range(0,indexCartones+1)];               
        }
        for (int i = 0; i < (cantidadMetales); i++)
        {
            Residuos[cantidadNoRecuperables+cantidadOrganicos+cantidadVidrios+cantidadPlasticos+cantidadCartones+i] = Metales[Random.Range(0,indexMetales+1)];            
        }

        for (int t = 0; t < Residuos.Length; t++)
        {
            GameObject temporal = Residuos[t]; 
            int r = Random.Range(t, Residuos.Length);
            Residuos[t] = Residuos[r];
            Residuos[r] = temporal;            
        }
        Debug.Log("TOTAL ARRAY"+Residuos.Length); 
    }
    void Controls()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 2.5f;
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            Time.timeScale = 1f;
        }
        /*
        if (!bloqueaMovimiento)
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
        }  
        */      
        if (Tacho1)
        {
            Tachos.transform.GetChild(0).gameObject.transform.position = Vector3.Lerp(Tachos.transform.GetChild(0).gameObject.transform.position, PosicionesTachos[0].transform.position, fraction*Time.deltaTime); 
            Tachos.transform.GetChild(1).gameObject.transform.position = Vector3.Lerp(Tachos.transform.GetChild(1).gameObject.transform.position, PosicionesTachos[1].transform.position, fraction*Time.deltaTime); 
            Tachos.transform.GetChild(2).gameObject.transform.position = Vector3.Lerp(Tachos.transform.GetChild(2).gameObject.transform.position, PosicionesTachos[2].transform.position, fraction*Time.deltaTime);
            TachoNoRecSprite.sortingOrder = sortingOrder;
            TachoNoRecSprite.sortingLayerName = PrincipalLayer;
            TachoRecSprite.sortingOrder = sortingOrder;
            TachoRecSprite.sortingLayerName = SecundarioLayer;
            TachoOrgSprite.sortingOrder = sortingOrder;
            TachoOrgSprite.sortingLayerName = SecundarioLayer;
        }
        if (Tacho2)
        {
            Tachos.transform.GetChild(1).gameObject.transform.position = Vector3.Lerp(Tachos.transform.GetChild(1).gameObject.transform.position, PosicionesTachos[0].transform.position, fraction*Time.deltaTime); 
            Tachos.transform.GetChild(2).gameObject.transform.position = Vector3.Lerp(Tachos.transform.GetChild(2).gameObject.transform.position, PosicionesTachos[1].transform.position, fraction*Time.deltaTime); 
            Tachos.transform.GetChild(0).gameObject.transform.position = Vector3.Lerp(Tachos.transform.GetChild(0).gameObject.transform.position, PosicionesTachos[2].transform.position, fraction*Time.deltaTime); 
            TachoNoRecSprite.sortingOrder = sortingOrder;
            TachoNoRecSprite.sortingLayerName = SecundarioLayer;
            TachoRecSprite.sortingOrder = sortingOrder;
            TachoRecSprite.sortingLayerName = PrincipalLayer;
            TachoOrgSprite.sortingOrder = sortingOrder;
            TachoOrgSprite.sortingLayerName = SecundarioLayer;
        }
        if (Tacho3)
        {
            Tachos.transform.GetChild(2).gameObject.transform.position = Vector3.Lerp(Tachos.transform.GetChild(2).gameObject.transform.position, PosicionesTachos[0].transform.position, fraction*Time.deltaTime); 
            Tachos.transform.GetChild(0).gameObject.transform.position = Vector3.Lerp(Tachos.transform.GetChild(0).gameObject.transform.position, PosicionesTachos[1].transform.position, fraction*Time.deltaTime); 
            Tachos.transform.GetChild(1).gameObject.transform.position = Vector3.Lerp(Tachos.transform.GetChild(1).gameObject.transform.position, PosicionesTachos[2].transform.position, fraction*Time.deltaTime);
            TachoNoRecSprite.sortingOrder = sortingOrder;
            TachoNoRecSprite.sortingLayerName = SecundarioLayer;
            TachoRecSprite.sortingOrder = sortingOrder;
            TachoRecSprite.sortingLayerName = SecundarioLayer;
            TachoOrgSprite.sortingOrder = sortingOrder;
            TachoOrgSprite.sortingLayerName = PrincipalLayer;
        }      
        
    }
    void InstanceIntervalo()    
    {
        if (PrimeraVuelta)
        {
            Instantiate(Residuos[contadorBasura],spawnPoint.position,transform.rotation);
            contadorBasura += 1;
            PrimeraVuelta = false;
        }
        if (Mathf.Round(Tiempo) == intervalo) //&& contadorBasura <= cantidadResiduos)
        {            
            //Debug.Log("Tiempo es igual a intervalo:"+Tiempo);
            if  (contadorBasura < cantidadResiduos)
            {
                Instantiate(Residuos[contadorBasura],spawnPoint.position,transform.rotation);
                contadorBasura += 1;
                Tiempo = 0f;
            }
            else
            {
                Debug.Log("contadorBasura: "+contadorBasura+"  cantidadResiduos: "+cantidadResiduos);
                Terminaste = true;
            }
            
        }
        
    }
    

   
}
