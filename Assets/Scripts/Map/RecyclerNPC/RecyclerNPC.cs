using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;
public class RecyclerNPC : MonoBehaviour
{
    public GameObject dialogueBox;
    public TextMeshProUGUI recyclerText;
    public bool idle;
    public bool isGreeting;
    public bool lockedIdle;
    public bool isBlocker;
    public Vector3 pointA, pointB;
    Rigidbody rb;
    [SerializeField] float speed;
    NavMeshAgent nav;
    Animator anim;
    public bool isSpeaking;
    public GameObject player;
    public GameObject cinematicCamera;
    GameObject mainCamera;
    public General_UI general_UI;
    Vector3 previousRot;
    bool going = true;
    public string walkingStyle;
    void Start()
    {
        pointA = pointA + transform.parent.position;
        pointB = pointB + transform.parent.position;
        previousRot = transform.eulerAngles;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        mainCamera = Camera.main.gameObject;
    }
    void Update()
    {
        CheckIdle();
    }
    void CallDialogue()
    {
        recyclerText.text = Resources.Load("Textos/" + gameObject.name).ToString();
    }
    void Wander(Vector3 target)
    {
        nav.destination = target;
    }
    public void Speak()
    {
        isSpeaking = true;
        idle = true;
        nav.destination = transform.position;
        CheckSpeaking();
        Greet();
    }
    void CheckSpeaking()
    {
        if (isSpeaking)
        {
            dialogueBox.SetActive(true);
            if (!isBlocker)
            {
                transform.LookAt(player.transform);
            }
            CinematicCamera();
            dialogueBox.transform.position = cinematicCamera.GetComponent<Camera>().WorldToScreenPoint(transform.position + new Vector3(0, 0.1f, 0));
            CallDialogue();
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
        player.transform.GetChild(3).transform.gameObject.SetActive(false);
        cinematicCamera.transform.position = transform.position + transform.forward * 3.5f + transform.up * 1f;
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
