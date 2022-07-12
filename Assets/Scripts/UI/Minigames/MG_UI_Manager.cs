using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MG_UI_Manager : MonoBehaviour
{
    public void BackTo3D()
    {
        SceneManager.LoadScene("Main_Stage");
    }

    public void Reintentar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
