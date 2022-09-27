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
    [SerializeField] GameObject loadingScreen;
    [SerializeField] TextMeshProUGUI orchardTitle, reqCompostOrchard;
    public TextMeshProUGUI debugText;
    int selection = 1;
    [SerializeField] GameObject minimap;
    [SerializeField] GameObject fullMap;
    public GameObject constructonRender, orchardRender;
    public Sprite Panel, Molino, Bomba, Zanahoria, Tomate, Cebolla, Rabanito;
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
    [SerializeField] Cinemachine.CinemachineFreeLook freeLookCamera;
    MainMission mainMission;
    [SerializeField] GameObject tutoButton;
    void Start()
    {
        saveSystem = playerInteraction.saveSystem;
        mainMission = playerInteraction.mainMission;
        if (playerInteraction.inDoor == "Inside")
        {
            CheckTutoAvailable();
        }
    }
    public void CheckTutoAvailable()
    {
        if (playerInteraction.gv.firstTime)
        {
            tutoButton.SetActive(true);
        }
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
    public void LoadingScreenSwitcher(bool state)
    {
        loadingScreen.SetActive(state);
    }
    public void RefinerPanelSwitcher(bool state)
    {
        refinerPanel.SetActive(state);
        FreeLookCameraSwitcher(!state);
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
        FreeLookCameraSwitcher(!state);
    }
    public void OrchardPanelSwitcher(bool state)
    {
        orchardPanel.SetActive(state);
        FreeLookCameraSwitcher(!state);
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
            selection = 4;
        }
        if (selection >= 5)
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
            case 3:
                orchard.seedType = "Cebolla";
                break;
            case 4:
                orchard.seedType = "Rabanito";
                break;
        }
        orchardTitle.text = "Plantando " + orchard.seedType;
        RenderConstruction(orchard.seedType, orchardRender);
    }
    public void ConstructionPanelSwitcher(bool state)
    {
        constructionPanel.SetActive(state);
        FreeLookCameraSwitcher(!state);
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
            case "Zanahoria":
                render.GetComponent<Image>().sprite = Zanahoria;
                found = true;
                break;
            case "Tomate":
                render.GetComponent<Image>().sprite = Tomate;
                found = true;
                break;
            case "Cebolla":
                render.GetComponent<Image>().sprite = Cebolla;
                found = true;
                break;
            case "Rabanito":
                render.GetComponent<Image>().sprite = Rabanito;
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
        FreeLookCameraSwitcher(!state);
    }
    public void CompostPanelSwitcher(bool state)
    {
        compostPanel.SetActive(state);
        FreeLookCameraSwitcher(!state);
    }
    public void MinigameAspireSwitcher(bool state)
    {
        minigameAspire.SetActive(state);
        if (state)
        {
            MainPanelSwitcher(false);
            playerInteraction.BagPercentage(false);
            playerInteraction.enabled = false;
        }
        else
        {
            MainPanelSwitcher(true);
            playerInteraction.enabled = true;
            playerInteraction.playerAnim.Aspire(false);
        }
        FreeLookCameraSwitcher(!state);
    }
    public void MinimapSwitcher(bool state)
    {
        minimap.SetActive(state);
    }
    public void FullMapSwitcher(bool state)
    {
        fullMap.SetActive(state);
    }
    public void InteractionCloud(bool state)
    {
        interactionCloud.SetActive(state);
    }
    public void ChangeScene(string scene)
    {
        LoadingScreenSwitcher(true);
        playerInteraction.SaveTransform();
        saveSystem.Save();
        if (playerInteraction.takeBools != null)
        {
            playerInteraction.takeBools.TakeDestoyed();
            FileHandler.SaveToJSON<bool>(playerInteraction.takeBools.destoyedList, "saveTrash.txt");
        }
        StartCoroutine(LoadAsyncScene(scene));
    }
    IEnumerator LoadAsyncScene(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    public void FreeLookCameraSwitcher(bool state)
    {
        if (freeLookCamera != null)
        {
            freeLookCamera.enabled = state;
        }
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
