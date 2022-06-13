using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalVariables : MonoBehaviour
{
    [SerializeField]
    public int noRecTrash, organicTrash, recTrash;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        LoadPlayer();
    }
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        noRecTrash = data.noRecTrash;
        organicTrash = data.organicTrash;
        recTrash = data.recTrash;
    }
}
