using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class InitialSceneManager : MonoBehaviour
{
    [SerializeField] GameObject logos;
    [SerializeField] GameObject loadStartGamePanel;
    [SerializeField] GameObject confirmationBox;
    [SerializeField] GameObject globalVariables;
    [SerializeField] GameObject missionController;
    [SerializeField] AudioManager audioManager;
    [HideInInspector] public float fadeTime;
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0))
        {
            SkipLogos();
        }
    }
    public void ShowNewLogo(GameObject nextLogo)
    {
        if (nextLogo == null)
        {
            if (File.Exists(Application.persistentDataPath + "/save.txt"))
            {
                loadStartGamePanel.SetActive(true);
            }
            else
            {
                StartSaveGameObjects();
                SceneManager.LoadScene("Inside");
            }
        }
        else
        {
            nextLogo.SetActive(true);
        }
    }
    public void FadeIn(GameObject logo)
    {
        Image logoImage = logo.GetComponent<Image>();
        fadeTime += 0.01f;
        logoImage.color = new Color(logoImage.color.r, logoImage.color.g, logoImage.color.b, fadeTime);
    }
    public void FadeOut(GameObject logo)
    {
        Image logoImage = logo.GetComponent<Image>();
        fadeTime -= 0.01f;
        logoImage.color = new Color(logoImage.color.r, logoImage.color.g, logoImage.color.b, fadeTime);
    }
    public void CheckConfirmation()
    {
        loadStartGamePanel.SetActive(false);
        confirmationBox.SetActive(true);
    }
    public void CanelNewGame()
    {
        loadStartGamePanel.SetActive(true);
        confirmationBox.SetActive(false);
    }
    public void NewGame()
    {
        File.Delete(Application.persistentDataPath + "/save.txt");
        File.Delete(Application.persistentDataPath + "/saveTrash.txt");
        StartSaveGameObjects();
        SceneManager.LoadScene("Inside");
    }
    public void ContinueGame()
    {
        StartSaveGameObjects();
        SceneManager.LoadScene("Inside");
    }
    public void StartSaveGameObjects()
    {
        globalVariables.SetActive(true);
        missionController.SetActive(true);
        audioManager.CheckAudio();
    }
    void SkipLogos()
    {
        if (logos.activeSelf)
        {
            logos.SetActive(false);
            if (File.Exists(Application.persistentDataPath + "/save.txt"))
            {
                loadStartGamePanel.SetActive(true);
            }
            else
            {
                StartSaveGameObjects();
                SceneManager.LoadScene("Inside");
            }
        }
    }
}
