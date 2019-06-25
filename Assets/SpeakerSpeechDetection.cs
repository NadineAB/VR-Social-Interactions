using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerSpeechDetection : MonoBehaviour
{

    public bool isSpeaking = false;
    public static bool Speaking = false;
    private float[] allData;
    private int allDatalen = 0;
    private int allDatalenMax = 0;
    public float lastSpeakDetected;
    public static int maxDatalen = 10000;

    /*public struct TimestampedSignal
    {
        public float time, signal;

        public TimestampedSignal(float p1, float p2)
        {
            time = p1;
            signal = p2;
        }
    }*/
    // Define variables
    //private TimestampedSignal[] signals = new TimestampedSignal[500000];
    //private int signalslen = 0;

    // Start is called before the first frame update
    void Start()
    {
        allData = new float[2*maxDatalen];
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        if (allDatalen > allDatalenMax)
            allDatalen = 0;
        for (int i = 0; i < data.Length; i++)
        {
            allData[allDatalen] = data[i];
            allDatalen++;
        //    Debug.Log(allData[i].ToString());


        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(currentTime.ToString());
        //float mean = 0.0f;
        float avgOfSquares = 0.0f;
        //float deltaI;
 
        
        // Compute squared signals.
        for (int i = 0; i < allDatalen; i++)
        {
            allData[i] *= allData[i];
        }
        
        // Compute average of squared values.
        for (int i=0; i < allDatalen; i++)
        {
            //avgEnergy += Mathf.Log(Mathf.Sqrt((allData[i] - mean) * (allData[i] - mean)));
            avgOfSquares += allData[i];
        }
        avgOfSquares /= allDatalen;

        // If deltaI as computed in the line below exceeds 0.1, we consider the participant to be speaking.
        //deltaI = avgEnergy - ((1) / (avgEnergy));
        if (allDatalenMax < allDatalen)
            allDatalenMax = allDatalen;
        //Debug.Log(allDatalen.ToString());
        if (avgOfSquares >= 0.00001f)
        {
            isSpeaking = true;
            Speaking = true;
            lastSpeakDetected = Time.time;

        }
        else
        {     if (Time.time >= lastSpeakDetected + 0.25)
            {
                isSpeaking = false;
                Speaking = false;
            }

        }
        allDatalen = 0;
    }
}