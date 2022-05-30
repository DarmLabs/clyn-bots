using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_UI : MonoBehaviour
{
    public GameObject Player;
    PlayerInteraction playerInteractionScript;
    public TextMeshProUGUI trashText;
    void Start()
    {
        playerInteractionScript = Player.GetComponent<PlayerInteraction>();
    }

    void LateUpdate()
    {
        trashText.text = "Organicos "+ playerInteractionScript.organicTrash + "\nRecuperables "+ playerInteractionScript.recTrash + "\nNo Recuperables " + playerInteractionScript.noRecTrash;                    
    }
}
