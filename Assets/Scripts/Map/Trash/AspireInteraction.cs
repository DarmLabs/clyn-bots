using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspireInteraction : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerInteraction playerInteraction = other.GetComponent<PlayerInteraction>();
            if (playerInteraction.isAspiring == true)
            {
                switch (this.gameObject.tag)
                {
                    case "Vidrio":
                        break;
                    case "Plastico":
                        break;
                    case "Compost":
                        break;
                    case "Carton":
                        break;
                    case "Metal":
                        break;
                }
            }
        }
    }
}
