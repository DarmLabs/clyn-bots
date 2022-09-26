using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject musicContainer;
    public GameObject sfxContainer;
    [SerializeField] AudioSource currentAudio;
    [SerializeField] AudioSource currentMusic;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
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
            if (currentAudio != null)
            {
                currentAudio.Play();
            }
        }
    }
    public void PlayMusic(string name)
    {
        currentMusic = musicContainer.transform.Find(name).GetComponent<AudioSource>();
        if (currentMusic != null)
        {
            currentMusic.Play();
        }
    }
    public void ReplayMusic()
    {
        if (currentMusic != null)
        {
            currentMusic.Play();
        }
    }

    public void StopAudio()
    {
        if (currentAudio != null)
        {
            currentAudio.Stop();
            currentAudio = null;
        }
    }
    public void StopMusic()
    {
        if (currentMusic != null)
        {
            currentMusic.Stop();
            currentMusic = null;
        }
    }
}
