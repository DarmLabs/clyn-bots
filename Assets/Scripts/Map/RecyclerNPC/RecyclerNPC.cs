using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;
public class RecyclerNPC : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    [SerializeField] GameObject okSection, missionSection;
    [SerializeField] TextMeshProUGUI recyclerText;
    [SerializeField] bool idle;
    public bool isGreeting;
    [SerializeField] bool haveMission;
    public bool lockedIdle;
    public bool isBlocker;
    public Vector3 pointA, pointB;
    NavMeshAgent nav;
    Animator anim;
    public bool isSpeaking;
    GameObject player;
    GameObject playerBody;
    GameObject cinematicCamera;
    GameObject mainCamera;
    General_UI general_UI;
    Vector3 previousRot;
    bool going = true;
    public bool fromResponse;
    [SerializeField] string walkingStyle;
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
        if(file != null){
            general_UI.debugText.text = "file isnt null";
            recyclerText.text = file.text;
        }
        else
        {
            general_UI.debugText.text = "file is null";
        }
    }
    void Wander(Vector3 target)
    {
        nav.destination = target;
    }
    public void Speak(string id)
    {
        isSpeaking = true;
        idle = true;
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
                missionSection.SetActive(true);
            }
            else
            {
                okSection.SetActive(true);
            }
            CinematicCamera();
            dialogueBox.transform.position = cinematicCamera.GetComponent<Camera>().WorldToScreenPoint(transform.position + new Vector3(0, 0.1f, 0));
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
        cinematicCamera.transform.position = player.transform.position - player.transform.forward * 1.5f + player.transform.up;
        cinematicCamera.transform.LookAt(transform.position + transform.up * 1f);
        general_UI.MainPanelSwitcher(false);
        mainCamera.SetActive(false);
        cinematicCamera.SetActive(true);
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
