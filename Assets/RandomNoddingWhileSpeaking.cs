using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNoddingWhileSpeaking : MonoBehaviour
{
    // Define variables
    public bool currentlylistening = false;
    public Transform headsetCamera;
    public float noddingDepth = 10.0f;
    public float noddingDuration = 1.0f;
    public float doubleNoddingDuration = 0.5f; // Due to implementation, actual duration is 0.85 times this value (in seconds)
    public float haveBeenSpeakingWindow = 2.0f; // Time that participant needs to speak to enable one nod (if time out window is not enabled)
    public float timeOutDuration = 5.0f; // Time out after having performed one nod.
    public bool timeOutActivated = false;
    public float timeOutLastActivated = 0.0f;
    public Vector3 Offset;
    public bool haveBeenSpeaking = false;
    public float speakingStarted = 0.0f;
    public float speakingWindowPassedTime = 0.0f;


    Animator anim;
    Transform headBone;
    Transform headBoneRest;

    private float nodSpeedMultiplier = 1.0f;
    private float randomNoddingTime;
    private float nodStarted;
    private bool nodWillPlay;
    private bool doubleNodWillPlay;

    public static bool NodAnimationPlaying = false;
    bool NodAnimationForwardPlaying = false;
    bool NodAnimationBackwardPlaying = false;
    bool doubleNodAnimationForwardPlaying = false;
    bool doubleNodAnimationBackwardPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        currentlylistening = !AutoExperimentInstruction.Reading;
        nodWillPlay = false;
        // Get the neck bone in the rigged avatar
        anim = GetComponent<Animator>();
        headBone = anim.GetBoneTransform(HumanBodyBones.Neck);
        // Get the Headset rotation in rest position
        headBone.LookAt(headsetCamera.position);
        headBone.rotation = headBone.rotation * Quaternion.Euler(Offset);
    }

    // Update is called once per frame
    void Update()
    {
        // We activate the timeout mode at the point Anna _starts_ listening.
        if (AutoExperimentInstruction.Reading == currentlylistening)
        {
            timeOutActivated = true;
            timeOutLastActivated = Time.time;
        }
        currentlylistening = !AutoExperimentInstruction.Reading;

        if (timeOutActivated && Time.time - timeOutLastActivated >= timeOutDuration)
            timeOutActivated = false;

        if (currentlylistening)
        {
            haveBeenSpeaking = SpeakerSpeechDetection.Speaking;
            if (!DelayedHeadNodding.mirrorNodding)
            {
                if (!NodAnimationPlaying)
                {
                    if (haveBeenSpeaking == false)
                    {
                        speakingStarted = 0.0f;
                    }

                    if (speakingStarted == 0.0f && haveBeenSpeaking == true)
                        speakingStarted = Time.time;

                    // A nod will start if:
                    // i. it has not already started
                    // ii. We are not within the first second of the game
                    // iii. Time out mode is not activated
                    // iv. The participant has been speaking for an amount of time that is at least haveBeenSpeakingWindow
                    if (!nodWillPlay && speakingStarted > 1.0f && !timeOutActivated && Time.time - speakingStarted >= haveBeenSpeakingWindow)
                    {
                       // Debug.Log(Time.time - speakingStarted);
                        // Start a normal nod with 0% probability and do a double nod with 100% probability.
                        if (Random.Range(0.0f, 1.0f) < 1.0f)
                        {
                            doubleNodWillPlay = true;
                        }
                        else
                        {
                            doubleNodWillPlay = false;
                        }
                        nodWillPlay = true;
                        speakingWindowPassedTime = Time.time;
                        randomNoddingTime = Random.Range(0.15f, 0.45f); // generate the random timing 
                                                                        // of the nodding while speaking

                        // Activate time out mode, i.e., wait at least timeOutDuration seconds before the next nod will play.
                        timeOutActivated = true;
                        timeOutLastActivated = Time.time;
                    }

                    if (nodWillPlay && Time.time - speakingWindowPassedTime >= randomNoddingTime)
                    {
                        nodStarted = Time.time;
                        speakingStarted = 0.0f;

                        NodAnimationPlaying = true;
                        NodAnimationForwardPlaying = true;
                        noddingDepth = Random.Range(7.0f, 15.0f);
                    }
                }

                if (NodAnimationPlaying)
                {
                    if (!doubleNodWillPlay)
                    {
                        // Normal Nod Animation
                        if (NodAnimationForwardPlaying == true)
                        {
                            headBone.RotateAround(headBone.position, Vector3.forward, (1.0f / (0.5f * noddingDuration)) * nodSpeedMultiplier * noddingDepth * Time.deltaTime);

                            if (Time.time - nodStarted > 0.5f * noddingDuration)
                            {
                                NodAnimationForwardPlaying = false;
                                NodAnimationBackwardPlaying = true;
                            }
                        }
                        if (NodAnimationBackwardPlaying == true)
                        {
                            headBone.RotateAround(headBone.position, Vector3.forward, (-1.0f / (0.5f * noddingDuration)) * nodSpeedMultiplier * noddingDepth * Time.deltaTime);

                            if (Time.time - nodStarted > noddingDuration)
                            {
                                nodWillPlay = false;
                                NodAnimationBackwardPlaying = false;
                                NodAnimationPlaying = false;
                                noddingDepth = 0.0f;
                                speakingStarted = 0.0f;
                                //  speakingWindowPassedTime = 0.0f;
                                headBone.LookAt(headsetCamera.position);
                                headBone.rotation = headBone.rotation * Quaternion.Euler(Offset);
                            }
                        }
                    }
                    else
                    {
                        // Double Nod Animation, consisting of 4 movements: 
                        // 1. first forward head rotation 
                        // 2. quick head rotation backward 
                        // 3. second deeper forward head rotation 
                        // 4. backward head rotation to original position
                        noddingDepth = Random.Range(3.0f, 12.0f);
                        if (NodAnimationForwardPlaying == true)
                        {
                            headBone.RotateAround(headBone.position, Vector3.forward, (1.0f / (0.2f * doubleNoddingDuration)) * nodSpeedMultiplier * (0.4f * noddingDepth) * Time.deltaTime);

                            if (Time.time - nodStarted > 0.2f * noddingDuration)
                            {
                                NodAnimationForwardPlaying = false;
                                NodAnimationBackwardPlaying = true;
                            }
                        }
                        if (NodAnimationBackwardPlaying == true)
                        {
                            headBone.RotateAround(headBone.position, Vector3.forward, (-1.0f / (0.1f * doubleNoddingDuration)) * nodSpeedMultiplier * (0.2f * noddingDepth) * Time.deltaTime);

                            if (Time.time - nodStarted > 0.3f * noddingDuration)
                            {
                                NodAnimationBackwardPlaying = false;
                                doubleNodAnimationForwardPlaying = true;
                            }
                        }
                        if (doubleNodAnimationForwardPlaying == true)
                        {
                            headBone.RotateAround(headBone.position, Vector3.forward, (1.0f / (0.3f * doubleNoddingDuration)) * nodSpeedMultiplier * (0.3f * noddingDepth) * Time.deltaTime);

                            if (Time.time - nodStarted > 0.6f * noddingDuration)
                            {
                                doubleNodAnimationForwardPlaying = false;
                                doubleNodAnimationBackwardPlaying = true;
                            }
                        }
                        if (doubleNodAnimationBackwardPlaying == true)
                        {
                            headBone.RotateAround(headBone.position, Vector3.forward, (-1.0f / (0.15f * doubleNoddingDuration)) * nodSpeedMultiplier * (0.5f * noddingDepth) * Time.deltaTime);

                            if (Time.time - nodStarted > 0.75f * noddingDuration)
                            {
                                nodWillPlay = false;
                                doubleNodAnimationBackwardPlaying = false;
                                NodAnimationPlaying = false;
                                noddingDepth = 0.0f;
                                speakingStarted = 0.0f;
                                headBone.LookAt(headsetCamera.position);
                                headBone.rotation = headBone.rotation * Quaternion.Euler(Offset);
                            }
                        }
                    }
                }
            }
        }
    }

    private void LateUpdate()
    {

        if (!NodAnimationPlaying && !DelayedHeadNodding.mirrorNodding && currentlylistening)
        {
            headBone.LookAt(headsetCamera.position);
            headBone.rotation = headBone.rotation * Quaternion.Euler(Offset);
        }
    }
}