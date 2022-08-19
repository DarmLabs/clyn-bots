using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class CompostMinigamePad : MonoBehaviour
{
    [SerializeField] General_UI general_UI;
    [SerializeField] GameObject compostera;
    [SerializeField] GlobalVariables gv;
    [SerializeField] PlayerInteraction player;
    [SerializeField] GameObject responseRecycler;
    void Start()
    {
        gv = GameObject.FindObjectOfType<GlobalVariables>();
        if(gv.compostActiva)
        {
            CompostState(true);
        }
        else
        {
            CompostState(false);
        }
    }
    public void Response(string id)
    {
        player.targetRecycler = responseRecycler;
        responseRecycler.GetComponent<RecyclerNPC>().fromResponse = true;
        responseRecycler.GetComponent<RecyclerNPC>().Speak(id);
    }
    public void CompostState(bool state)
    {
        compostera.GetComponent<Animator>().SetBool("open", state);
    }
    public void ActivatePanel()
    {
        general_UI.CompostMinigameSwitcher(true);
        general_UI.MainPanelSwitcher(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            general_UI.InteractionCloud(true);
            general_UI.playerInteraction.targetCompostPad = gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
   
        if (other.gameObject.tag == "Player")
        {
            general_UI.InteractionCloud(false);
            general_UI.playerInteraction.targetCompostPad = null;
        }
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene("Compost");
    }
}
