using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
public class SaveLoadSystem : MonoBehaviour
{
    public string SavePath => $"{Application.persistentDataPath}/save.txt";
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLoadScene;
    }
    void OnLoadScene(Scene scene, LoadSceneMode mode)
    {
        Load();
    }
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void Save()
    {
        var state = LoadFile();
        SaveState(state);
        SaveFile(state);
    }
    public void Load()
    {
        var state = LoadFile();
        LoadState(state);
    }
    void OnApplicationQuit()
    {
        Save();
    }
    public void SaveFile(object state)
    {
        using (var stream = File.Open(SavePath, FileMode.Create))
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, state);
        }
    }
    Dictionary<string, object> LoadFile()
    {
        if (!File.Exists(SavePath))
        {
            Debug.Log("No save file found");
            return new Dictionary<string, object>();
        }
        using (FileStream stream = File.Open(SavePath, FileMode.Open))
        {
            var formatter = new BinaryFormatter();
            return (Dictionary<string, object>)formatter.Deserialize(stream);
        }
    }
    void SaveState(Dictionary<string, object> state)
    {
        foreach (var saveable in FindObjectsOfType<SavableEntity>())
        {
            state[saveable.Id] = saveable.SaveState();
        }
    }
    void LoadState(Dictionary<string, object> state)
    {
        foreach (var saveable in FindObjectsOfType<SavableEntity>())
        {
            if (state.TryGetValue(saveable.Id, out object savedState))
            {
                saveable.LoadState(savedState);
            }
        }
    }
}
