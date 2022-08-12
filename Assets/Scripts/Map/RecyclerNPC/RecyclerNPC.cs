using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class RecyclerNPC : MonoBehaviour
{
    public Text recyclerText;
    public bool idle;
    public Vector3 pointA, pointB;
    Rigidbody rb;
    [SerializeField] float speed;
    NavMeshAgent nav;
    Animator anim;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    void Update()
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
    public void CallDialogue()
    {
        recyclerText.text = Resources.Load("Textos/" + gameObject.name).ToString();
    }
    void Wander(Vector3 target)
    {
        nav.SetDestination(target);
    }
}
