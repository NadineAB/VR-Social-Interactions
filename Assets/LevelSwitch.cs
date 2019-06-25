using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LevelSwitch : MonoBehaviour
{
    public int currLevel = 0;
    public string[] levelNames = new string[2] { "VR_SocialInteraction", "VR_SocialInteraction_AcclimateLevel" };

    static LevelSwitch s = null;
    // Start is called before the first frame update
    void Start()
    {
        if (s == null)
            s = this;
        else Destroy(this.gameObject);


        DontDestroyOnLoad(this.gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (enable)
        //{
            currLevel = (currLevel + 1) % 2; // next level
            SteamVR_LoadLevel.Begin(levelNames[currLevel]);


       // }
    }
}
