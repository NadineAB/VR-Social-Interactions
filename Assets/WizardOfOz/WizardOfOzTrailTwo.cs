using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

namespace RogoDigital.Lipsync
{
    public class WizardOfOzTrailTwo : MonoBehaviour
    {
        private bool MarylandshowOptions = false;
        public bool enableWOZ = false;
        public AudioSource LipSyncAudioSource;
        public LipSync LipSyncAudioData;
        // all the audio clips
        public AudioClip[] LipSyncDiscussionAudio = new AudioClip[30];
        public LipSyncData[] LipSyncAudioDataDiscussion = new LipSyncData[30];
        
        // Use this for initialization
        void Start()
        {
            MarylandshowOptions = false;
            LipSyncAudioSource = GetComponent<AudioSource>();
            LipSyncAudioData = GetComponent<LipSync>();
            enableWOZ = false;
        }
        // Update is called once per frame
        void Update()
        {
            if (AutoExperimentInstruction.WOZ2)
            {
                enableWOZ = true;
            }
            else
            {
                enableWOZ = false;
            }

        }

        void OnGUI()
        {
            if (enableWOZ &&
                Time.time > AutoExperimentInstruction.SecondDiscussionTimeStart-5.0f
                && Time.time < AutoExperimentInstruction.SecondDiscussionTimeEnd+2.0f)
            {
                GUI.Button(new Rect(550, 10, 300, 100), "Maryland");
                MarylandshowOptions = true;
            }
            else
            {
                MarylandshowOptions = false;
            }
            if (MarylandshowOptions == true)
            {
                GUIStyle Mainstyle = GUI.skin.GetStyle("Button");
                Mainstyle.fontSize = 20;

                //Left
                if (GUI.Button(new Rect(90, 115, 300, 100), "Jousting Is Old As Sport"))
                {

                    LipSyncAudioData.defaultClip = LipSyncAudioDataDiscussion[0];
                    LipSyncAudioSource.clip = LipSyncDiscussionAudio[0];
                    LipSyncAudioSource.Play();
                    LipSyncAudioData.Play(LipSyncAudioDataDiscussion[0]);
                }
                if (GUI.Button(new Rect(90, 225, 300, 100), "Heritage Walk"))
                {
                    LipSyncAudioData.defaultClip = LipSyncAudioDataDiscussion[1];
                    LipSyncAudioSource.clip = LipSyncDiscussionAudio[1];
                    LipSyncAudioSource.Play();
                    LipSyncAudioData.Play(LipSyncAudioDataDiscussion[1]);
                }
                if (GUI.Button(new Rect(90, 335, 300, 100), "I Like Walking"))
                {
                    LipSyncAudioData.defaultClip = LipSyncAudioDataDiscussion[2];
                    LipSyncAudioSource.clip = LipSyncDiscussionAudio[2];
                    LipSyncAudioSource.Play();
                    LipSyncAudioData.Play(LipSyncAudioDataDiscussion[2]);
                }
                if (GUI.Button(new Rect(90, 445, 300, 100), "It Seems Like a Popular Walking Path"))
                {
                    LipSyncAudioData.defaultClip = LipSyncAudioDataDiscussion[3];
                    LipSyncAudioSource.clip = LipSyncDiscussionAudio[3];
                    LipSyncAudioSource.Play();
                    LipSyncAudioData.Play(LipSyncAudioDataDiscussion[3]);
                }
                if (GUI.Button(new Rect(90, 555, 300, 100), "Walking Reduces Traffic"))
                {
                    LipSyncAudioData.defaultClip = LipSyncAudioDataDiscussion[4];
                    LipSyncAudioSource.clip = LipSyncDiscussionAudio[4];
                    LipSyncAudioSource.Play();
                    LipSyncAudioData.Play(LipSyncAudioDataDiscussion[4]);

                }
                if (GUI.Button(new Rect(90, 765, 300, 100), "Walking Is Silly"))
                {
                    LipSyncAudioData.defaultClip = LipSyncAudioDataDiscussion[7];
                    LipSyncAudioSource.clip = LipSyncDiscussionAudio[7];
                    LipSyncAudioSource.Play();
                    LipSyncAudioData.Play(LipSyncAudioDataDiscussion[7]);
                }
                // Middle
                if (GUI.Button(new Rect(400, 115, 300, 100), "WOW"))
                {
                    LipSyncAudioData.defaultClip = LipSyncAudioDataDiscussion[26];
                    LipSyncAudioSource.clip = LipSyncDiscussionAudio[26];
                    LipSyncAudioSource.Play();
                    LipSyncAudioData.Play(LipSyncAudioDataDiscussion[26]);
                }
                if (GUI.Button(new Rect(90, 665, 300, 100), "Isn't Baltimore City"))
                {
                    LipSyncAudioData.defaultClip = LipSyncAudioDataDiscussion[5];
                    LipSyncAudioSource.clip = LipSyncDiscussionAudio[5];
                    LipSyncAudioSource.Play();
                    LipSyncAudioData.Play(LipSyncAudioDataDiscussion[5]);
                }
                if (GUI.Button(new Rect(400, 225, 300, 100), "Is That All"))
                {
                    LipSyncAudioData.defaultClip = LipSyncAudioDataDiscussion[6];
                    LipSyncAudioSource.clip = LipSyncDiscussionAudio[6];
                    LipSyncAudioSource.Play();
                    LipSyncAudioData.Play(LipSyncAudioDataDiscussion[6]);
                }

                if (GUI.Button(new Rect(400, 335, 300, 100), "Yes I Guess"))
                {
                    LipSyncAudioData.defaultClip = LipSyncAudioDataDiscussion[8];
                    LipSyncAudioSource.clip = LipSyncDiscussionAudio[8];
                    LipSyncAudioSource.Play();
                    LipSyncAudioData.Play(LipSyncAudioDataDiscussion[8]);
                }
                if (GUI.Button(new Rect(400, 445, 300, 100), "Hmm"))
                {
                    LipSyncAudioData.defaultClip = LipSyncAudioDataDiscussion[9];
                    LipSyncAudioSource.clip = LipSyncDiscussionAudio[9];
                    LipSyncAudioSource.Play();
                    LipSyncAudioData.Play(LipSyncAudioDataDiscussion[9]);
                }
                if (GUI.Button(new Rect(400, 555, 300, 100), "Hmmmm"))
                {
                    LipSyncAudioData.defaultClip = LipSyncAudioDataDiscussion[10];
                    LipSyncAudioSource.clip = LipSyncDiscussionAudio[10];
                    LipSyncAudioSource.Play();
                    LipSyncAudioData.Play(LipSyncAudioDataDiscussion[10]);
                }
                if (GUI.Button(new Rect(400, 665, 300, 100), "I Do Not Know"))
                {
                    LipSyncAudioData.defaultClip = LipSyncAudioDataDiscussion[11];
                    LipSyncAudioSource.clip = LipSyncDiscussionAudio[11];
                    LipSyncAudioSource.Play();
                    LipSyncAudioData.Play(LipSyncAudioDataDiscussion[11]);
                }
                // middle
                if (GUI.Button(new Rect(710, 115, 300, 100), "Ha. I never Guessed That"))
                {
                    LipSyncAudioData.defaultClip = LipSyncAudioDataDiscussion[12];
                    LipSyncAudioSource.clip = LipSyncDiscussionAudio[12];
                    LipSyncAudioSource.Play();
                    LipSyncAudioData.Play(LipSyncAudioDataDiscussion[12]);
                }
                if (GUI.Button(new Rect(710, 225, 300, 100), "I know, Right"))
                {
                    LipSyncAudioData.defaultClip = LipSyncAudioDataDiscussion[13];
                    LipSyncAudioSource.clip = LipSyncDiscussionAudio[13];
                    LipSyncAudioSource.Play();
                    LipSyncAudioData.Play(LipSyncAudioDataDiscussion[13]);
                }
                if (GUI.Button(new Rect(710, 335, 300, 100), "Oh. Yeah That One True"))
                {
                    LipSyncAudioData.defaultClip = LipSyncAudioDataDiscussion[14];
                    LipSyncAudioSource.clip = LipSyncDiscussionAudio[14];
                    LipSyncAudioSource.Play();
                    LipSyncAudioData.Play(LipSyncAudioDataDiscussion[14]);
                }
                if (GUI.Button(new Rect(710, 445, 300, 100), "No. I Never Been There"))
                {
                    LipSyncAudioData.defaultClip = LipSyncAudioDataDiscussion[15];
                    LipSyncAudioSource.clip = LipSyncDiscussionAudio[15];
                    LipSyncAudioSource.Play();
                    LipSyncAudioData.Play(LipSyncAudioDataDiscussion[15]);
                }
                if (GUI.Button(new Rect(710, 555, 300, 100), "Thats All I Know"))
                {
                    LipSyncAudioData.defaultClip = LipSyncAudioDataDiscussion[16];
                    LipSyncAudioSource.clip = LipSyncDiscussionAudio[16];
                    LipSyncAudioSource.Play();
                    LipSyncAudioData.Play(LipSyncAudioDataDiscussion[16]);
                }
                if (GUI.Button(new Rect(710, 665, 300, 100), "What Do you Think"))
                {
                    LipSyncAudioData.defaultClip = LipSyncAudioDataDiscussion[17];
                    LipSyncAudioSource.clip = LipSyncDiscussionAudio[17];
                    LipSyncAudioSource.Play();
                    LipSyncAudioData.Play(LipSyncAudioDataDiscussion[17]);
                }

                // Right
                if (GUI.Button(new Rect(1020, 165, 300, 100), "I Love That"))
                {
                    LipSyncAudioData.defaultClip = LipSyncAudioDataDiscussion[18];
                    LipSyncAudioSource.clip = LipSyncDiscussionAudio[18];
                    LipSyncAudioSource.Play();
                    LipSyncAudioData.Play(LipSyncAudioDataDiscussion[18]);
                }
                if (GUI.Button(new Rect(1020, 275, 300, 100), "Interesting"))
                {
                    LipSyncAudioData.defaultClip = LipSyncAudioDataDiscussion[19];
                    LipSyncAudioSource.clip = LipSyncDiscussionAudio[19];
                    LipSyncAudioSource.Play();
                    LipSyncAudioData.Play(LipSyncAudioDataDiscussion[19]);
                }
                if (GUI.Button(new Rect(1020, 395, 300, 100), "Oh WOW"))
                {
                    LipSyncAudioData.defaultClip = LipSyncAudioDataDiscussion[20];
                    LipSyncAudioSource.clip = LipSyncDiscussionAudio[20];
                    LipSyncAudioSource.Play();
                    LipSyncAudioData.Play(LipSyncAudioDataDiscussion[20]);
                }
                if (GUI.Button(new Rect(1020, 515, 300, 100), "Quite Interesting"))
                {
                    LipSyncAudioData.defaultClip = LipSyncAudioDataDiscussion[21];
                    LipSyncAudioSource.clip = LipSyncDiscussionAudio[21];
                    LipSyncAudioSource.Play();
                    LipSyncAudioData.Play(LipSyncAudioDataDiscussion[21]);
                }
                if (GUI.Button(new Rect(1020, 625, 300, 100), "Quite Nice"))
                {
                    LipSyncAudioData.defaultClip = LipSyncAudioDataDiscussion[22];
                    LipSyncAudioSource.clip = LipSyncDiscussionAudio[22];
                    LipSyncAudioSource.Play();
                    LipSyncAudioData.Play(LipSyncAudioDataDiscussion[22]);
                }
                if (GUI.Button(new Rect(1020, 735, 300, 100), "That Is Beautiful"))
                {
                    LipSyncAudioData.defaultClip = LipSyncAudioDataDiscussion[23];
                    LipSyncAudioSource.clip = LipSyncDiscussionAudio[23];
                    LipSyncAudioSource.Play();
                    LipSyncAudioData.Play(LipSyncAudioDataDiscussion[23]);
                }
                if (GUI.Button(new Rect(400, 775, 300, 100), "That One Is Awesome"))
                {
                    LipSyncAudioData.defaultClip = LipSyncAudioDataDiscussion[24];
                    LipSyncAudioSource.clip = LipSyncDiscussionAudio[24];
                    LipSyncAudioSource.Play();
                    LipSyncAudioData.Play(LipSyncAudioDataDiscussion[24]);
                }
                if (GUI.Button(new Rect(710, 775, 300, 100), "Thats Funny"))
                {
                    LipSyncAudioData.defaultClip = LipSyncAudioDataDiscussion[25];
                    LipSyncAudioSource.clip = LipSyncDiscussionAudio[25];
                    LipSyncAudioSource.Play();
                    LipSyncAudioData.Play(LipSyncAudioDataDiscussion[25]);
                }
                if (GUI.Button(new Rect(1350, 355, 300, 100), "Silly"))
                {
                    LipSyncAudioData.defaultClip = LipSyncAudioDataDiscussion[27];
                    LipSyncAudioSource.clip = LipSyncDiscussionAudio[27];
                    LipSyncAudioSource.Play();
                    LipSyncAudioData.Play(LipSyncAudioDataDiscussion[27]);
                }
                if (GUI.Button(new Rect(1350, 465, 300, 100), "Very Depressing"))
                {
                    LipSyncAudioData.defaultClip = LipSyncAudioDataDiscussion[28];
                    LipSyncAudioSource.clip = LipSyncDiscussionAudio[28];
                    LipSyncAudioSource.Play();
                    LipSyncAudioData.Play(LipSyncAudioDataDiscussion[28]);
                }

            }
        }
    }
}
