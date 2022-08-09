using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    [SerializeField] private Transform[] Tubos;    
    public static bool gano = false;
    
    void Start()
    {
        gano = false;
    }

   
    void Update()
    {
        if (Tubos[0].rotation.z == 0 && Tubos[1].rotation.z == 0 && Tubos[2].rotation.z == 0 && Tubos[3].rotation.z == 0 && Tubos[4].rotation.z == 0 &&
        Tubos[5].rotation.z == 0 && Tubos[9].rotation.z == 0 && Tubos[13].rotation.z == 0 && Tubos[17].rotation.z == 0 && Tubos[21].rotation.z == 0 &&
        Tubos[6].rotation.z == 0 && Tubos[10].rotation.z == 0 && Tubos[14].rotation.z == 0 && Tubos[18].rotation.z == 0 && Tubos[22].rotation.z == 0 &&
        Tubos[7].rotation.z == 0 && Tubos[11].rotation.z == 0 && Tubos[15].rotation.z == 0 && Tubos[19].rotation.z == 0 && Tubos[23].rotation.z == 0 &&
        Tubos[8].rotation.z == 0 && Tubos[12].rotation.z == 0 && Tubos[16].rotation.z == 0 && Tubos[20].rotation.z == 0 && Tubos[24].rotation.z == 0)
        {
            gano = true;
            Debug.Log("GANASTE NIÑO BOBO");            
        }
    }
}
