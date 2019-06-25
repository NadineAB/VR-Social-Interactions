using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script makes Anna switch between looking at the tablet and looking at the headset camera, at random intervals as can be specified in the variables.
// It only performs this switching when "reading mode" is enabled, as kept track of by the variables Reading and currentlyReading below.
public class EnableReadingOnly : MonoBehaviour
{
    // public static bool Reading = true;
    public bool currentlyReading = false;
    public Transform headsetCamera;
    public Transform tablet;
    public Vector3 Offset;
    public bool animationPlaying = false;
    public bool animationDownwardPlaying = false;
    public bool animationUpwardPlaying = false;

    public float animationStarted = 0.0f;

    // The duration of the animation can be set here, but it is randomly adjusted by a multiplier between aSMLB and aSMUB everytime Anna switches.
    public float animationDuration = 0.5f;


    // The animationSpeedMultiplier will be a random value between aSMLB and aSMUB, reset every time Anna makes a switch.
    public float animationSpeedMultiplier = 1.0f;

    // lastSwitch tracks when was the last time that Anna switched between looking at the camera and the tablet.
    public float lastSwitch = 0.0f;
    // isLookingAtTablet indicates whether Anna is looking at the tablet (if false, she is looking at the person).
    public bool isLookingAtTablet = false;

    // switchAfter stores the time it will take since last switch, until the next switch will happen.
    // This value is set after each switch to a random value between intervalLB and intervalUB.
    public float switchAfter = 50.0f;
  
    Animator anim;
    Transform headBone;
    Transform SplineBone;
    Transform ChestBone;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        headBone = anim.GetBoneTransform(HumanBodyBones.Neck);
        ChestBone = anim.GetBoneTransform(HumanBodyBones.Chest);
        SplineBone = anim.GetBoneTransform(HumanBodyBones.Spine);
        // Get the Headset rotation in rest position
        headBone.LookAt(headsetCamera.position);
        headBone.rotation = headBone.rotation * Quaternion.Euler(Offset);
        currentlyReading = AutoExperimentInstruction.Reading;
        if (currentlyReading)
        {
            // She is looking at the tablet initially.
            isLookingAtTablet = true;
            animationPlaying = true;
            animationDownwardPlaying = true;
            switchAfter = Random.Range(5.0f, 10.0f);

        }

    }

    // Update is called once per frame
    void Update()
    {
        currentlyReading = AutoExperimentInstruction.Reading;

        // First, check if we are in the process of playing an animation where Anna is switching headbone position.
        if (animationPlaying)
        {

            // Check if we need to stop the animation.
            if (animationStarted + (animationDuration * animationSpeedMultiplier) < Time.time)
            {
                animationPlaying = false;
                animationUpwardPlaying = false;
                animationDownwardPlaying = false;
            }

            if (animationUpwardPlaying)
            {
                // The 23.1f is the angle at which the tablet is located. Currently hardcoded.
                SplineBone.RotateAround(SplineBone.position, Vector3.forward, 1.5f * Time.deltaTime / (animationDuration * animationSpeedMultiplier));
                ChestBone.RotateAround(ChestBone.position, Vector3.forward, 0.5f * Time.deltaTime / (animationDuration * animationSpeedMultiplier));
                headBone.RotateAround(headBone.position, Vector3.forward, -23.1f * Time.deltaTime / (animationDuration * animationSpeedMultiplier));

            }
            else
            {
                SplineBone.RotateAround(SplineBone.position, Vector3.forward, -1.5f * Time.deltaTime / (animationDuration * animationSpeedMultiplier));
                ChestBone.RotateAround(ChestBone.position, Vector3.forward, -0.5f * Time.deltaTime / (animationDuration * animationSpeedMultiplier));
                headBone.RotateAround(headBone.position, Vector3.forward, 23.1f * Time.deltaTime / (animationDuration * animationSpeedMultiplier));

            }
        }
        // If no animation is playing, check if we need to switch.
        else if (currentlyReading)
        {
            animationUpwardPlaying = false;
            animationDownwardPlaying = false;
            animationPlaying = false;

            // Make sure that Anna is looking at the correct object.
            if (isLookingAtTablet)
            {
                // In case she is looking at the tablet, there is no adjustment in the current version of this script. 

                // headBone.LookAt(tablet.position);
                // headBone.rotation = headBone.rotation * Quaternion.Euler(Offset);
            }
            else
            {
                // In case she is looking at the headsetCamera, we make sure that she is looking straight into the camera.
                headBone.LookAt(headsetCamera.position);
                headBone.rotation = headBone.rotation * Quaternion.Euler(Offset);
            }

            // Check if we need to switch between looking at the camera and the tablet.
            if (lastSwitch + switchAfter < Time.time)
            {
                // If we indeed need to switch, we sample a new time for switchAfter and we set lastSwitch to the current time.
                switchAfter = 50f;
                lastSwitch = Time.time;

                // Start the animation.
                animationPlaying = true;
                animationStarted = Time.time;

                // Now, check if we need to look at camera or look at tablet.
                if (isLookingAtTablet)
                {
                    animationUpwardPlaying = true;
                    isLookingAtTablet = false;

                }
                else
                {
                    animationDownwardPlaying = true;
                    isLookingAtTablet = true;
                }
            }




            //currentlyReading = Reading;
        }

    }
}