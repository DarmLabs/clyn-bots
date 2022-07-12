using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grilla : MonoBehaviour
{
    [SerializeField] private GameObject[] UbicacionCartas; //18 cartas 9 distintas
    [SerializeField] private GameObject[] Cartas; //16 distintas
    [SerializeField] private GameObject[] CartasGrilla; //18 cartas 9 distintas   
    //private int indexUbicacionCartas = 17;
    //private int indexCartas = 0;   
    private GameObject globalaux;
    private GlobalVariables gv; 

    public static int vidas = 20;
    public static int refinadosDestruidos = 0;

    private int randomIndex;
    private int cantidadRandoms = 10;
    List<int> listaRandoms = new List<int>();   

    void Start()
    {
        //globalaux = GameObject.Find("GlobalVariables");
        //gv = globalaux.GetComponent<GlobalVariables>();
        //if (gv.cardDistribution)  
        vidas = 20;
        refinadosDestruidos = 0;      
        listaRandoms = new List<int>(new int[cantidadRandoms]); 
        for (int i = 1; i < cantidadRandoms; i++)
        {
            randomIndex = Random.Range(0,16);           
            while (listaRandoms.Contains(randomIndex))
            {
                randomIndex = Random.Range(0,16);
            } 
            listaRandoms[i] = randomIndex;
            //print(listaRandoms[i]);            
        }        
        for (int i = 0; i < 9; i++)
        {
            CartasGrilla[i] = Cartas[listaRandoms[i+1]];                   
            CartasGrilla[i+9] = Cartas[listaRandoms[i+1]];         
        }
        for (int t = 0; t < CartasGrilla.Length; t++)
        {
            GameObject temporal = CartasGrilla[t]; 
            int r = Random.Range(t, CartasGrilla.Length);
            CartasGrilla[t] = CartasGrilla[r];
            CartasGrilla[r] = temporal;
            //Debug.Log("TOTAL ARRAY"+CartasGrilla.Length); 
        }
        for (int i = 0; i < UbicacionCartas.Length; i++)
        {
            Instantiate(CartasGrilla[i],UbicacionCartas[i].transform.position,transform.rotation); 
        }
            
        
    }

    
    void Update()
    {
        
    }
}
