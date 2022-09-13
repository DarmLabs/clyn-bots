using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class InitialSceneManager : MonoBehaviour
{
    [SerializeField] GameObject loadStartGamePanel;
    [SerializeField] GameObject confirmationBox;
    [HideInInspector] public float fadeTime;
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
        SceneManager.LoadScene("Inside");
    }
    public void ContinueGame()
    {
        SceneManager.LoadScene("Inside");
    }
}
