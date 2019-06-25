using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnaEnableAudio : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource Lip; 

    void Start()
    {
        Lip= gameObject.GetComponent<AudioSource>();
        Lip.enabled = false;
    }

    void Update()
    {
        if (AnnaVoiceRecogntion.helloAnnaFlag)
            Lip.enabled = true;
        else
            Lip.enabled = false;
    }
}
