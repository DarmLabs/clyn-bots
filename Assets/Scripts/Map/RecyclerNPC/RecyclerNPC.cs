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
    public Vector3 pointA, pointB;
    Rigidbody rb;
    [SerializeField] float speed;
    NavMeshAgent nav;
    Animator anim;
    bool isSpeaking;
    public GameObject player;
    public GameObject cinematicCamera;
    GameObject mainCamera;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        nav.SetDestination(target);
    }
    public void Speak(){
        isSpeaking = true;
        CheckSpeaking();
    }
    void CheckSpeaking()
    {
        if (isSpeaking)
        {
            dialogueBox.SetActive(true);
            transform.LookAt(player.transform);
            CinematicCamera();
            dialogueBox.transform.position = cinematicCamera.GetComponent<Camera>().WorldToScreenPoint(transform.position + new Vector3(-0.8f, 2.5f, 0));
            CallDialogue();
        }
    }
    void CheckIdle()
    {
        if (!idle)
        {
            if (nav.remainingDistance < 0.1 && nav.destination == pointA)
            {
                Wander(pointB);
            }
            else if (nav.remainingDistance < 0.1 && nav.destination == pointB)
            {
                Wander(pointA);
            }
        }
    }
    void CinematicCamera(){
        cinematicCamera.transform.position = transform.position + transform.forward;
        cinematicCamera.transform.LookAt(transform.position + transform.up);
        mainCamera.SetActive(false);
        cinematicCamera.SetActive(true);
    }
}
