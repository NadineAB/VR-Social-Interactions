/* AvatarLookAt class
// capture audio in real-time from the VIVE microphone and then apply Low pass filter
// Nadine Abu Rumman, UCL 2019 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vatio.Filters;


public class ParticipantSpeaking : MonoBehaviour
{
    public static AudioSource participantaudio;
    public static bool  playAudio=true;

    public float lowPassFilterCutoff=2000.0f;
    public float lowPassFilterresonance=1.0f;
    public  int lowPassFiltersampleRate= 44100;


    // Start is called before the first frame update
    void Start()
    {
       participantaudio = GetComponent<AudioSource>();
       // foreach (string device in Microphone.devices)
       //    Debug.Log(device);
        participantaudio.clip = Microphone.Start("Microphone (VIVE Pro Mutimedia Audio)", true,1, lowPassFiltersampleRate);
        participantaudio.loop = true;
        while (!(Microphone.GetPosition(null) > 0)) { }
        Debug.Log("start playing... position is " + Microphone.GetPosition(null));
       
        lowPassFilterCutoff = 2000.0f;
        lowPassFilterresonance = 1.0f;

        lowPassFiltersampleRate = AudioSettings.outputSampleRate;

        if (playAudio)
        {
            participantaudio.Play();
        }
    }
    void OnAudioFilterRead(float[] data, int channels)
    {

       
    }
    void Update()
    {

       // float[] spectrum = new float[256];

      //  participantaudio.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        //for (int i = 1; i < spectrum.Length - 1; i++)
        //{
          //  Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
          //  Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
          //  Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
          //  Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.blue);
       // }
    }

}
