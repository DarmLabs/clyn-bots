using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    #region Imports & Required Objects
    [Header("Imports & Required Objects")]
    public SaveLoadSystem saveSystem;
    public GlobalVariables globalVariables;
    public PlayerAnimations playerAnim;
    public PlayerMovement playerMovement;
    public Player_UI player_UI;
    public General_UI general_UI;
    public GameObject BasuralPoint, LobbyPointB, LobbyPointGZ, GreenZonePoint;
    public GameObject basural, central, greenZone;
    public GameObject currentTrashPile;
    public GameObject targetConstruction;
    #endregion
    void Start()
    {
        playerAnim = GetComponent<PlayerAnimations>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    public void OnPause()
    {
        Time.timeScale = 0;
    }
    public void OnResume()
    {
        Time.timeScale = 1;
    }
    public void SaveTransform()
    {
        GetComponent<SavePosition>().PositionUpdated();
        GetComponent<SavePosition>().RotationUpdated();
    }
}
