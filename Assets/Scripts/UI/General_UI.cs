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
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject pipesMinigame;
    [SerializeField] GameObject mainMissionPanel;
    [SerializeField] GameObject constructionPanel;
    [SerializeField] GameObject orchardPanel;
    [SerializeField] GameObject tutorialPanel;
    [SerializeField] GameObject refinerPanel;
    [SerializeField] GameObject compostPanel;
    [SerializeField] TextMeshProUGUI orchardTitle, reqCompostOrchard;
    public TextMeshProUGUI debugText;
    int selection = 1;
    [SerializeField] GameObject minimap;
    [SerializeField] GameObject fullMap;
    public GameObject constructonRender, orchardRender;
    public Sprite Panel, Molino, Bomba, Trigo, Zanahoria, Tomate;
    public GameObject exitPanel;
    public TextMeshProUGUI constructionTitle, reqVidrio, reqPlastico, reqCompost, reqMetal, reqCarton;
    public GameObject constructionBtn;
    public Color32 lockColor;
    public Color32 unlockColor;
    public GameObject minigameAspire;
    public GameObject interactionCloud;
    [SerializeField] GameObject changeStagePanel;
    [SerializeField] TextMeshProUGUI textChangeStage;
    [SerializeField] TextMeshProUGUI[] mainMissionTexts;
    [SerializeField] Slider[] mainMissionProgressBars;
    MainMission mainMission;
    void Start()
    {
        saveSystem = playerInteraction.saveSystem;
        mainMission = playerInteraction.mainMission;
    }
    public void MainPanelSwitcher(bool state)
    {
        mainPanel.SetActive(state);
    }
    public void MainMenuSwitcher(bool state)
    {
        mainMenuPanel.SetActive(state);
    }
    public void StartPanelSwitcher(bool state)
    {
        startPanel.SetActive(state);
    }
    public void MainMissionSwitcher(bool state)
    {
        mainMissionPanel.SetActive(state);
        if (state)
        {
            CheckMainMissionStatus();
        }
    }
    public void CheckMainMissionStatus()
    {
        mainMissionTexts[0].text = mainMission.trashRecolected + "/" + mainMission.maxTrash;
        mainMissionTexts[1].text = mainMission.constructionsFinished + "/" + mainMission.maxConstructions;
        mainMissionTexts[2].text = mainMission.maintainancePlayed + "/" + mainMission.maxMaintainance;
        mainMissionTexts[3].text = mainMission.lakesCleaned + "/" + mainMission.maxLakes;
        mainMissionTexts[4].text = mainMission.cropsGrew + "/" + mainMission.maxCrops;
        RefreshProgressBars();
    }
    void RefreshProgressBars()
    {
        mainMissionProgressBars[0].value = Mathf.Round((mainMission.trashRecolected * 100) / mainMission.maxTrash);
        mainMissionProgressBars[1].value = Mathf.Round((mainMission.constructionsFinished * 100) / mainMission.maxConstructions);
        mainMissionProgressBars[2].value = Mathf.Round((mainMission.maintainancePlayed * 100) / mainMission.maxMaintainance);
        mainMissionProgressBars[3].value = Mathf.Round((mainMission.lakesCleaned * 100) / mainMission.maxLakes);
        mainMissionProgressBars[4].value = Mathf.Round((mainMission.cropsGrew * 100) / mainMission.maxCrops);
    }
    public void TutorialPanelSwithcer(bool state)
    {
        tutorialPanel.SetActive(state);
    }
    public void RefinerPanelSwitcher(bool state)
    {
        refinerPanel.SetActive(state);
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
    public void OrchardPanelSwitcher(bool state)
    {
        orchardPanel.SetActive(state);
    }
    public void OrchardSelection(GameObject targetBtn)
    {
        if (targetBtn.name == "Derecha")
        {
            selection++;
        }
        else
        {
            selection--;
        }
        if (selection <= 0)
        {
            selection = 2;
        }
        if (selection >= 3)
        {
            selection = 1;
        }
        Orchard orchard = playerInteraction.targetOrchard.GetComponent<Orchard>();
        switch (selection)
        {
            case 1:
                orchard.seedType = "Tomate";
                break;
            case 2:
                orchard.seedType = "Zanahoria";
                break;
        }
        orchardTitle.text = "Plantando " + orchard.seedType;
        RenderConstruction(orchard.seedType, orchardRender);
    }
    public void ConstructionPanelSwitcher(bool state)
    {
        constructionPanel.SetActive(state);
    }
    public void BuildingConstructionMenu(string title, string[] req, string reqSprite, bool isOrchard)
    {
        if (isOrchard)
        {
            orchardTitle.text = title;
            reqCompostOrchard.text = req[4];
            RenderConstruction(reqSprite, orchardRender);
        }
        else
        {
            constructionTitle.text = title;
            reqVidrio.text = req[0];
            reqPlastico.text = req[1];
            reqCarton.text = req[2];
            reqMetal.text = req[3];
            reqCompost.text = req[4];
            RenderConstruction(reqSprite, constructonRender);
        }
    }
    public void RenderConstruction(string reqSprite, GameObject render)
    {
        bool found = false;
        switch (reqSprite)
        {
            case "Panel Solar":
                render.GetComponent<Image>().sprite = Panel;
                found = true;
                break;
            case "Molino":
                render.GetComponent<Image>().sprite = Molino;
                found = true;
                break;
            case "Bomba de Agua":
                render.GetComponent<Image>().sprite = Bomba;
                found = true;
                break;
            case "Trigo":
                render.GetComponent<Image>().sprite = Trigo;
                found = true;
                break;
            case "Zanahoria":
                render.GetComponent<Image>().sprite = Zanahoria;
                found = true;
                break;
            case "Tomate":
                render.GetComponent<Image>().sprite = Tomate;
                found = true;
                break;
        }
        if (!found)
        {
            constructonRender.GetComponent<Image>().sprite = null;
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

    public void PipesMinigameSwitcher(bool state)
    {
        pipesMinigame.SetActive(state);
    }
    public void CompostPanelSwitcher(bool state)
    {
        compostPanel.SetActive(state);
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
    public void FullMapSwitcher(bool state)
    {
        fullMap.SetActive(state);
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
        if (playerInteraction.takeBools != null)
        {
            playerInteraction.takeBools.TakeDestoyed();
            FileHandler.SaveToJSON<bool>(playerInteraction.takeBools.destoyedList, "save.txt");
        }
        playerInteraction.SaveTransform();
        saveSystem.Save();
        SceneManager.LoadScene(scene);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
