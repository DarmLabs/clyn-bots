using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            transform.localScale = transform.localScale * 3;
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            transform.localScale = transform.localScale / 3;
        }
    }
    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 newPosition = target.position;
            newPosition.y = transform.position.y;
            transform.position = newPosition + offset;
        }
        else
        {
            gameObject.SetActive(false);
        }

    }
}
