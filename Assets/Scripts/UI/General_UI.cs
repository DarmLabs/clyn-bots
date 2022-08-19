using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class General_UI : MonoBehaviour
{
    public PlayerInteraction playerInteraction;
    public SaveLoadSystem saveSystem;
    public GameObject mainPanel;
    [SerializeField] GameObject centralMinigame;
    [SerializeField] GameObject memoryMinigame;
    [SerializeField] GameObject compostMinigame;
    [SerializeField] GameObject pipesMinigame;
    [SerializeField] GameObject mainMissionPanel;
    public GameObject constructionPanel;
    public GameObject minimap;
    public GameObject constructonRender;
    public Sprite Panel, Molino, Bomba, Trigo, Zanahoria, Tomate;
    public GameObject exitPanel;
    public TextMeshProUGUI constructionTitle, reqVidrio, reqPlastico, reqCompost, reqMetal, reqCarton;
    public GameObject constructionBtn;
    public GameObject upgradeBtn;
    public GameObject seedSection;
    public GameObject[] plantacionBtns = new GameObject[3];
    public Color32 lockColor;
    public Color32 unlockColor;
    public GameObject minigameAspire;
    public GameObject interactionCloud;
    [SerializeField] GameObject changeStagePanel;
    [SerializeField] TextMeshProUGUI textChangeStage;
    void Start()
    {
        saveSystem = playerInteraction.saveSystem;
    }
    public void MainPanelSwitcher(bool state)
    {
        mainPanel.SetActive(state);
    }
    public void MainMissionSwitcher(bool state)
    {
        mainMissionPanel.SetActive(state);
    }
    public void BuildChangeStagePanel(string target)
    {
        playerInteraction.MovmentState(false);
        playerInteraction.inDoor = target;
        if (target == "Outside")
        {
            textChangeStage.text = "¿Quieres salir de la central?";
        }
        else
        {
            textChangeStage.text = "¿Quieres entrar a la central?";
        }
    }
    public void ChangeStageSwitcher(bool state)
    {
        changeStagePanel.SetActive(state);
    }
    public void ConstructionPanelSwitcher(bool state)
    {
        constructionPanel.SetActive(state);
    }
    public void BuildingConstructionMenu(string title, string[] req, string reqSprite)
    {
        constructionTitle.text = title;

        reqVidrio.text = req[0];
        reqPlastico.text = req[1];
        reqCarton.text = req[2];
        reqMetal.text = req[3];
        reqCompost.text = req[4];

        bool found = false;
        switch (reqSprite)
        {
            case "Panel Solar":
                constructonRender.GetComponent<Image>().sprite = Panel;
                found = true;
                break;
            case "Molino":
                constructonRender.GetComponent<Image>().sprite = Molino;
                found = true;
                break;
            case "Bomba de Agua":
                constructonRender.GetComponent<Image>().sprite = Bomba;
                found = true;
                break;
            case "Trigo":
                constructonRender.GetComponent<Image>().sprite = Trigo;
                found = true;
                break;
            case "Zanahoria":
                constructonRender.GetComponent<Image>().sprite = Zanahoria;
                found = true;
                break;
            case "Tomate":
                constructonRender.GetComponent<Image>().sprite = Tomate;
                found = true;
                break;
        }
        if (!found)
        {
            constructonRender.GetComponent<Image>().sprite = null;
        }
    }
    public void EnabledSection(string name)
    {
        switch (name)
        {
            case "Construir":
                constructionBtn.SetActive(true);
                seedSection.SetActive(false);
                upgradeBtn.SetActive(false);
                break;
            case "Sembrar":
                seedSection.SetActive(true);
                constructionBtn.SetActive(false);
                upgradeBtn.SetActive(false);
                break;
            case "Mejorar":
                seedSection.SetActive(false);
                upgradeBtn.SetActive(true);
                constructionBtn.SetActive(false);
                break;
        }
    }
    public void ConstructionButtonState(bool state)
    {
        constructionBtn.GetComponent<Button>().enabled = state;
        if (!state)
        {
            constructionBtn.GetComponent<Button>().image.color = lockColor;
        }
        else
        {
            constructionBtn.GetComponent<Button>().image.color = unlockColor;
        }
    }
    public void SeedButtonsState(bool state)
    {
        if (!state)
        {
            foreach (var button in plantacionBtns)
            {
                button.GetComponent<Button>().enabled = state;
                button.GetComponent<Button>().image.color = lockColor;
            }
        }
        else
        {
            int i = 0;
            foreach (var button in plantacionBtns)
            {
                button.GetComponent<Button>().onClick.RemoveAllListeners();
                button.GetComponent<Button>().enabled = state;
                button.GetComponent<Button>().image.color = unlockColor;
                if (playerInteraction.targetConstruction.GetComponent<Seed>() != null)
                {
                    switch (i)
                    {
                        case 0:
                            button.GetComponent<Button>().onClick.AddListener(() => { playerInteraction.targetConstruction.GetComponent<Seed>().ChooseSeed(0); });
                            break;
                        case 1:
                            button.GetComponent<Button>().onClick.AddListener(() => { playerInteraction.targetConstruction.GetComponent<Seed>().ChooseSeed(1); });
                            break;
                        case 2:
                            button.GetComponent<Button>().onClick.AddListener(() => { playerInteraction.targetConstruction.GetComponent<Seed>().ChooseSeed(2); });
                            break;
                    }
                    button.GetComponent<Button>().onClick.AddListener(delegate { playerInteraction.targetConstruction.GetComponent<Seed>().PlaceSeed(); });
                }
                i += 1;
            }
        }
    }
    public void UpgradeButtonState(bool state)
    {
        upgradeBtn.GetComponent<Button>().enabled = state;
        upgradeBtn.GetComponent<Button>().onClick.RemoveAllListeners();
        upgradeBtn.GetComponent<Button>().onClick.AddListener(delegate { playerInteraction.targetConstruction.GetComponent<Seed>().GrowSeed(); });
        if (!state)
        {
            upgradeBtn.GetComponent<Button>().image.color = lockColor;
        }
        else
        {
            upgradeBtn.GetComponent<Button>().image.color = unlockColor;
        }
    }
    public void PipesMinigameSwitcher(bool state)
    {
        pipesMinigame.SetActive(state);
    }
    public void CompostMinigameSwitcher(bool state)
    {
        compostMinigame.SetActive(state);
    }
    public void CentralMinigameSwitcher(bool state)
    {
        centralMinigame.SetActive(state);
    }
    public void MemoryMinigameSwitcher(bool state)
    {
        memoryMinigame.SetActive(state);
    }
    public void MinigameAspireSwitcher(bool state)
    {
        minigameAspire.SetActive(state);
        if (state)
        {
            MainPanelSwitcher(false);
            playerInteraction.BagPercentage();
            playerInteraction.enabled = false;
        }
        else
        {
            MainPanelSwitcher(true);
            playerInteraction.enabled = true;
            playerInteraction.playerAnim.Aspire(false);
        }
    }
    public void MinimapSwitcher(bool state)
    {
        minimap.SetActive(state);
    }
    public void ExitPanelSwitcher(bool state)
    {
        exitPanel.SetActive(state);
    }
    public void InteractionCloud(bool state)
    {
        interactionCloud.SetActive(state);
    }
    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
