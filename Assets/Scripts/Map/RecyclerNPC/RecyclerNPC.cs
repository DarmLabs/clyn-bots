using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;
public class RecyclerNPC : MonoBehaviour
{
    GameObject dialogueBox;
    GameObject okSection, missionSection;
    TextMeshProUGUI recyclerText;
    [HideInInspector] public bool missionTarget;
    [SerializeField] bool haveMission;
    [SerializeField] string scene;
    [SerializeField] bool idle;
    public bool lockedIdle;
    public bool isBlocker;
    public Vector3 pointA, pointB;
    Vector3 previousPoint;
    [SerializeField] string walkingStyle;
    NavMeshAgent nav;
    Animator anim;
    public bool isGreeting;
    public bool isSpeaking;
    GameObject player;
    GameObject playerBody;
    [HideInInspector] public GameObject cinematicCamera;
    GameObject mainCamera;
    General_UI general_UI;
    Vector3 previousRot;
    bool going = true;
    public bool fromResponse;
    [HideInInspector][SerializeField] Transform cinematicCameraPoint;
    void Awake()
    {
        pointA = pointA + transform.parent.position;
        pointB = pointB + transform.parent.position;
        previousRot = transform.eulerAngles;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        mainCamera = Camera.main.gameObject;
        FindImports();
    }
    void FindImports()
    {
        dialogueBox = GameObject.Find("DialogueBox");
        okSection = GameObject.Find("Ok");
        missionSection = GameObject.Find("MissionSection");
        recyclerText = dialogueBox.GetComponentInChildren<TextMeshProUGUI>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerBody = GameObject.FindGameObjectWithTag("PlayerBody");
        cinematicCamera = GameObject.Find("CinematicCamera");
        general_UI = GameObject.FindObjectOfType<General_UI>().GetComponent<General_UI>();
    }
    void Update()
    {
        CheckIdle();
    }
    void CallDialogue(string id)
    {
        TextAsset file;
        if (fromResponse)
        {
            if (id != null)
            {
                file = Resources.Load<TextAsset>("Textos/" + gameObject.name + "_Response" + id);
            }
            else
            {
                file = Resources.Load<TextAsset>("Textos/" + gameObject.name + "_Response_01");
            }
        }
        else
        {
            file = Resources.Load<TextAsset>("Textos/" + gameObject.name);
        }
        if (file != null)
        {
            recyclerText.text = file.text;
        }
        fromResponse = false;
    }
    void Wander(Vector3 target)
    {
        nav.destination = target;
    }
    public void RestoreDestination()
    {
        nav.destination = previousPoint;
    }
    public void Speak(string id)
    {
        isSpeaking = true;
        idle = true;
        previousPoint = nav.destination;
        nav.destination = transform.position;
        CheckSpeaking(id);
        Greet();
    }
    void CheckSpeaking(string id)
    {
        if (isSpeaking)
        {
            dialogueBox.SetActive(true);
            if (!isBlocker)
            {
                transform.LookAt(player.transform);
            }
            if (haveMission)
            {
                Button yesBtn = missionSection.transform.Find("Yes").GetComponent<Button>();
                if (gameObject.name == "RecyclerMainMission")
                {
                    yesBtn.onClick.AddListener(delegate { general_UI.MainMissionSwitcher(true); });
                }
                else
                {
                    if (gameObject.name == "RecyclerMinigameCentral" && general_UI.playerInteraction.bagPercentage != 100)
                    {
                        fromResponse = true;
                        CinematicCamera();
                        CallDialogue(null);
                        okSection.SetActive(true);
                        return;
                    }
                    else
                    {
                        yesBtn.onClick.AddListener(delegate { general_UI.ChangeScene(scene); });
                    }
                }
                missionSection.SetActive(true);
            }
            else
            {
                okSection.SetActive(true);
            }
            CinematicCamera();
            CallDialogue(id);
        }
    }
    void CheckIdle()
    {
        if (!idle)
        {
            if (going && nav.remainingDistance <= nav.stoppingDistance)
            {
                Wander(pointB);
                going = false;
            }
            else if (!going && nav.remainingDistance <= nav.stoppingDistance)
            {
                Wander(pointA);
                going = true;
            }
            anim.Play(walkingStyle);
            previousRot = transform.eulerAngles;
        }
        else if (!isGreeting)
        {
            anim.Play("Idle");
        }
    }
    void CinematicCamera()
    {
        playerBody.SetActive(false);
        cinematicCamera.transform.parent = this.transform;
        cinematicCamera.transform.position = cinematicCameraPoint.position;
        cinematicCamera.transform.rotation = cinematicCameraPoint.rotation;
        general_UI.MainPanelSwitcher(false);
        mainCamera.SetActive(false);
        cinematicCamera.SetActive(true);
        dialogueBox.transform.position = cinematicCamera.GetComponent<Camera>().WorldToScreenPoint(transform.position + new Vector3(0, -0.2f, 0));
    }
    public void RestoreRotation()
    {
        transform.rotation = Quaternion.Euler(previousRot);
    }
    public void Greet()
    {
        isGreeting = true;
        anim.Play("Greet");
    }
    public void Attention()
    {
        isGreeting = true;
        anim.Play("Attention");
    }
    public void CheckLockedIdle()
    {
        if (!lockedIdle)
        {
            idle = false;
        }
    }
}
