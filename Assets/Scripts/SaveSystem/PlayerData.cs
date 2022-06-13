using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int noRecTrash, organicTrash, recTrash;

    public void Data(globalVariables globalVariables)
    {
        noRecTrash = globalVariables.noRecTrash;
        organicTrash = globalVariables.organicTrash;
        recTrash = globalVariables.recTrash;
    }
}
