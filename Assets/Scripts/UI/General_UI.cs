using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class General_UI : MonoBehaviour
{
    public GameObject miniGamePanel;

    public void MinigamePanelSwitcher(bool state){
        miniGamePanel.SetActive(state);
    }

    public void ChangeScene(string scene){
        SceneManager.LoadScene(scene);
    }
}
