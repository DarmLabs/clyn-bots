using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class RecyclerNPC : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text recyclerText;
    public bool idle;
    public Vector3 pointA, pointB;
    Rigidbody rb;
    [SerializeField] float speed;
    NavMeshAgent nav;
    Animator anim;
    bool isSpeaking;
    public Vector3 offset;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        CheckIdle();
        CheckSpeaking();
    }
    public void CallDialogue()
    {
        dialogueBox.SetActive(true);
        recyclerText.text = Resources.Load("Textos/" + gameObject.name).ToString();
    }
    void Wander(Vector3 target)
    {
        nav.SetDestination(target);
    }
    void CheckSpeaking()
    {
        if (isSpeaking)
        {
            dialogueBox.SetActive(true);
            dialogueBox.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
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
}
