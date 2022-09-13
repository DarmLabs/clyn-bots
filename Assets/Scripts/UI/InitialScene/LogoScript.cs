using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoScript : MonoBehaviour
{
    [SerializeField] InitialSceneManager sceneManager;
    [SerializeField] GameObject nextLogo;
    bool fadeState = true;
    void Update()
    {
        if (fadeState)
        {
            sceneManager.FadeIn(gameObject);
            if (sceneManager.fadeTime >= 1)
            {
                StartCoroutine(WaitForFadeOut(1.5f));
            }
        }
        if (!fadeState)
        {
            sceneManager.FadeOut(gameObject);
            if (sceneManager.fadeTime <= 0)
            {
                sceneManager.fadeTime = 0;
                sceneManager.ShowNewLogo(nextLogo);
                gameObject.SetActive(false);
            }
        }
        IEnumerator WaitForFadeOut(float secs)
        {
            yield return new WaitForSecondsRealtime(secs);
            fadeState = false;
        }
    }
}
