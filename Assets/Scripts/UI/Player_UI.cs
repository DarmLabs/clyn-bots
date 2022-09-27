using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player_UI : MonoBehaviour
{
    #region Imports & Required Objects
    GlobalVariables globalVariables;
    [SerializeField] TextMeshProUGUI trashText;
    [SerializeField] GameObject greenButton;
    [SerializeField] Slider aspireProgress;
    [SerializeField] GameObject refMarco;
    [SerializeField] GameObject divMarco;
    public TextMeshProUGUI vidrioRefText, plasticoRefText, compostText, cartonRefText, metalRefText;
    [SerializeField] TextMeshProUGUI vidrioDivText, plasticoDivText, compostDivText, cartonDivText, metalDivText;
    public PlayerInteraction playerInteraction;
    #endregion
    void Start()
    {
        globalVariables = GameObject.FindObjectOfType<GlobalVariables>().GetComponent<GlobalVariables>();
    }
    void LateUpdate()
    {
        DisplayRefTrash();
        DisplayDivTrash();
    }
    public void DisplayBagPercentage(bool fromSceneChange)
    {
        trashText.text = playerInteraction.bagPercentage + " %";
        aspireProgress.value = playerInteraction.bagPercentage;
        if (playerInteraction.bagPercentage == 100 && !fromSceneChange)
        {
            playerInteraction.audioManager.PlayAudio("Max_Bag");
            greenButton.SetActive(true);
            playerInteraction.missionTrack.MaxBagMission();
        }
        else if (playerInteraction.bagPercentage == 100)
        {
            greenButton.SetActive(true);
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
    void DisplayDivTrash()
    {
        vidrioDivText.text = globalVariables.divisionVidrio.ToString();
        plasticoDivText.text = globalVariables.divisionPlastico.ToString();
        compostDivText.text = globalVariables.divisionOrganic.ToString();
        cartonDivText.text = globalVariables.divisionCarton.ToString();
        metalDivText.text = globalVariables.divisionMetal.ToString();
    }
    public void SwitchMarcos(bool state)
    {
        if (state)
        {
            divMarco.SetActive(true);
            refMarco.SetActive(false);
        }
        else
        {
            refMarco.SetActive(true);
            divMarco.SetActive(false);
        }
    }
}
