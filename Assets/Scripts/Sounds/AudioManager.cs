using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject musicContainer;
    public GameObject sfxContainer;
    [SerializeField] AudioSource currentAudio;
    [SerializeField] AudioSource currentMusic;
    [SerializeField] GlobalVariables gv;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void GetGV(){
        gv = GameObject.FindObjectOfType<GlobalVariables>();
    }
    public void CheckAudio()
    {
        if (gv.musicState)
        {
            MusicSwitcher(false);
        }
        if (gv.soundState)
        {
            SFXSwitcher(false);
        }
    }
    public void MusicSwitcher(bool state)
    {
        musicContainer.SetActive(state);
    }
    public void SFXSwitcher(bool state)
    {
        sfxContainer.SetActive(state);
    }
    public void PlayAudio(string name)
    {
        if (sfxContainer.activeSelf)
        {
            currentAudio = sfxContainer.transform.Find(name).GetComponent<AudioSource>();
            if (sfxContainer.activeSelf)
            {
                currentAudio.Play();
            }
        }
    }
    public void PlayMusic(string name)
    {
        currentMusic = musicContainer.transform.Find(name).GetComponent<AudioSource>();
        if (musicContainer.activeSelf)
        {
            currentMusic.Play();
        }
    }
    public void ReplayMusic()
    {
        if (musicContainer.activeSelf)
        {
            currentMusic.Play();
        }
    }

    public void StopAudio()
    {
        if (sfxContainer.activeSelf)
        {
            currentAudio.Stop();
            currentAudio = null;
        }
    }
    public void StopMusic()
    {
        if (musicContainer.activeSelf && currentMusic !=null)
        {
            currentMusic.Stop();
            currentMusic = null;
        }
    }
}
