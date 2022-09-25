using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PipesMinigame : MonoBehaviour
{
    [SerializeField] General_UI general_UI;
    [SerializeField] PlayerInteraction player;
    [SerializeField] GameObject responseRecycler;
    void Start()
    {
        if (player.gv.pipesActiva && player.targetPipes == this.gameObject)
        {
            Block();
        }
    }
    public void Block()
    {
        gameObject.tag = "Untagged";
        GetComponent<SaveTag>().UpdateTag();
        player.gv.pipesActiva = false;
        this.enabled = false;
    }
    public void ActivatePanel()
    {
        general_UI.PipesMinigameSwitcher(true);
        general_UI.MainPanelSwitcher(false);
        general_UI.InteractionCloud(false);
    }
}
