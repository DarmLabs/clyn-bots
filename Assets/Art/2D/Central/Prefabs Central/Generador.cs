using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generador : MonoBehaviour
{
    [SerializeField] private GameObject[] Recuperables;
    [SerializeField] private GameObject[] NoRecuperables;
    [SerializeField] private GameObject[] Organicos;
    [SerializeField] private GameObject[] Residuos;

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private GameObject Tachos;

    private int indexRecuperables = 11;
    private int indexNoRecuperables = 5;
    private int indexOrganicos = 5;

    private int cantidadResiduos = 0;

    private int cantidadRecuperables = 14;
    private int cantidadNoRecuperables = 6;
    private int cantidadOrganicos = 10;
   
    public float timeSpawn = 1f;

    
    void Start () 
    {
        cantidadResiduos = cantidadNoRecuperables + cantidadOrganicos + cantidadRecuperables;
        for (int i = 0; i < cantidadRecuperables; i++)
        {
            Residuos[i] = Recuperables[Random.Range(0,indexRecuperables)];
        }
        for (int i = 0; i < indexNoRecuperables; i++)
        {
            Residuos[cantidadRecuperables+i]= NoRecuperables[Random.Range(0,indexNoRecuperables)];        
        }
        for (int i = 0; i < indexOrganicos; i++)
        {
            Residuos[cantidadRecuperables+cantidadNoRecuperables+i] = Organicos[Random.Range(0,indexOrganicos)];
        }
        for (int t = 0; t < Residuos.Length; t++)
        {
            GameObject temporal = Residuos[t]; 
            int r = Random.Range(t, Residuos.Length);
            Residuos[t] = Residuos[r];
            Residuos[r] = temporal;
        }
        for (int i = 0; i < Residuos.Length; i++)
        {
          Instantiate(Residuos[i],spawnPoint.position,transform.rotation); 
          //Invoke("invocarResiduos", timeSpawn);
        }              
    }

    void Update()
    {
        Controles();
    }

    void invocarResiduos ()
    {
        //Instantiate(Residuos[Indice],spawnPoint.position,transform.rotation); 
    }
    void Controles()
    {
        if(Input.GetKey(KeyCode.Keypad1) || Input.GetKey(KeyCode.Alpha1) )
        {
            Tachos.transform.GetChild(0).gameObject.SetActive(true);
            Tachos.transform.GetChild(1).gameObject.SetActive(false);
            Tachos.transform.GetChild(2).gameObject.SetActive(false);
        }
        if(Input.GetKey(KeyCode.Keypad2) || Input.GetKey(KeyCode.Alpha2) )
        {
            Tachos.transform.GetChild(0).gameObject.SetActive(false);
            Tachos.transform.GetChild(1).gameObject.SetActive(true);
            Tachos.transform.GetChild(2).gameObject.SetActive(false);
        }
        if(Input.GetKey(KeyCode.Keypad3) || Input.GetKey(KeyCode.Alpha3) )
        {
            Tachos.transform.GetChild(0).gameObject.SetActive(false);
            Tachos.transform.GetChild(1).gameObject.SetActive(false);
            Tachos.transform.GetChild(2).gameObject.SetActive(true);
        }
    }
    
   
   
}
