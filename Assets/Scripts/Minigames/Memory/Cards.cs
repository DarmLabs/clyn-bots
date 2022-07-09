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
        //se inicializan las banderas
        //vidas = 20;
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
           

            if(sequence.Count ==1)
            {
                //Debug.Log("UNA CARTA VOLTEADA");                
                //Debug.Log("vidas: "+vidas);
              

            }

            if(sequence.Count ==2)
            {
                
                //sequence.Enqueue(auxiliar);                
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
            //vidas = vidas + 1;        
            //verificar tags
            //Debug.Log("TAG para refinado de:  "+firstInPair.tag);
            //Debug.Log("vidas dentro tag: "+vidas);
            if (firstInPair.tag == "Recuperable")
            {
                if (firstInPairName == "VasoVidrio" || firstInPairName== "BotellaVidrio" || firstInPairName == "FrascoVidrio")
                {
                    Debug.Log("VIDRIO REFINADO");
                    vidrioPartida+=10;
                    textVidrios.text = "+"+vidrioPartida.ToString();
                    normalVidrios.gameObject.SetActive(true);
                    Instantiate(vidrioRefinadoGO,ubicacionRefinados.transform.position,transform.rotation);
                    StartCoroutine (LateCall(normalVidrios.gameObject));                    

                } 
                if (firstInPairName == "BotellaPlastico" || firstInPairName== "BidonPlastico" || firstInPairName == "CubiertosPlastico")
                {
                    Debug.Log("PLASTICO REFINADO");
                    plasticoPartida+=10;
                    textPlasticos.text = "+"+plasticoPartida.ToString();
                    normalPlasticos.gameObject.SetActive(true);
                    Instantiate(plasticoRefinadoGO,ubicacionRefinados.transform.position,transform.rotation);
                    StartCoroutine (LateCall(normalPlasticos.gameObject));
                    
                }
                if (firstInPairName == "Diario" || firstInPairName== "CajaCarton" || firstInPairName == "CajaHuevos")
                {
                    Debug.Log("CARTON REFINADO");
                    cartonPartida+=10;
                    textCartones.text = "+"+cartonPartida.ToString();
                    normalCartones.gameObject.SetActive(true);
                    Instantiate(cartonRefinadoGO,ubicacionRefinados.transform.position,transform.rotation);
                    StartCoroutine (LateCall(normalCartones.gameObject));

                }       
                if (firstInPairName == "TapaFrasco" || firstInPairName== "LataAluminio")
                {
                    Debug.Log("METAL REFINADO");
                    metalPartida+=10;
                    textMetales.text = "+"+metalPartida.ToString();
                    normalMetales.gameObject.SetActive(true);
                    Instantiate(metalRefinadoGO,ubicacionRefinados.transform.position,transform.rotation);
                    StartCoroutine (LateCall(normalMetales.gameObject));

                }        
            }            
            if (firstInPair.tag == "Organico")
            {
                Debug.Log("COMPOST y BIOMASA");
                compostPartida+=10;
                textComposts.text = "+"+compostPartida.ToString();
                normalComposts.gameObject.SetActive(true);
                Instantiate(compostRefinadoGO,ubicacionRefinados.transform.position,transform.rotation);
                StartCoroutine (LateCall(normalComposts.gameObject));
               

            }
        }
        else
        {
            firstInPair.StartCoroutine("RotateBack");
            secondInPair.StartCoroutine("RotateBack");          
            
        }

        if (pairsFound == 9)
        {
            //terminó el juego
            Debug.Log("GANÓ y se guardaron los refinados");
            Debug.Log("Vidas que le quedaron:"+Grilla.vidas);
            gv.vidrioRefinado = gv.vidrioRefinado + (10*vidrioPartida);
            gv.plasticoRefinado= gv.plasticoRefinado + (10*plasticoPartida);
            gv.cartonRefinado= gv.cartonRefinado + (10*cartonPartida);
            gv.metalRefinado= gv.metalRefinado + (10*metalPartida);
            gv.compostRefinado= gv.compostRefinado + (10*compostPartida); 
            saveSystem.Save();
            
        }
    }

    //vuelve a voltear la carta
    public IEnumerator RotateBack()
    {
        coroutineAllowed =false; 
        colisionAux.enabled = !colisionAux.enabled;
        Grilla.vidas = Grilla.vidas -1;  
        Debug.Log("Vidas:"+Grilla.vidas);  
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
           
   
}
