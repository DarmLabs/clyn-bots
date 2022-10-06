using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SaveSlotData : MonoBehaviour, ISaveable
{
    public string slotName1, slotName2, slotName3;
    public object SaveState()
    {
        return new SaveData()
        {
            slotName1 = this.slotName1,
            slotName2 = this.slotName2,
            slotName3 = this.slotName3
        };
    }
    public void LoadState(object state)
    {
        var saveData = (SaveData)state;
        slotName1 = saveData.slotName1;
        slotName2 = saveData.slotName2;
        slotName3 = saveData.slotName3;
    }
    [Serializable]
    private struct SaveData
    {
        public string slotName1, slotName2, slotName3;
    }
}
