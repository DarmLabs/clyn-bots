using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    #region Imports & Required Objects
    [Header("Imports & Required Objects")]
    public SaveLoadSystem saveSystem;
    public GlobalVariables gv;
    [SerializeField] ConeCollider cone;
    public PlayerAnimations playerAnim;
    public PlayerMovement playerMovement;
    public Player_UI player_UI;
    public General_UI general_UI;
    [SerializeField] GameObject outisdePoint, insidePoint;
    [SerializeField] GameObject inside, outside;
    public GameObject currentTrashPile;
    public GameObject targetConstruction;
    public GameObject targetRecycler;
    public GameObject targetDeposit;
    public GameObject targetCentralPad;
    public GameObject targetMemoryPad;
    public GameObject targetCompostPad;
    #endregion
    public bool interactionHappen;
    public bool isAspiring;
    public bool minigameAsipire;
    public string inDoor;
    int maxBagSpace = 30, itemsInBag;
    public float bagPercentage;
    bool isDepositing;
    LoadSceneMode mode;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLoadScene;
    }
    void Start()
    {
        playerAnim = GetComponent<PlayerAnimations>();
        playerMovement = GetComponent<PlayerMovement>();
        gv = GameObject.FindObjectOfType<GlobalVariables>().GetComponent<GlobalVariables>();
        saveSystem = GameObject.FindObjectOfType<SaveLoadSystem>().GetComponent<SaveLoadSystem>();
        IntializeFunctions();
    }
    void OnLoadScene(Scene scene, LoadSceneMode mode)
    {
        this.mode = mode;
    }
    void IntializeFunctions()
    {
        OnResume();
        Generador.contadorBasura = 0;
        BagPercentage();
        if (mode == LoadSceneMode.Single)
        {
            player_UI.SetFade(255);
            player_UI.fadeState = 2;
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
            else
            {
                MovmentState(false);
            }
        }
        else if (isDepositing)
        {
            MovmentState(false);
        }
        else
        {
            Controls();
        }
    }
    void Controls()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            general_UI.ExitPanelSwitcher(true);
            general_UI.MainPanelSwitcher(false);
            general_UI.ConstructionPanelSwitcher(false);
            general_UI.MinigameAspireSwitcher(false);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (targetRecycler != null)
            {
                speakWithRecycler();
            }

            if (targetDeposit != null && !targetDeposit.GetComponent<DepositObject>().isFull)
            {
                if (bagPercentage == 100)
                {
                    DepositTrash();
                }
                else
                {
                    Debug.Log("No tienes la mochila llena");
                }
            }
            else if (targetDeposit != null)
            {
                Debug.Log("El deposito esta lleno, juega a la central");
            }

            if (targetCentralPad != null && targetCentralPad.GetComponent<CentralMinigamePad>().deposit.isFull)
            {
                targetCentralPad.GetComponent<CentralMinigamePad>().ActivatePanel();
                MovmentState(false);
            }
            else if (targetCentralPad != null)
            {
                Debug.Log("No hay basura en el deposito");
            }

            if (targetMemoryPad != null && gv.memoriaAccesible)
            {
                targetMemoryPad.GetComponent<MemoryMinigamePad>().ActivatePanel();
                MovmentState(false);
            }
            else if (targetMemoryPad != null)
            {
                Debug.Log("No tienes los recursos suficientes para jugar a la memoria");
            }

            if (targetCompostPad != null /*&& gv.composteraActiva*/)
            {
                targetCompostPad.GetComponent<CompostMinigamePad>().ActivatePanel();
                MovmentState(false);
            }
            else/* if (gv.composteraActiva)*/
            {
                Debug.Log("La compostera esta bien");
            }

            if (targetConstruction != null && targetConstruction.tag != "Untagged")
            {
                general_UI.ConstructionPanelSwitcher(true);
                general_UI.MinimapSwitcher(false);
                targetConstruction.GetComponent<ConstructibleObj>().ShowResources();
                MovmentState(false);
                general_UI.InteractionCloud(false);
            }
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
            gv.compostRefinado += 5;
            gv.metalRefinado += 5;
            gv.vidrioRefinado += 5;
            gv.plasticoRefinado += 5;
            gv.cartonRefinado += 5;
            saveSystem.Save();
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
            saveSystem.Save();
        }
        if (Input.GetKey(KeyCode.Space) && itemsInBag < 30 && inDoor == "ToOutside")
        {
            cone.enabled = true;
            playerAnim.Aspire(true);
            isAspiring = true;
        }
        else if (!isDepositing)
        {
            cone.enabled = false;
            isAspiring = false;
            playerAnim.Aspire(false);
        }
    }
    public void BagPercentage()
    {
        itemsInBag = gv.vidrioTrash + gv.cartonTrash + gv.metalTrash + gv.plasticoTrash + gv.organicTrash + gv.noRecTrash;
        bagPercentage = (itemsInBag * 100) / 30;
        player_UI.DisplayBagPercentage();
    }
    public void ChangeStage()
    {
        player_UI.FadePanel.SetActive(true);
        player_UI.fadeState = 1;
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
                transform.Rotate(0, 180, 0);
                inDoor = "Inside";
                break;
            case "Inside":
                transform.position = outisdePoint.transform.position;
                transform.Rotate(0, 180, 0);
                inDoor = "Outside";
                break;
        }
        MovmentState(true);
        general_UI.MainPanelSwitcher(true);
    }
    public void BuildObject()
    {
        targetConstruction.GetComponent<ConstructibleObj>().BuildObject();
        targetConstruction.GetComponent<SaveTag>().UpdateTag();
    }
    public void UpgradeObject()
    {
        targetConstruction.GetComponent<Seed>().GrowSeed();
    }
    public void speakWithRecycler()
    {
        targetRecycler.GetComponent<RecyclerNPC>().Speak();
    }
    public void stopSpeakingWithRecycler()
    {
        RecyclerNPC targetRecyclerScript = targetRecycler.GetComponent<RecyclerNPC>();
        targetRecyclerScript.RestoreRotation();
        targetRecyclerScript.CheckLockedIdle();
        targetRecyclerScript.isSpeaking = false;
        if (targetRecyclerScript.isBlocker)
        {
            transform.position = targetRecycler.transform.position + targetRecycler.transform.forward * 3;
            transform.rotation = targetRecycler.transform.rotation;
        }
    }
    void DepositTrash()
    {
        isDepositing = true;
        transform.position = targetDeposit.GetComponent<DepositObject>().depositPoint.position;
        transform.rotation = Quaternion.Euler(0, 90, 0);
        playerAnim.Aspire(true);
        StartCoroutine(WaitForAnimation(3f));
    }
    IEnumerator WaitForAnimation(float secs)
    {
        yield return new WaitForSeconds(secs);
        playerAnim.Aspire(false);
        targetDeposit.GetComponent<DepositObject>().SetValues(gv.vidrioTrash, gv.cartonTrash, gv.organicTrash, gv.noRecTrash, gv.plasticoTrash, gv.metalTrash);
        gv.vidrioTrash = 0;
        gv.plasticoTrash = 0;
        gv.organicTrash = 0;
        gv.noRecTrash = 0;
        gv.metalTrash = 0;
        gv.cartonTrash = 0;
        isDepositing = false;
        BagPercentage();
        saveSystem.Save();
    }
    public void EnterDetectObject(GameObject targetObject)
    {
        if (targetObject.tag == "trash")
        {
            currentTrashPile = targetObject.gameObject;
            general_UI.InteractionCloud(true);
        }
        if (targetObject.tag == "construction")
        {
            targetConstruction = targetObject.gameObject;
            general_UI.InteractionCloud(true);
        }
        if (targetObject.tag == "Recycler")
        {
            targetRecycler = targetObject.gameObject;
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
    }
    public void ExitDetectObject(GameObject targetObject)
    {
        if (targetObject.tag == "trash")
        {
            currentTrashPile = null;
            general_UI.InteractionCloud(false);
        }
        if (targetObject.tag == "construction")
        {
            targetConstruction = null;
            general_UI.InteractionCloud(false);
        }
        if (targetObject.tag == "Recycler")
        {
            targetRecycler = null;
            Debug.Log("esNull");
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
        saveSystem.Save();
    }
}