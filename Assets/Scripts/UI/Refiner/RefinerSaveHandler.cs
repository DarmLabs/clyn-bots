using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefinerSaveHandler : MonoBehaviour
{
    [SerializeField] FilterManager filterManager;

    public void ApplyValuesToGv()
    {
        //Resto residuos
        filterManager.gv.divisionVidrio -= filterManager.vidrioValue * 10;
        filterManager.gv.divisionCarton -= filterManager.cartonValue * 10;
        filterManager.gv.divisionPlastico -= filterManager.plasticoValue * 10;
        filterManager.gv.divisionMetal -= filterManager.metalValue * 10;

        //Añado refinados
        filterManager.gv.vidrioRefinado += filterManager.vidrioValue;
        filterManager.gv.cartonRefinado += filterManager.cartonValue;
        filterManager.gv.plasticoRefinado += filterManager.plasticoValue;
        filterManager.gv.metalRefinado += filterManager.metalValue;

        //Reseteo Valores
        filterManager.ResetValues();
    }
}
