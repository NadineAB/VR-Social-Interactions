using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class AnnaVoiceRecogntion : MonoBehaviour
{
    public static bool helloAnnaFlag = false;
    public static bool okayAnnaFlag = false;
    public static bool AnnaReadingFlag = false;
    public bool ReadingFlag = false;
    private KeywordRecognizer m_Recognizer;
    private Dictionary<string, Action> actions = new Dictionary <string, Action>();

    void Start()
    {
        Debug.Log("HEREEEEEEEEEEEEEEEEE Nadine");
        actions.Add("hello", Hello);
        actions.Add("okay", Okay);
        actions.Add("Read", EnableAnnaReading);
        m_Recognizer = new KeywordRecognizer(actions.Keys.ToArray());
        m_Recognizer.OnPhraseRecognized += AnnaPhraseRecognized;
        m_Recognizer.Start();
    }

    private void AnnaPhraseRecognized(PhraseRecognizedEventArgs speech)
    {
      
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void Hello()
    {
        helloAnnaFlag = true;
     //   Debug.Log(helloAnnaFlag.ToString());
    }
    private void Okay()
    {
        okayAnnaFlag = true;
    }
    private void EnableAnnaReading()
    {
        AnnaReadingFlag = true;
        ReadingFlag = AnnaReadingFlag;
        Debug.Log(AnnaReadingFlag.ToString());
    }
    void Update()
    {
      //  helloAnnaFlag = false;
      // okayAnnaFlag = false;
}
}