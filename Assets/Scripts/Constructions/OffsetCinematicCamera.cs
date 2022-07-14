using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetCinematicCamera : MonoBehaviour
{
    public General_UI general_UI;
    Vector3 offset = new Vector3(-15, 20, -15);
    public void SetTarget(GameObject target){
        if(target.transform.childCount != 0){
            transform.position = target.transform.GetChild(0).transform.position + offset;
        }else{
            transform.position = target.transform.position + offset;
        }
        gameObject.SetActive(true);
        transform.LookAt(target.transform);
        general_UI.MainPanelSwitcher(false);
        StartCoroutine(WaitInCinematic(5f));
    }

    IEnumerator WaitInCinematic(float secs){
        yield return new WaitForSeconds(secs);
        gameObject.SetActive(false);
        general_UI.playerInteraction.MovmentState(true);
        general_UI.MainPanelSwitcher(true);
        general_UI.playerInteraction.enabled = true;
    }
}
