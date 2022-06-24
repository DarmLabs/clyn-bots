using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards : MonoBehaviour
{
    public bool facedUp, locked;
    public static bool coroutineAllowed;
    private Cards firstInPair, secondInPair;
    private string firstInPairName, secondInPairName;

    public static Queue<Cards> sequence;
    public static int pairsFound;
    public int vidas = 20;
    
    

    void Start()
    {    
        //se inicializan las banderas
        vidas = 20;
        Debug.Log("VIDAS INCIA"+vidas);
        facedUp=false;
        coroutineAllowed=true;
        locked=false;
        sequence=new Queue<Cards>();
        pairsFound=0;

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
                for (float i = 0f; i < 190f; i+=10)
                {
                    transform.rotation = Quaternion.Euler(0f,i,0f);
                    yield return new WaitForSeconds(0f);
                }
                Debug.Log("!facedUP");               
                               
            }
            else if (facedUp)
            {
                for (float i = 190f; i >= 0f; i-=10)
                {
                    transform.rotation=Quaternion.Euler(0f,i,0f);
                    yield return new WaitForSeconds(0f);
                    sequence.Clear();
                }
                Debug.Log("facedUP");
                                
            }
            coroutineAllowed=true;
        
            facedUp=!facedUp;
            Debug.Log("timescale=0");

            if(sequence.Count ==1)
            {
                Debug.Log("UNA CARTA VOLTEADA");
                vidas = vidas - 1;
                Debug.Log("vidas: "+vidas);
            }

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

        firstInPairName= firstInPair.name.Substring(0,firstInPair.name.Length-5);
        secondInPairName= secondInPair.name.Substring(0, secondInPair.name.Length-5);
        
        if (firstInPairName==secondInPairName)
        {
            firstInPair.locked=true;
            secondInPair.locked=true;
            pairsFound+=1;
            vidas = vidas + 1;        
            //verificar tags
            Debug.Log("TAG para refinado de:  "+firstInPair.tag);
            Debug.Log("vidas dentro tag: "+vidas);
        }
        else
        {
            firstInPair.StartCoroutine("RotateBack");
            secondInPair.StartCoroutine("RotateBack");
            vidas = vidas -1;
        }

        if (pairsFound == 9)
        {
            //terminó el juego
            Debug.Log("GANÓ ");
        }
    }

    //vuelve a voltear la carta
    public IEnumerator RotateBack()
    {
        coroutineAllowed =false;
        yield return new WaitForSeconds(0.2f);
        for (float i=190f; i>=0f; i-=10)
        {
            transform.rotation =Quaternion.Euler(0f,i,0f);
            yield return new WaitForSeconds(0f);
            sequence.Clear();            
        }
        facedUp=false;
        coroutineAllowed=true;
    }

    
    void Update()
    {
        
    }
}
