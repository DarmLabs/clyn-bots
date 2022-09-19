using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject musicContainer;
    public GameObject sfxContainer;
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
    public void CallAudio(GameObject type, string name)
    {
        if (type.activeSelf)
        {
            AudioSource audio = type.transform.Find(name).GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.Play();
            }
        }
    }
}
