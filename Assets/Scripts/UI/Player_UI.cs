using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
{
    #region Imports & Required Objects
    GlobalVariables globalVariables;
    public GameObject bag;
    [SerializeField] TextMeshProUGUI trashText;
    [SerializeField] GameObject greenButton;
    [SerializeField] Slider aspireProgress;
    public TextMeshProUGUI vidrioRefText, plasticoRefText, compostText, cartonRefText, metalRefText;
    public PlayerInteraction playerInteraction;
    #endregion
    void Start()
    {
        globalVariables = GameObject.FindObjectOfType<GlobalVariables>().GetComponent<GlobalVariables>();
    }

    void LateUpdate()
    {
        DisplayRefTrash();
    }
    public void DisplayBagPercentage()
    {
        trashText.text = playerInteraction.bagPercentage + " %";
        aspireProgress.value = playerInteraction.bagPercentage;
        if (playerInteraction.bagPercentage == 100)
        {
            greenButton.SetActive(true);
            playerInteraction.audioManager.PlayAudio("Max_Bag");
            playerInteraction.missionTrack.MaxBagMission();
        }
        else
        {
            greenButton.SetActive(false);
        }
    }
    void DisplayRefTrash()
    {
        vidrioRefText.text = globalVariables.vidrioRefinado.ToString();
        plasticoRefText.text = globalVariables.plasticoRefinado.ToString();
        compostText.text = globalVariables.compostRefinado.ToString();
        cartonRefText.text = globalVariables.cartonRefinado.ToString();
        metalRefText.text = globalVariables.metalRefinado.ToString();
    }
}
