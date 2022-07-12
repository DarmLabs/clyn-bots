using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cards : MonoBehaviour
{
    public bool facedUp, locked;
    public static bool coroutineAllowed;
    private Cards firstInPair, secondInPair;
    private Cards auxiliar;
    private string firstInPairName, secondInPairName;

    public static Queue<Cards> sequence;
    public static int pairsFound;
    //public int vidas = 20;

    [SerializeField] private GameObject vidrioRefinadoGO;
    [SerializeField] private GameObject metalRefinadoGO;
    [SerializeField] private GameObject cartonRefinadoGO;
    [SerializeField] private GameObject plasticoRefinadoGO;
    [SerializeField] private GameObject compostRefinadoGO;
    [SerializeField] private GameObject ubicacionRefinados; 

    [SerializeField] private GameObject PanelVictoria;
    [SerializeField] private GameObject PanelDerrota;     
    
    private GameObject globalaux;
    private GlobalVariables gv;
    private GameObject saveaux;
    private SaveLoadSystem saveSystem;
    private Collider colisionAux;    

    public Text textVidrios;
    public Text textPlasticos;
    public Text textCartones;
    public Text textMetales;
    public Text textComposts;

    public Image normalVidrios;
    public Image normalPlasticos;
    public Image normalCartones;
    public Image normalMetales;
    public Image normalComposts; 
    
    public static int vidrioPartida;
    public static int plasticoPartida;
    public static int cartonPartida;
    public static int metalPartida;
    public static int compostPartida;
        

    void Start()
    {           
        vidrioPartida=0;
        plasticoPartida=0;
        cartonPartida=0;
        metalPartida=0;
        compostPartida=0;
        Debug.Log("VIDAS INICIALES: "+Grilla.vidas);
        facedUp=false;
        coroutineAllowed=true;
        locked=false;
        sequence=new Queue<Cards>();
        pairsFound=0;
        globalaux = GameObject.Find("GlobalVariables");
        gv = globalaux.GetComponent<GlobalVariables>();
        saveaux = GameObject.Find ("SaveLoadSystem");
        saveSystem = saveaux.GetComponent<SaveLoadSystem>();
    }

    void  OnMouseDown() 
    {
        //si el movimiento no esta bloqueado se voltea la carta
        if (!locked && coroutineAllowed)
        {
            StartCoroutine(RotateCard());            
        }
        
    }

    public IEnumerator RotateCard()
    {
        
        if(Time.timeScale != 0)
        {
            coroutineAllowed=false;
            if(!facedUp)
            {
                sequence.Enqueue(this);
                colisionAux = this.GetComponent<Collider>();
                colisionAux.enabled = !colisionAux.enabled;                
                for (float i = 0f; i < 190f; i+=10)
                {
                    transform.rotation = Quaternion.Euler(0f,i,0f);
                    yield return new WaitForSeconds(0.04f* Time.deltaTime);
                }              
                              
            }
            else if (facedUp)
            {
                for (float i = 190f; i >= 0f; i-=10)
                {
                    transform.rotation=Quaternion.Euler(0f,i,0f);
                    yield return new WaitForSeconds(0.04f* Time.deltaTime);
                    sequence.Clear();
                }
                                                
            }
            
            coroutineAllowed=true;        
            facedUp=!facedUp;           

            if(sequence.Count ==2)
            {                                             
                CheckResults();
            }
        }        
    }

    //al dar vuelta un par de cartas se comprueba si tienen el mismo nombre, lo que quiere decir que son pares
    //caso contrario se vuelven a voltear
    void CheckResults()
    {
        firstInPair=sequence.Dequeue();
        secondInPair=sequence.Dequeue();

        firstInPairName= firstInPair.name.Substring(0,firstInPair.name.Length-7);
        secondInPairName= secondInPair.name.Substring(0, secondInPair.name.Length-7);
        
        if (firstInPairName==secondInPairName)
        {
            firstInPair.locked=true;
            secondInPair.locked=true;
            pairsFound+=1;           
            if (firstInPair.tag == "Recuperable")
            {
                if (firstInPairName == "VasoVidrio" || firstInPairName== "BotellaVidrio" || firstInPairName == "FrascoVidrio")
                {
                    Debug.Log("VIDRIO REFINADO");
                    vidrioPartida+=10;
                    gv.vidrioRefinado = gv.vidrioRefinado + (vidrioPartida);
                    saveSystem.Save();
                    textVidrios.text = "+"+vidrioPartida.ToString();
                    GameObject goVidrio = Instantiate(vidrioRefinadoGO,ubicacionRefinados.transform.position,transform.rotation);
                    goVidrio.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    if (MovimientoRefinados.destruyoRefinado == true)
                    {
                        normalVidrios.gameObject.SetActive(true);
                        StartCoroutine (LateCall(normalVidrios.gameObject));
                        MovimientoRefinados.destruyoRefinado = false;                   
                    }

                } 
                if (firstInPairName == "BotellaPlastico" || firstInPairName== "BidonPlastico" || firstInPairName == "CubiertosPlastico")
                {
                    Debug.Log("PLASTICO REFINADO");
                    plasticoPartida+=10;
                    gv.plasticoRefinado= gv.plasticoRefinado + (plasticoPartida);
                    saveSystem.Save();
                    textPlasticos.text = "+"+plasticoPartida.ToString();
                    GameObject goPlastico = Instantiate(plasticoRefinadoGO,ubicacionRefinados.transform.position,transform.rotation);
                    goPlastico.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    if (MovimientoRefinados.destruyoRefinado == true)
                    {
                        normalPlasticos.gameObject.SetActive(true);
                        StartCoroutine (LateCall(normalPlasticos.gameObject));
                        MovimientoRefinados.destruyoRefinado = false; 
                    }
                    
                }
                if (firstInPairName == "Diario" || firstInPairName== "CajaCarton" || firstInPairName == "CajaHuevos")
                {
                    Debug.Log("CARTON REFINADO");
                    cartonPartida+=10;
                    gv.cartonRefinado= gv.cartonRefinado + (cartonPartida);
                    saveSystem.Save();
                    textCartones.text = "+"+cartonPartida.ToString();
                    GameObject goCarton = Instantiate(cartonRefinadoGO,ubicacionRefinados.transform.position,transform.rotation);
                    goCarton.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    if (MovimientoRefinados.destruyoRefinado == true)
                    {
                        normalCartones.gameObject.SetActive(true);
                        StartCoroutine (LateCall(normalCartones.gameObject));
                        MovimientoRefinados.destruyoRefinado = false; 
                    }

                }       
                if (firstInPairName == "TapaFrasco" || firstInPairName== "LataAluminio")
                {
                    Debug.Log("METAL REFINADO");
                    metalPartida+=10;
                    gv.metalRefinado= gv.metalRefinado + (metalPartida);
                    saveSystem.Save();
                    textMetales.text = "+"+metalPartida.ToString();
                    GameObject goMetal = Instantiate(metalRefinadoGO,ubicacionRefinados.transform.position,transform.rotation);
                    goMetal.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    if (MovimientoRefinados.destruyoRefinado == true)
                    {
                        normalMetales.gameObject.SetActive(true);
                        StartCoroutine (LateCall(normalMetales.gameObject));
                        MovimientoRefinados.destruyoRefinado = false; 
                    }

                }        
            }            
            if (firstInPair.tag == "Organico")
            {
                Debug.Log("COMPOST y BIOMASA");
                compostPartida+=10;
                gv.compostRefinado= gv.compostRefinado + (compostPartida); 
                saveSystem.Save();
                textComposts.text = "+"+compostPartida.ToString();
                GameObject goOrganico = Instantiate(compostRefinadoGO,ubicacionRefinados.transform.position,transform.rotation);
                goOrganico.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                if (MovimientoRefinados.destruyoRefinado == true)
                {
                    normalComposts.gameObject.SetActive(true);
                    StartCoroutine (LateCall(normalComposts.gameObject));
                    MovimientoRefinados.destruyoRefinado = false; 
                }              
            }
        }
        else
        {
            firstInPair.StartCoroutine("RotateBack");
            secondInPair.StartCoroutine("RotateBack");          
            
        }
        if (pairsFound == 9)
        {
                Debug.Log("GANÓ y se guardaron los refinados");
                gv.vidrioRefinado = gv.vidrioRefinado + (vidrioPartida);
                gv.plasticoRefinado= gv.plasticoRefinado + (plasticoPartida);
                gv.cartonRefinado= gv.cartonRefinado + (cartonPartida);
                gv.metalRefinado= gv.metalRefinado + (metalPartida);
                gv.compostRefinado= gv.compostRefinado + (compostPartida); 
                saveSystem.Save(); 
        }


        
    }

    //vuelve a voltear la carta
    public IEnumerator RotateBack()
    {
        coroutineAllowed =false; 
        colisionAux.enabled = !colisionAux.enabled;
        Grilla.vidas = Grilla.vidas -1;  
        Debug.Log("Vidas:"+Grilla.vidas/2);  
        yield return new WaitForSeconds(40f* Time.deltaTime);
        for (float i=190f; i>=0f; i-=10)
        {
            transform.rotation =Quaternion.Euler(0f,i,0f);
            yield return new WaitForSeconds(1f* Time.deltaTime);
            sequence.Clear();            
        }
        facedUp=false;
        coroutineAllowed=true;
    }
    
    IEnumerator LateCall(GameObject objeto)
    {
        yield return new WaitForSeconds(50.0f*Time.deltaTime);
        objeto.SetActive(false);
    }
    
    void Update() 
    {
        if (Grilla.vidas <= 0)
        {
            PanelDerrota.SetActive(true);            
        }
        if (pairsFound == 9)
        {
            //terminó el juego
            if(Grilla.refinadosDestruidos == 9)
            {                
                PanelVictoria.SetActive(true);            
                Debug.Log("Vidas que le quedaron:"+Grilla.vidas);                           
            }
            
        }
    }
           
   
}
