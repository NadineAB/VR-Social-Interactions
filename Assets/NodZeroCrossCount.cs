using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodZeroCrossCount : MonoBehaviour
{
    public static float crossNodFrequency = 0.0f;
    public float ZeroCrossNodFrequency = 0.0f;
    public float numofsecond = 1.0f;
    public struct TimestampedPitch
    {
        public float time, pitch;

        public TimestampedPitch(float p1, float p2)
        {
            time = p1;
            pitch = p2;
        }
    }
    // Define variables
    private TimestampedPitch[] pitches = new TimestampedPitch[300];
    private float[] normalisedPitches = new float[300];
    private int pitcheslen = 0;
    public Transform headsetCamera;

    float noddingHeadX;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float pitchesMax, pitchesMin, normalisedMean;
        int numCrossings;

        noddingHeadX = headsetCamera.rotation.eulerAngles.x;

        // Record new pitch
        pitches[pitcheslen] = new TimestampedPitch(Time.time, noddingHeadX);
        pitcheslen++;

        // If more than 1 second is recorded, throw out the first element and shift array
        while (pitcheslen > 1 && pitches[pitcheslen - 1].time - pitches[0].time > 1.0)
        {
            for (int i = 1; i < pitcheslen; i++)
                pitches[i - 1] = pitches[i];
            pitcheslen--;
        }

        // Normalise
        pitchesMax = pitches[0].pitch;
        pitchesMin = pitches[0].pitch;
        normalisedMean = 0.0f;
        for (int i = 0; i < pitcheslen; i++)
        {
            if (pitches[i].pitch > pitchesMax)
                pitchesMax = pitches[i].pitch;
            if (pitches[i].pitch < pitchesMin)
                pitchesMin = pitches[i].pitch;
        }
        for (int i = 0; i < pitcheslen; i++)
        {
            normalisedPitches[i] = (pitches[i].pitch - pitchesMin) / (pitchesMax - pitchesMin);
            normalisedMean += normalisedPitches[i];
        }
        normalisedMean /= pitcheslen;

        // Count crossings through mean
        numCrossings = 0;
        if (pitcheslen > 1)
        {
            for (int i = 1; i < pitcheslen; i++)
            {
                if (normalisedPitches[i] >= normalisedMean
                    && normalisedPitches[i - 1] < normalisedMean)
                    numCrossings++;

                if (normalisedPitches[i] <= normalisedMean
                    && normalisedPitches[i - 1] > normalisedMean)
                    numCrossings++;
            }
        }
        crossNodFrequency = (float)numCrossings / (2 * numofsecond);
        ZeroCrossNodFrequency = crossNodFrequency;

    }
}