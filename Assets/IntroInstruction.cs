/* Introduction Instruction
// Play the audio instruction for the beginning of the experiment
// Nadine Abu Rumman, UCL 2019 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;

public class IntroInstruction : MonoBehaviour
{
    public float instructionTimerStart = 0.0f;
    public float currentinstructionTime = 0.0f;

    public float[] instructionTimer = new float[4];
    public AudioSource instructionaudio;

    public AudioClip[] instructions = new AudioClip[2];

    private float lastTime = 0.0f;
    private int nextIndexToPlay = 0;

    public GameObject dataShow;
    public GameObject Homepage;
    public GameObject stageOne;
    public GameObject End;

    // Start is called before the first frame update
    void Start()
    {
        instructionTimerStart = Time.time;
        instructionTimer[0] = instructionTimerStart + instructionTimer[0];
        instructionTimer[1] = instructionTimerStart + instructionTimer[1];
        instructionaudio = GetComponent<AudioSource>();
    
    }

    // Update is called once per frame
    void Update()
    {
        currentinstructionTime = Time.time - instructionTimerStart;
        float currentTime = Time.time;
        if (Time.time > instructionTimer[2])
        {
            Homepage.SetActive(false);
            dataShow.SetActive(true);
        }
        if (Time.time > instructionTimer[1]-12.57f)
        {
            Homepage.SetActive(false);
            dataShow.SetActive(false);
            stageOne.SetActive(false);
            End.SetActive(true);
        }
        if (VideoPlayingTiming.currentTimePlaying > 10.0f)
        {
            instructionTimer[1] = (float)VideoPlayingTiming.currentTimePlaying + 3.0f;
            VRAcclimateTimer.SceneTimeStop = instructionTimer[1] + 5.0f;
        }
        if (nextIndexToPlay < instructionTimer.Length && currentTime > instructionTimer[nextIndexToPlay] && lastTime <= instructionTimer[nextIndexToPlay])
        {
            instructionaudio.clip = instructions[nextIndexToPlay];
            instructionaudio.Play();
            nextIndexToPlay++;
        }

        lastTime = currentTime;

     
    }
}