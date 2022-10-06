using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConfigPantalla : MonoBehaviour
{
    public Toggle toggle;
    [SerializeField] Toggle camToggle;
    public TMP_Dropdown resolucionesDropDown;
    public double aspectRatio;
    Resolution[] resoluciones;
    GlobalVariables gv;

    void Start()
    {
        gv = GameObject.FindObjectOfType<GlobalVariables>();
        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }
        if (gv.isCameraBlocked)
        {
            camToggle.isOn = true;
        }
        else
        {
            camToggle.isOn = false;
        }
        DontDestroyOnLoad(gameObject);
        RevisarResolucion();
    }

    public void RevisarResolucion()
    {
        resoluciones = Screen.resolutions;
        resolucionesDropDown.ClearOptions();
        List<string> opciones = new List<string>();
        int resolucionActual = 0;
        for (int i = 0; i < resoluciones.Length; i++)
        {
            aspectRatio = (double)resoluciones[i].width / (double)resoluciones[i].height;
            Debug.Log("aspectRatio:  " + aspectRatio);
            if ((aspectRatio) >= 1.77f && (aspectRatio) <= 1.8f)
            {
                string opcion = resoluciones[i].width + " x " + resoluciones[i].height;
                opciones.Add(opcion);
                if (Screen.fullScreen && resoluciones[i].width == Screen.currentResolution.width && resoluciones[i].height == Screen.currentResolution.height)
                {
                    resolucionActual = i;
                }
            }
        }
        resolucionesDropDown.AddOptions(opciones);
        resolucionesDropDown.value = resolucionActual;
        resolucionesDropDown.RefreshShownValue();
        //guardado
    }

    public void CambiarResolucion(int indiceResolucion)
    {
        Resolution resolucion = resoluciones[indiceResolucion];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
        //guardado
    }

    public void PantallaCompletaON(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }
    public void CameraLockSwitcher(bool state)
    {
        if (state)
        {
            gv.isCameraBlocked = true;
        }
        else
        {
            gv.isCameraBlocked = false;
        }
    }
}
