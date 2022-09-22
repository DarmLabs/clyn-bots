using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    #region Imports & Required Objects
    [Header("Imports & Required Objects")]
    [HideInInspector] public SaveLoadSystem saveSystem;
    [HideInInspector] public TakeBools takeBools;
    [HideInInspector] public GlobalVariables gv;
    [SerializeField] ConeCollider cone;
    [HideInInspector] public PlayerAnimations playerAnim;
    [HideInInspector] public PlayerMovement playerMovement;
    public Player_UI player_UI;
    public General_UI general_UI;
    [SerializeField] GameObject outisdePoint, insidePoint;
    public GameObject targetConstruction;
    public GameObject targetOrchard;
    public GameObject targetRecycler;
    public GameObject targetDeposit;
    public GameObject targetPipes;
    public GameObject targetMainMenu;
    public GameObject targetRefiner;
    public GameObject targetArcade;
    [SerializeField] GameObject initialPlayerPosition;
    SceneCache sceneCache;
    #endregion
    public bool isAspiring;
    [HideInInspector] public string inDoor;
    int maxBagSpace = 90, itemsInBag;
    public float bagPercentage;
    public LoadSceneMode mode;
    [HideInInspector] public MainMission mainMission;
    [HideInInspector] public EnviromentChanger enviromentChanger;
    VC_Switcher vC_Switcher;
    [HideInInspector] public AudioManager audioManager;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLoadScene;
        gv = GameObject.FindObjectOfType<GlobalVariables>();
        saveSystem = GameObject.FindObjectOfType<SaveLoadSystem>();
        takeBools = GameObject.FindObjectOfType<TakeBools>();
        mainMission = GameObject.FindObjectOfType<MainMission>();
        enviromentChanger = GameObject.FindObjectOfType<EnviromentChanger>();
        vC_Switcher = GameObject.FindObjectOfType<VC_Switcher>();
        sceneCache = GameObject.FindObjectOfType<SceneCache>();
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }
    void Start()
    {
        playerAnim = GetComponent<PlayerAnimations>();
        playerMovement = GetComponent<PlayerMovement>();
        IntializeFunctions();
    }
    void OnLoadScene(Scene scene, LoadSceneMode mode)
    {
        this.mode = mode;
        inDoor = scene.name;
    }
    void IntializeFunctions()
    {
        OnResume();
        Generador.contadorBasura = 0;
        BagPercentage();
        if (gv.firstTime)
        {
            if (inDoor == "Inside" && mode != LoadSceneMode.Single)
            {
                audioManager.StopMusic();
                audioManager.PlayMusic("Menu_Theme");
                general_UI.TutorialPanelSwithcer(false);
                vC_Switcher.NotFirstTime();
                general_UI.MainMenuSwitcher(true);
                MovmentState(true);
                transform.position = initialPlayerPosition.transform.position;
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (mode == LoadSceneMode.Single)
            {
                player_UI.SetFade(255);
                player_UI.fadeState = 2;
                if (inDoor == "Inside")
                {
                    audioManager.StopMusic();
                    audioManager.PlayMusic("Inside_Theme");
                    if (sceneCache.previousScene == "Outside")
                    {
                        transform.position = insidePoint.transform.position;
                        transform.eulerAngles = new Vector3(0, 90, 0);
                    }
                    else if (sceneCache.previousScene == "InitalScene")
                    {
                        transform.position = initialPlayerPosition.transform.position;
                        transform.eulerAngles = new Vector3(0, 180, 0);
                    }
                    general_UI.MainMenuSwitcher(false);
                    general_UI.TutorialPanelSwithcer(false);
                    vC_Switcher.NotFirstTime();
                    general_UI.MainPanelSwitcher(true);
                    MovmentState(true);
                }
                if (inDoor == "Outside")
                {
                    audioManager.StopMusic();
                    audioManager.PlayMusic("Outside_Theme");
                    if (sceneCache.previousScene == "Inside")
                    {
                        transform.position = outisdePoint.transform.position;
                        transform.eulerAngles = new Vector3(0, -90, 0);
                    }
                }
            }
        }
        else
        {
            audioManager.PlayMusic("Menu_Theme");
        }
        if (gv.pipesActiva)
        {
            gv.pipesActiva = false;
            targetPipes.GetComponent<PipesMinigame>().Block();
        }

    }
    void Update()
    {
        CheckExceptions();
    }
    void CheckExceptions()
    {
        if (targetRecycler != null)
        {
            if (!targetRecycler.GetComponent<RecyclerNPC>().isSpeaking)
            {
                Controls();
            }
        }
        else
        {
            Controls();
        }
    }
    void Controls()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Interaction();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            int aux;
            aux = (maxBagSpace - itemsInBag) / 6;
            gv.vidrioTrash = aux;
            gv.plasticoTrash = aux;
            gv.organicTrash = aux;
            gv.noRecTrash = aux;
            gv.metalTrash = aux;
            gv.cartonTrash = aux;
            BagPercentage();
            gv.divisionOrganic += 10;
            gv.compostRefinado += 5;
            gv.metalRefinado += 5;
            gv.vidrioRefinado += 5;
            gv.plasticoRefinado += 5;
            gv.cartonRefinado += 5;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            gv.vidrioTrash = 0;
            gv.plasticoTrash = 0;
            gv.organicTrash = 0;
            gv.noRecTrash = 0;
            gv.metalTrash = 0;
            gv.cartonTrash = 0;
            BagPercentage();
        }
        if (Input.GetKey(KeyCode.X) && itemsInBag < 90 && inDoor == "Outside")
        {
            cone.enabled = true;
            playerAnim.Aspire(true);
            isAspiring = true;
        }
        else if ((itemsInBag >= 90) && isAspiring)
        {
            cone.enabled = false;
            isAspiring = false;
            playerAnim.Aspire(false);
        }
        if (Input.GetKeyDown(KeyCode.M) && inDoor == "Outside" && general_UI.mainPanel.activeSelf != false)
        {
            general_UI.FullMapSwitcher(true);
            general_UI.MainPanelSwitcher(false);
        }
        if (Input.GetKeyUp(KeyCode.M) && inDoor == "Outside")
        {
            general_UI.FullMapSwitcher(false);
            general_UI.MainPanelSwitcher(true);
        }
    }
    void Interaction()
    {
        {
            if (targetRecycler != null)
            {
                speakWithRecycler();
            }
            if (targetPipes != null)
            {
                targetPipes.GetComponent<PipesMinigame>().ActivatePanel();
            }

            if (targetConstruction != null && (targetConstruction.tag != "Untagged" || targetConstruction.tag != "Pipes"))
            {
                general_UI.ConstructionPanelSwitcher(true);
                general_UI.MinimapSwitcher(false);
                targetConstruction.GetComponent<ConstructibleObj>().ShowResources();
                MovmentState(false);
                general_UI.InteractionCloud(false);
            }

            if (targetOrchard != null && targetOrchard.tag != "Untagged")
            {
                targetOrchard.GetComponent<Orchard>().ActivatePanel();
                targetOrchard.GetComponent<ConstructibleObj>().ShowResources();
            }
            else if (targetOrchard != null && targetOrchard.tag == "Untagged")
            {
                Debug.Log("Ya esta sembarado y crecido");
            }

            if (targetMainMenu != null)
            {
                audioManager.StopMusic();
                audioManager.PlayMusic("Menu_Theme");
                vC_Switcher.VC_MainMenuSwitcher(true);
                vC_Switcher.VC_PlayerViewSwitcher(false);
                MovmentState(false);
                general_UI.MainPanelSwitcher(false);
                general_UI.MainMenuSwitcher(true);
                general_UI.CheckTutoAvailable();
                ExitDetectObject(targetMainMenu);
            }
            if (targetRefiner != null)
            {
                general_UI.RefinerPanelSwitcher(true);
                general_UI.MinimapSwitcher(false);
                MovmentState(false);
                general_UI.InteractionCloud(false);
            }
            if (targetArcade != null)
            {
                general_UI.ChangeScene(targetArcade.name);
            }
        }
    }
    public void BagPercentage()
    {
        itemsInBag = gv.vidrioTrash + gv.cartonTrash + gv.metalTrash + gv.plasticoTrash + gv.organicTrash + gv.noRecTrash;
        bagPercentage = (itemsInBag * 100) / 90;
        player_UI.DisplayBagPercentage();
    }
    public void ChangeStage()
    {
        player_UI.FadePanel.SetActive(true);
        player_UI.fadeState = 1;
        SaveTransform();
        saveSystem.Save();
        if (takeBools != null)
        {
            takeBools.TakeDestoyed();
            FileHandler.SaveToJSON<bool>(takeBools.destoyedList, "saveTrash.txt");
        }
        StartCoroutine(LoadAsyncScene(inDoor));
    }
    public void MovmentState(bool state)
    {
        playerMovement.enabled = state;
    }
    IEnumerator LoadAsyncScene(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
    public void CancelChangeStage()
    {
        switch (inDoor)
        {
            case "Outside":
                transform.position = insidePoint.transform.position;
                transform.rotation = Quaternion.Euler(0, 90, 0);
                inDoor = "Inside";
                break;
            case "Inside":
                transform.position = outisdePoint.transform.position;
                transform.rotation = Quaternion.Euler(0, -90, 0);
                inDoor = "Outside";
                break;
        }
        MovmentState(true);
        general_UI.MainPanelSwitcher(true);
    }
    public void BuildObject()
    {
        targetConstruction.GetComponent<ConstructibleObj>().BuildObject();
    }
    public void PlantSeed()
    {
        targetOrchard.GetComponent<Orchard>().PlantSeed(false);
    }
    public void GrowSeed()
    {
        targetOrchard.GetComponent<Orchard>().GrowSeed(false);
    }
    public void speakWithRecycler()
    {
        RecyclerNPC targetRecyclerScript = targetRecycler.GetComponent<RecyclerNPC>();
        targetRecyclerScript.Speak(null);
        MovmentState(false);
    }
    public void stopSpeakingWithRecycler()
    {
        RecyclerNPC targetRecyclerScript = targetRecycler.GetComponent<RecyclerNPC>();
        targetRecyclerScript.RestoreDestination();
        targetRecyclerScript.RestoreRotation();
        targetRecyclerScript.CheckLockedIdle();
        targetRecyclerScript.isSpeaking = false;
        targetRecyclerScript.cinematicCamera.transform.parent = null;
        if (targetRecyclerScript.isBlocker)
        {
            transform.position = targetRecycler.transform.position + targetRecycler.transform.forward * 3;
            transform.rotation = targetRecycler.transform.rotation;
        }
        if (targetRecyclerScript.fromResponse)
        {
            targetRecyclerScript.fromResponse = false;
            targetRecycler = null;
        }
    }
    public void EnterDetectObject(GameObject targetObject)
    {
        if (targetObject.tag == "Pipes")
        {
            targetPipes = targetObject;
            general_UI.InteractionCloud(true);
        }
        if (targetObject.tag == "construction")
        {
            targetConstruction = targetObject;
            general_UI.InteractionCloud(true);
        }
        if (targetObject.tag == "Orchard")
        {
            targetOrchard = targetObject;
            general_UI.InteractionCloud(true);
        }
        if (targetObject.tag == "Recycler")
        {
            targetRecycler = targetObject;
            if (!targetRecycler.GetComponent<RecyclerNPC>().isBlocker || !targetRecycler.GetComponent<RecyclerNPC>().lockedIdle)
            {
                targetRecycler.GetComponent<RecyclerNPC>().Attention();
            }
            else
            {
                speakWithRecycler();
            }
            general_UI.InteractionCloud(true);
        }
        if (targetObject.tag == "MainMenu")
        {
            targetMainMenu = targetObject;
            general_UI.InteractionCloud(true);
        }
        if (targetObject.tag == "Refiner")
        {
            targetRefiner = targetObject;
            general_UI.InteractionCloud(true);
        }
        if (targetObject.tag == "arcade")
        {
            targetArcade = targetObject;
            general_UI.InteractionCloud(true);
        }
    }
    public void ExitDetectObject(GameObject targetObject)
    {
        if (targetObject.tag == "Pipes")
        {
            targetPipes = null;
            general_UI.InteractionCloud(false);
        }
        if (targetObject.tag == "construction")
        {
            targetConstruction = null;
            general_UI.InteractionCloud(false);
        }
        if (targetObject.tag == "Orchard")
        {
            targetOrchard = null;
            general_UI.InteractionCloud(false);
        }
        if (targetObject.tag == "Recycler")
        {
            targetRecycler = null;
            general_UI.InteractionCloud(false);
        }
        if (targetObject.tag == "MainMenu")
        {
            targetMainMenu = null;
            general_UI.InteractionCloud(false);
        }
        if (targetObject.tag == "Refiner")
        {
            targetRefiner = null;
            general_UI.InteractionCloud(false);
        }
        if (targetObject.tag == "arcade")
        {
            targetArcade = null;
            general_UI.InteractionCloud(false);
        }
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