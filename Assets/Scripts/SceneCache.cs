using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCache : MonoBehaviour
{
    public string previousScene;
    public string currentScene;
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLoadScene;
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    void OnLoadScene(Scene scene, LoadSceneMode mode)
    {
        if (currentScene != scene.name)
        {
            PassName();
            currentScene = scene.name;
        }
    }
    void PassName()
    {
        previousScene = currentScene;
    }
}
