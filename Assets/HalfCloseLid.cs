using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfCloseLid : MonoBehaviour
{

    int blendShapeIndex;
    SkinnedMeshRenderer skinnedMeshRenderer;
    Mesh skinnedMesh;

    float lastBlinkEnd = 0;
    float blinkStart;
    float lerpEnd;
    float currentWait;
    bool blinking;
    bool hasStarted;
    bool hasPaused;
    float eyelidFraction = 30.0f;
    public float wait = 6;
    public float waitRandomMin = -0.5f;
    public float waitRanomMax = 0.5f;
    public float startDuration = 0.08f;
    public float pauseDuration = 0.05f;
    public float endDuration = 0.075f;
    //public float weight;

    // Start is called before the first frame update
    void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        skinnedMesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
        blendShapeIndex = skinnedMesh.blendShapeCount;
        currentWait = wait + Random.Range(waitRandomMin, waitRanomMax);
        //weight = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnableReading.isLookingAtTablet)
        {
            // If Anna is looking at the tablet, she will not blink but her eyebrows will be slightly down.
            eyelidFraction = 75.0f;
            //skinnedMeshRenderer.SetBlendShapeWeight(5, 30);
        }
        else
        {
            eyelidFraction = 30.0f;
        }

        // Anna will blink at random intervals. The animation consists of 3 parts:
        // Eyelids moving downward, eyelids pausing, eyelids moving upward.
        // After the animation a new interval is selected.
        if (blinking)
        {
            if (!hasStarted)
            {
                if (Time.time < lerpEnd)
                {
                    skinnedMeshRenderer.SetBlendShapeWeight(5, eyelidFraction + ((100.0f - eyelidFraction) * (1 -
                        ((lerpEnd - Time.time) / startDuration))));
                }
                else
                {
                    hasStarted = true;
                    skinnedMeshRenderer.SetBlendShapeWeight(5, 100);
                    lerpEnd = Time.time + pauseDuration;
                }
            }
            else if (!hasPaused)
            {
                if (Time.time > lerpEnd)
                {
                    hasPaused = true;
                    lerpEnd = Time.time + endDuration;
                }
            }
            else
            {
                if (Time.time < lerpEnd)
                {
                    skinnedMeshRenderer.SetBlendShapeWeight(5, eyelidFraction + ((100.0f - eyelidFraction) * (lerpEnd - Time.time) / endDuration));
                }
                else
                {
                    blinking = false;
                    lastBlinkEnd = Time.time;
                    currentWait = wait
                        + Random.Range(waitRandomMin, waitRanomMax);
                    if (currentWait <= 0.0f)
                        currentWait = 6.0f;
                    if (currentWait >= 10.0f)
                        currentWait = 6.0f;

                    hasStarted = false;
                    hasPaused = false;
                }
            }
        }
        else
        {
            skinnedMeshRenderer.SetBlendShapeWeight(5, eyelidFraction);
            if (Time.time >= lastBlinkEnd + currentWait)
            {
                blinking = true;
                lerpEnd = Time.time + startDuration;
            }
        }
    }
}
