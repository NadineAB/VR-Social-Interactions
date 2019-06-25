using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

namespace RogoDigital.Lipsync
{
public class AutoAssignLipSync : MonoBehaviour
  {
    
    public bool LipSyncReading=false;
    public AudioSource LipSyncAudioSource;
    public AudioClip LipSyncAudio;
    public LipSync LipSyncAudioData;
    public LipSyncData LipSyncAudioDataSpeaker;
    public EnableLipsync AutoEnable;
    public EnableReading IsReadingLid;
    private static AudioClip infoClip;
    private static LipSyncData infoData;

        // Start is called before the first frame update
        void Start()
    {
            LipSyncReading = AutoExperimentInstruction.Reading;
            LipSyncAudioSource = GetComponent<AudioSource>();
            LipSyncAudioData = GetComponent<LipSync>();
            AutoEnable = GetComponent<EnableLipsync>();
            infoClip = LipSyncAudio;
            infoData = LipSyncAudioDataSpeaker;
         //   AutoEnable.enabled = false;
        }

    // Update is called once per frame
    public void  ReadUpdate()
    {
       
            LipSyncReading = AutoExperimentInstruction.Reading;
            LipSyncAudioSource = GetComponent<AudioSource>();
            LipSyncAudioData = GetComponent<LipSync>();
            AutoEnable = GetComponent<EnableLipsync>();
            infoClip = LipSyncAudio;
            infoData = LipSyncAudioDataSpeaker;
            AutoEnable.enabled = false;

            LipSyncReading = AutoExperimentInstruction.Reading;
            if (LipSyncReading)
            {
                LipSyncAudioData.defaultClip = infoData;
                LipSyncAudioSource.clip = infoClip;
                LipSyncAudioData.Play(infoData);
                LipSyncAudioSource.Play();
            }
            else
            {
                 LipSyncAudio= null;
                 LipSyncAudioData = null;
                 AutoEnable.enabled = false;

            }
   
        }
}
}
