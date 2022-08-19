using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MemoryMinigamePad : MonoBehaviour
{
    [SerializeField] General_UI general_UI;
    [SerializeField] PlayerInteraction player;
    [SerializeField] GameObject responseRecycler;
    public void Response(string id)
    {
        player.targetRecycler = responseRecycler;
        responseRecycler.GetComponent<RecyclerNPC>().fromResponse = true;
        responseRecycler.GetComponent<RecyclerNPC>().Speak(id);
    }
    public void ActivatePanel()
    {
        general_UI.MemoryMinigameSwitcher(true);
        general_UI.MainPanelSwitcher(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            general_UI.InteractionCloud(true);
            general_UI.playerInteraction.targetMemoryPad = gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            general_UI.InteractionCloud(false);
            general_UI.playerInteraction.targetMemoryPad = null;
        }
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene("Memory");
    }
}