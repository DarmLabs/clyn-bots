using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHelper : MonoBehaviour
{
    [SerializeField] string audioName;
    AudioManager audioManager;

    void OnEnable()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }
    public void CallThisButtonAudio()
    {
        Debug.Log(gameObject.name);
        audioManager.PlayAudio(audioName);
    }
}
