using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleHelper : MonoBehaviour
{
    [SerializeField] string audioNameOn;
    [SerializeField] string audioNameOff;
    AudioManager audioManager;
    Toggle toggle;
    GlobalVariables gv;

    void OnEnable()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        gv = GameObject.FindObjectOfType<GlobalVariables>();
        toggle = GetComponent<Toggle>();
        if (gv.musicState && gameObject.name == "Musica")
        {
            toggle.isOn = true;
        }
        if (gv.soundState && gameObject.name == "Sonido")
        {
            toggle.isOn = true;
        }
    }
    public void CallThisToggleAudio()
    {
        if (!toggle.isOn)
        {
            audioManager.PlayAudio(audioNameOn);
        }
        else
        {
            audioManager.PlayAudio(audioNameOff);
        }
    }
    public void ToggleMusic()
    {
        audioManager.MusicSwitcher(!toggle.isOn);
        SaveMusicState(toggle.isOn);
        if (!toggle.isOn)
        {
            audioManager.ReplayMusic();
        }
    }
    public void ToggleSFX()
    {
        audioManager.SFXSwitcher(!toggle.isOn);
        SaveSoundState(toggle.isOn);
    }
    public void ToggleMarcos()
    {
        Player_UI player_UI = GameObject.FindObjectOfType<Player_UI>();
        player_UI.SwitchMarcos(toggle.isOn);
    }
    void SaveMusicState(bool state)
    {
        gv.musicState = state;
    }
    void SaveSoundState(bool state)
    {
        gv.soundState = state;
    }
}
