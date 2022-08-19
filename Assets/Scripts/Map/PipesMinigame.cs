using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PipesMinigame : MonoBehaviour
{
    [SerializeField] General_UI general_UI;
    [SerializeField] PlayerInteraction player;
    [SerializeField] GameObject responseRecycler;
    public void Block(){
        gameObject.tag= "Untagged";
        this.enabled = false;
    }
    public void Response(string id)
    {
        player.targetRecycler = responseRecycler;
        responseRecycler.GetComponent<RecyclerNPC>().fromResponse = true;
        responseRecycler.GetComponent<RecyclerNPC>().Speak(id);
    }
    public void ActivatePanel()
    {
        general_UI.PipesMinigameSwitcher(true);
        general_UI.MainPanelSwitcher(false);
        general_UI.InteractionCloud(false);
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene("Pipes");
    }
}