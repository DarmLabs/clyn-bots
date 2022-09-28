using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPS_Counter : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this);
        if(!Debug.isDebugBuild){
            gameObject.SetActive(false);
        }
    }
    public TextMeshProUGUI fpsText;
    private float pollingTime = 1f;
    private float time;
    private int frameCount;
    void Update()
    {
        time += Time.deltaTime;

        frameCount++;
        if(time >= pollingTime){
            int frameRate = Mathf.RoundToInt(frameCount/time);
            fpsText.text = frameRate.ToString() + " FPS";

            time -= pollingTime;
            frameCount = 0;
        }
    }
}
