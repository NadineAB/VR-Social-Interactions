using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;

public class AutoExperimentInstruction : MonoBehaviour
{
    public float instructionTimerStart = 0.0f;
    public float currentinstructionTime = 0.0f;
    public static int SceneNo = 1;
    public static bool Reading = false;
    public static bool WOZ = false;
    public static bool WOZ2 = false;

    public float[] instructionTimer = new float[9];
    public AudioSource instructionaudio;

    public static float FirstDiscussionTimeStart = 0.0f;
    public static float FirstDiscussionTimeEnd = 0.0f;
    public static float SecondDiscussionTimeStart = 0.0f;
    public static float SecondDiscussionTimeEnd = 0.0f;
    public static float ReadingTimeStart = 0.0f;
    public AudioClip[] instructions = new AudioClip[9];

    private float lastTime = 0.0f;
    private int nextIndexToPlay = 0;
    public GameObject dataShow;

    // Start is called before the first frame update
    void Start()
    {
        instructionTimerStart = Time.time;
        instructionTimer[0] = instructionTimerStart + instructionTimer[0];
        instructionTimer[1] = instructionTimerStart + instructionTimer[1];
        instructionTimer[2] = instructionTimerStart + instructionTimer[2];
        instructionTimer[3] = instructionTimerStart + instructionTimer[3];
        instructionTimer[4] = instructionTimerStart + instructionTimer[4];
        instructionTimer[5] = instructionTimerStart + instructionTimer[5];
        instructionTimer[6] = instructionTimerStart + instructionTimer[6];
        instructionTimer[7] = instructionTimerStart + instructionTimer[7];
        instructionTimer[8] = instructionTimerStart + instructionTimer[8];
        FirstDiscussionTimeStart = instructionTimer[2];
        FirstDiscussionTimeEnd = instructionTimer[3];
        SecondDiscussionTimeStart = instructionTimer[6];
        SecondDiscussionTimeEnd = instructionTimer[7];
        instructionaudio = GetComponent<AudioSource>();

        //  dataShow = GameObject.Find("SlideShow");
        SceneNo = SceneNo + 1;
        Reading = false;
        WOZ = false;
        WOZ2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentinstructionTime = Time.time - instructionTimerStart;
        float currentTime = Time.time;

        if (nextIndexToPlay < instructionTimer.Length && currentTime > instructionTimer[nextIndexToPlay] && lastTime <= instructionTimer[nextIndexToPlay])
        {
            instructionaudio.clip = instructions[nextIndexToPlay];
            instructionaudio.Play();
            nextIndexToPlay++;
        }

        lastTime = currentTime;

        if (Time.time <= instructionTimer[2] - 0.9f && Time.time > instructionTimer[1] + 2.2f)
        {
            Reading = true;
        }
        else
            Reading = false;

        if (Time.time > instructionTimer[6] - 5.0f)
        {
            WOZ2 = true;
        }
        else
            WOZ2 = false;

        if (Time.time > instructionTimer[2] + 0.5)
        {
            WOZ = true;
        }
        else
            WOZ = false;

        if (Time.time > instructionTimer[5] - 1.3f)
        {
            dataShow.SetActive(true);
        }
        if (SceneNo < 9 && Time.time > instructionTimer[8] + 5.0f)
        {
            SteamVR_Fade.Start(Color.clear, 0.0f);
            SteamVR_Fade.View(Color.clear, 0.1f);
            SceneManager.LoadScene(SceneNo);

        }
    }
}
