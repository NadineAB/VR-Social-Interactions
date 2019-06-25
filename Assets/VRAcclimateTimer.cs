using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;

public class VRAcclimateTimer : MonoBehaviour
{

    public static bool SceneEnable = false;
    public float SceneTimeStart = 0;  // start scene
    public float SceneStop = 300.0f;
    public static float SceneTimeStop =300.0f;  // stop the scene after 2 min
    public float currentTime = 0.0f;
    
    private void Start()
    {
     
    }

    void Update()
    {
        SceneStop = SceneTimeStop;
        currentTime = Time.time - SceneTimeStart;
        if (Time.time - SceneTimeStart > SceneTimeStop)
            SceneEnable = true;
        else
            SceneEnable = false;

        if (SceneEnable)
        {
            SteamVR_Fade.Start(Color.black, 0.0f);
            SteamVR_Fade.View(Color.black, 0.1f);
           

            if (Time.time - SceneTimeStart > SceneTimeStop + 0.8f)
            {
                SteamVR_Fade.Start(Color.clear, 0.0f);
                SteamVR_Fade.View(Color.clear, 0.1f);
                 SceneManager.LoadScene(1);
              
            }
        }

    }
}


