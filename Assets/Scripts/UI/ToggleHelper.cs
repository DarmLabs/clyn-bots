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

    void OnEnable()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        toggle = GetComponent<Toggle>();
        if (!audioManager.musicContainer.activeSelf && gameObject.name == "Musica")
        {
            toggle.isOn = true;
        }
        if (!audioManager.sfxContainer.activeSelf && gameObject.name == "Sonido")
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
        if (!toggle.isOn)
        {
            audioManager.MusicSwitcher(true);
        }
        else
        {
            audioManager.MusicSwitcher(false);
        }
    }
    public void ToggleSFX()
    {
        if (!toggle.isOn)
        {
            audioManager.MusicSwitcher(true);
        }
        else
        {
            audioManager.MusicSwitcher(false);
        }
    }

}
