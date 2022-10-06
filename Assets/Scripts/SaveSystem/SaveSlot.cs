using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;
using System;
public class SaveSlot : MonoBehaviour
{
    [SerializeField] SaveLoadSystem saveLoadSystem;
    [SerializeField] GameObject[] saveSlots;
    [SerializeField] InitialSceneManager initManager;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] SaveSlotData saveSlotData;
    void Start()
    {
        CheckSlots();
    }
    public void SetSaveSlot(int slotNumber)
    {
        saveLoadSystem.SetSaveSlot(slotNumber);
    }
    void CheckSlots()
    {
        for (int i = 0; i < 3; i++)
        {
            TextMeshProUGUI slotText = saveSlots[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            Button slotButton = saveSlots[i].GetComponent<Button>();
            slotButton.onClick.RemoveAllListeners();
            string slotName = "";
            switch (i)
            {
                case 0:
                    slotName = saveSlotData.slotName1;
                    break;
                case 1:
                    slotName = saveSlotData.slotName2;
                    break;
                case 2:
                    slotName = saveSlotData.slotName3;
                    break;
            }
            if (File.Exists(Application.persistentDataPath + "/save" + (i + 1) + ".txt") && File.Exists(Application.persistentDataPath + "/slotsNames.txt"))
            {
                slotText.text = "Partida guardada de: " + slotName;
                slotButton.onClick.AddListener(delegate { initManager.LoadNewContinuePanel(true); });
            }
            else
            {
                slotText.text = "Guardado vacío, presiona para crear un nuevo guardado.";
                slotButton.onClick.AddListener(delegate { initManager.InputPanelField(true); });
            }
        }
    }
    public void SetSaveName()
    {
        switch (saveLoadSystem.saveSlot)
        {
            case 1:
                saveSlotData.slotName1 = inputField.text;
                break;
            case 2:
                saveSlotData.slotName2 = inputField.text;
                break;
            case 3:
                saveSlotData.slotName3 = inputField.text;
                break;
        }
        saveLoadSystem.SaveName();
        saveLoadSystem.LoadWithPath();
        initManager.StartScene();
    }
}
