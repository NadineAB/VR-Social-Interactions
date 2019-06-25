using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EyeBlinker : MonoBehaviour
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
    public float wait = 6;
    public float waitRandomMin = -0.75f;
    public float waitRanomMax = 0.75f;
    public float startDuration = 0.08f;
    public float pauseDuration = 0.05f;
    public float endDuration = 0.075f;
    public float weight;


    void Awake()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        skinnedMesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
        currentWait = wait + Random.Range(waitRandomMin, waitRanomMax);
        weight = 5.0f;
    }

    void Start()
    {
        blendShapeIndex = skinnedMesh.blendShapeCount;
    }

    void Update()
    {
        if (blinking)
        {
            if (!hasStarted)
            {
                if (Time.time < lerpEnd)
                {
                    skinnedMeshRenderer.SetBlendShapeWeight(5, 100 *(1 -
                        ((lerpEnd - Time.time) / startDuration)));
                }
                else
                {
                    hasStarted = true;
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
                    skinnedMeshRenderer.SetBlendShapeWeight(5, 100 *(lerpEnd - Time.time) / endDuration);
                }
                else
                {
                    blinking = false;
                    lastBlinkEnd = Time.time;
                    currentWait = wait
                        + Random.Range(waitRandomMin, waitRanomMax);
                    hasStarted = false;
                    hasPaused = false;
                }
            }
        }
        else
        {
            if (Time.time >= lastBlinkEnd + currentWait)
            {
                blinking = true;
                lerpEnd = Time.time + startDuration;
            }
        }
    }
}