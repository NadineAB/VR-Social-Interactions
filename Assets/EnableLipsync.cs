using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RogoDigital.Lipsync
{
    public class EnableLipsync : MonoBehaviour
    {
        public AutoAssignLipSync AutoReading;
        public AudioSource LipSyncAudioSource;
        public LipSync LipSyncAudioData;

        // Use this for initialization
        void Start()
        {
            LipSyncAudioSource = GetComponent<AudioSource>();
            LipSyncAudioData = GetComponent<LipSync>();
            AutoReading = GetComponent<AutoAssignLipSync>();
            AutoReading.enabled = false;
        }
        // Update is called once per frame
        void Update()
        {
            if (AutoExperimentInstruction.Reading)
            {
                AutoReading.enabled = true;
                AutoReading.ReadUpdate();
            }
            else
            {
                AutoReading.enabled = false;
            }

        }

    }
}
    