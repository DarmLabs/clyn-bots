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
    [SerializeField] SaveLoadSystem saveLoadSystem;
    [SerializeField] GameObject saveSlots;
    [SerializeField] GameObject panelField;
    [SerializeField] AudioManager audioManager;
    [HideInInspector] public float fadeTime;
    [SerializeField] Button confirmButton;
    [SerializeField] Button cancelButton;
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
            saveSlots.SetActive(true);
            saveLoadSystem.LoadNames();
        }
        else
        {
            nextLogo.SetActive(true);
        }
    }
    public void LoadNewContinuePanel(bool state)
    {
        saveSlots.SetActive(!state);
        loadStartGamePanel.SetActive(state);
    }
    public void InputPanelField(bool state)
    {
        saveSlots.SetActive(!state);
        panelField.SetActive(state);
        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(delegate { InputPanelField(false); });
    }
    public void StartScene()
    {
        StartSaveGameObjects();
        SceneManager.LoadScene("Inside");
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
        confirmationBox.SetActive(false);
        panelField.SetActive(true);
        confirmButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
        confirmButton.onClick.AddListener(NewGameInitScene);
        cancelButton.onClick.AddListener(delegate { panelField.SetActive(false); });
        cancelButton.onClick.AddListener(delegate { loadStartGamePanel.SetActive(true); });
    }
    public void ContinueGame()
    {
        StartSaveGameObjects();
        SceneManager.LoadScene("Inside");
    }
    void NewGameInitScene()
    {
        File.Delete(Application.persistentDataPath + "/save" + saveLoadSystem.saveSlot + ".txt");
        File.Delete(Application.persistentDataPath + "/saveTrash" + saveLoadSystem.saveSlot + ".txt");
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
            saveSlots.SetActive(true);
            saveSlots.transform.parent.gameObject.SetActive(true);
            saveLoadSystem.LoadNames();
        }
    }
}
