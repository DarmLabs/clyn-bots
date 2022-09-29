using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPong : MonoBehaviour
{
    //public GameObject flecha;
    public float speed = 2.3f;

    public void Update()
    {
        float y = Mathf.PingPong(Time.time * speed, 1) * 6 - 3;
        this.gameObject.transform.position = new Vector3(1219, 200+y,0);
    }
}
