using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWizardOfOz : MonoBehaviour
{
    public float shadowDrawDistance;
    public int ResX;
    public int ResY;
    public bool Fullscreen;


    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnGUI()
    {
        GUIStyle Mainstyle = GUI.skin.GetStyle("Button");
        Mainstyle.fontSize = 30;
        GUI.Button(new Rect(400, 150, 300, 100), "Auto Wizard Of Oz", Mainstyle);
    }       
      
}
