using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class JSONSaver : MonoBehaviour
{
    [SerializeField] GameObject trashContainer;
    void Start()
    {
        if (File.Exists($"{Application.persistentDataPath}/saveTrash.txt"))
        {
            LoadTrashContainer();
        }
    }
    public void SaveAsJson(object obj, string path)
    {
        string json = JsonUtility.ToJson(obj);
        File.WriteAllText(path, json);
    }
    public T LoadObject<T>(string path)
    {
        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<T>(json);
    }
    void OnApplicationQuit()
    {
        SaveTrashConatiner();
    }
    public void SaveTrashConatiner()
    {
        SaveAsJson(trashContainer, $"{Application.persistentDataPath}/saveTrash.txt");
    }
    public void LoadTrashContainer()
    {
        LoadObject<GameObject>($"{Application.persistentDataPath}/saveTrash.txt");
    }
}
