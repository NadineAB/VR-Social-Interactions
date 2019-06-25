/* DelayedHeadNodding class
// The virtual character excutes a nod by copying the participant nodding after 600ms
// This class ignores fast nods which have frequency above 1.8Hz
// Slow nods is 0.2-1.8hz
// ordinary nods 1.8-3.7hz
// rapid nods 3.7-7hz
// Nadine Abu Rumman, UCL 2019 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedHeadNodding : MonoBehaviour
{

    // Define variables
    public Transform headsetCamera;
    public float noddingDepth = 0;
    public static bool mirrorNodding = false;
    public bool currentlylistening = false;
    public float nodDuration = 1.0f;
    public float nodDetectionWindow = 2.0f;
    public float minDetect = 4.0f;
    public float maxDetect = 60.0f;
    public float nodFrequency = 0.0f;
    public float frequencyThreshold = 3.6f;
    public Vector3 Offset;
   


    float noddingHeadX;
    float noddingRestHeadX;
    Animator anim;
    Transform headBone;
    Transform headBoneRest;

    private float nodThresholdPassedTime;
    private bool nodThresholdPassed;
    private float noddingWait = 0.6f;
    private float noddingMeanWait = 0.6f;
    private float nodSpeedMultiplier = 1.0f;
    private float lastNoddingDetected;
    //  private float offsetDetect = 345.0f;
    private float NextNoddingWait;
    bool NoddingDetected = false;
    bool NodAnimationPlaying = false;
    bool NodAnimationForwardPlaying = false;
    bool NodAnimationBackwardPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        currentlylistening =!AutoExperimentInstruction.Reading;
        // Get the neck bone in the rigged avatar
        anim = GetComponent<Animator>();
        headBone = anim.GetBoneTransform(HumanBodyBones.Neck);
        // Get the Headset rotation in rest position
        noddingRestHeadX = headsetCamera.rotation.eulerAngles.x;
        noddingDepth = noddingRestHeadX;
        headBone.LookAt(headsetCamera.position);
        headBone.rotation = headBone.rotation * Quaternion.Euler(Offset);
    }

    // Update is called once per frame
    void Update()
    {
        currentlylistening = !AutoExperimentInstruction.Reading;
        if (currentlylistening)
        {
            noddingHeadX = headsetCamera.rotation.eulerAngles.x;
            if (headsetCamera.rotation.eulerAngles.y >= 55 && headsetCamera.rotation.eulerAngles.y < 120)
            {
                if (NoddingDetected == false
                && nodThresholdPassed == false
                && noddingHeadX >= minDetect && noddingHeadX <= maxDetect)
                {
                    nodThresholdPassed = true;
                    nodThresholdPassedTime = Time.time;
                    noddingDepth = noddingHeadX;

                }
                /*else
                {
                    if (NoddingDetected == false
                       && nodThresholdPassed == false
                       && noddingHeadX >= offsetDetect && noddingHeadX <= 360.0f)
                    {
                        nodThresholdPassed = true;
                        nodThresholdPassedTime = Time.time;
                        noddingDepth = 15.0f;

                    }

                }*/

                if (nodThresholdPassed == true
                     && NoddingDetected == false
                     && (noddingHeadX <= minDetect || noddingHeadX >= 180.0f))
                {
                    if (Time.time - nodThresholdPassedTime <= nodDetectionWindow)
                    {
                        lastNoddingDetected = Time.time;

                        /**********************************************************************************/
                        //  To make it more natural instead of just copying the motion right after the
                        // participant, we add some randomness in wait duration before we start copying
                        /**********************************************************************************/
                        noddingWait = noddingMeanWait - 0.1f + Random.Range(0.0f, 0.2f); ;

                        /**********************************************************************************/
                        // To make it more natural instead of just copying the motion with same depth
                        // we add some randomness in the depth  
                        /**********************************************************************************/
                        noddingDepth *= Random.Range(0.9f, 1.1f);

                        /**********************************************************************************/
                        // To make it more natural instead of just copying the motion with same speed
                        // we add some randomness in the speed
                        /**********************************************************************************/
                        NextNoddingWait = lastNoddingDetected + noddingWait;
                        nodDuration = lastNoddingDetected - nodThresholdPassedTime;
                        nodDuration *= Random.Range(0.9f, 1.1f);
                        nodFrequency = 1.0f / nodDuration;


                        //nodFrequency = NodZeroCrossCount.crossNodFrequency;
                        if (nodFrequency <= frequencyThreshold)
                        {
                            NoddingDetected = true;
                            mirrorNodding = true;

                        }
                    }
                    nodThresholdPassed = false;
                }


                if (nodThresholdPassed == true && !NoddingDetected)
                {
                    if (noddingDepth < noddingHeadX && noddingHeadX < maxDetect)
                        noddingDepth = noddingHeadX;

                }

                if (NodAnimationForwardPlaying == true)
                {
                    headBone.RotateAround(headBone.position, Vector3.forward, (1.0f / (0.5f * nodDuration)) * nodSpeedMultiplier * noddingDepth * Time.deltaTime);
                    if (Time.time > NextNoddingWait + 0.5f * nodDuration)
                    {
                        NodAnimationForwardPlaying = false;
                        NodAnimationBackwardPlaying = true;
                    }
                }
                if (NodAnimationBackwardPlaying == true)
                {

                    headBone.RotateAround(headBone.position, Vector3.forward, (-1.0f / (0.5f * nodDuration)) * nodSpeedMultiplier * noddingDepth * Time.deltaTime);

                    if (Time.time > NextNoddingWait + nodDuration)
                    {
                        NodAnimationBackwardPlaying = false;
                        NoddingDetected = false;
                        mirrorNodding = false;
                        NodAnimationPlaying = false;
                        noddingDepth = 0.0f;
                        headBone.LookAt(headsetCamera.position);
                        headBone.rotation = headBone.rotation * Quaternion.Euler(Offset);
                    }
                }
                if (NoddingDetected == true && NodAnimationPlaying == false && Time.time > NextNoddingWait)
                {
                    // perform the head nodding with specific speed
                    NodAnimationPlaying = true;
                    NodAnimationForwardPlaying = true;
                }
            }
        }

    }
    private void LateUpdate()
    {

        if (!NodAnimationPlaying && !RandomNoddingWhileSpeaking.NodAnimationPlaying && currentlylistening)
        {
            headBone.LookAt(headsetCamera.position);
            headBone.rotation = headBone.rotation * Quaternion.Euler(Offset);
        }
    }
}