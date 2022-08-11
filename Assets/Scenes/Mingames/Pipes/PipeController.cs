using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    [SerializeField] private Transform[] Tubos;  
    [SerializeField] public static bool[] banderaTubo = new bool[25];  
    public static bool gano = false;
    public static int contadorCorrectas = 0;    
    [SerializeField] private Vector3[] ArrayRotaciones;   
    private int randomIndex = 0;
    
    void Start()
    {
        contadorCorrectas = 0;
        gano = false;        
        for (int i = 0; i < Tubos.Length; i++)
        {
            banderaTubo[i]=false; 
            randomIndex = Random.Range(0,3);
            Tubos[i].transform.Rotate(ArrayRotaciones[randomIndex]);
            //Debug.Log("pieza: "+Tubos[i].gameObject.name+"  array:"+ArrayRotaciones[randomIndex]);            
        }       
    }

   
    void Update()
    {
        Origenes();
        FlechasFeedback();
        //Debug.Log("Update RotatePipe.IndiceFlecha: "+RotatePipe.IndiceFlecha);          
        if (contadorCorrectas == 25)
        {
            gano = true;
            Debug.Log("GANASTE NIÑO BOBO");     
        }
        
    }

    void Origenes()
    {
        if (Tubos[0].rotation== Quaternion.identity && !banderaTubo[0]) //|| Tubos[0].rotation.z == 360 || Tubos[0].rotation.z == 0)//  
        {
            banderaTubo[0]=true;
            //Debug.Log("banderaTubo"+0+": "+banderaTubo[0]); 
        }        

        if (Tubos[1].rotation== Quaternion.identity  && !banderaTubo[1])
        {
            banderaTubo[1]=true;
            //Debug.Log("banderaTubo"+1+": "+banderaTubo[1]); 
        }

        if (Tubos[2].rotation== Quaternion.identity  && !banderaTubo[2])
        {
            banderaTubo[2]=true;
            //Debug.Log("banderaTubo"+2+": "+banderaTubo[2]); 
        }

        if (Tubos[3].rotation== Quaternion.identity  && !banderaTubo[3])
        {
            banderaTubo[3]=true;
            //Debug.Log("banderaTubo"+3+": "+banderaTubo[3]); 
        }

        if (Tubos[4].rotation== Quaternion.identity  && !banderaTubo[4])
        {
            banderaTubo[4]=true;
            //Debug.Log("banderaTubo"+4+": "+banderaTubo[4]); 
        }

        if (Tubos[5].rotation== Quaternion.identity  && !banderaTubo[5])
        {
            banderaTubo[5]=true;
            //Debug.Log("banderaTubo"+5+": "+banderaTubo[5]); 
        }

        if (Tubos[6].rotation== Quaternion.identity  && !banderaTubo[6])
        {
            banderaTubo[6]=true;
            //Debug.Log("banderaTubo"+6+": "+banderaTubo[6]); 
        }

        if (Tubos[7].rotation== Quaternion.identity  && !banderaTubo[7])
        {
            banderaTubo[7]=true;
            //Debug.Log("banderaTubo"+7+": "+banderaTubo[7]); 
        }

        if (Tubos[8].rotation== Quaternion.identity  && !banderaTubo[8])
        {
            banderaTubo[8]=true;
            //Debug.Log("banderaTubo"+8+": "+banderaTubo[8]); 
        }

        if (Tubos[9].rotation== Quaternion.identity  && !banderaTubo[9])
        {
            banderaTubo[9]=true;
            //Debug.Log("banderaTubo"+9+": "+banderaTubo[9]); 
        }

        if (Tubos[10].rotation== Quaternion.identity  && !banderaTubo[10])
        {
            banderaTubo[10]=true;
            //Debug.Log("banderaTubo"+10+": "+banderaTubo[10]); 
        }

        if (Tubos[11].rotation== Quaternion.identity  && !banderaTubo[11])
        {
            banderaTubo[11]=true;
            //Debug.Log("banderaTubo"+11+": "+banderaTubo[11]); 
        }

        if (Tubos[12].rotation== Quaternion.identity  && !banderaTubo[12])
        {
            banderaTubo[12]=true;
            //Debug.Log("banderaTubo"+12+": "+banderaTubo[12]); 
        }

        if (Tubos[13].rotation== Quaternion.identity  && !banderaTubo[13])
        {
            banderaTubo[13]=true;
            //Debug.Log("banderaTubo"+13+": "+banderaTubo[13]); 
        }
        if (Tubos[14].rotation== Quaternion.identity  && !banderaTubo[14])
        {
            banderaTubo[14]=true;
            //Debug.Log("banderaTubo"+14+": "+banderaTubo[14]); 
        }

        if (Tubos[15].rotation== Quaternion.identity  && !banderaTubo[15])
        {
            banderaTubo[15]=true;
            //Debug.Log("banderaTubo"+15+": "+banderaTubo[15]); 
        }
        if (Tubos[16].rotation== Quaternion.identity  && !banderaTubo[16])
        {
            banderaTubo[16]=true;
            //Debug.Log("banderaTubo"+16+": "+banderaTubo[16]); 
        }

        if (Tubos[17].rotation== Quaternion.identity  && !banderaTubo[17])
        {
            banderaTubo[17]=true;
            //Debug.Log("banderaTubo"+17+": "+banderaTubo[17]); 
        }

        if (Tubos[18].rotation== Quaternion.identity  && !banderaTubo[18])
        {
            banderaTubo[18]=true;
            //Debug.Log("banderaTubo"+18+": "+banderaTubo[18]); 
        }

        if (Tubos[19].rotation== Quaternion.identity  && !banderaTubo[19])
        {
            banderaTubo[19]=true;
            //Debug.Log("banderaTubo"+19+": "+banderaTubo[19]); 
        }

         if (Tubos[20].rotation== Quaternion.identity  && !banderaTubo[20])
        {
            banderaTubo[20]=true;
            //Debug.Log("banderaTubo"+20+": "+banderaTubo[20]); 
        }

        if (Tubos[21].rotation== Quaternion.identity  && !banderaTubo[21])
        {
            banderaTubo[21]=true;
            //Debug.Log("banderaTubo"+21+": "+banderaTubo[21]); 
        }

        if (Tubos[22].rotation== Quaternion.identity  && !banderaTubo[22])
        {
            banderaTubo[22]=true;
            //Debug.Log("banderaTubo"+22+": "+banderaTubo[22]); 
        }

        if (Tubos[23].rotation== Quaternion.identity  && !banderaTubo[23])
        {
            banderaTubo[23]=true;
            //Debug.Log("banderaTubo"+23+": "+banderaTubo[23]); 
        }

        if (Tubos[24].rotation== Quaternion.identity  && !banderaTubo[24])
        {
            banderaTubo[24]=true;
            //Debug.Log("banderaTubo"+24+": "+banderaTubo[24]); 
        }

        
        
        
    }

    void FlechasFeedback()
    {
        Tubos[RotatePipe.IndiceFlecha].GetChild(0).gameObject.SetActive(true);
        Tubos[RotatePipe.IndiceFlecha].GetChild(1).gameObject.SetActive(true); 
             
    }
}
