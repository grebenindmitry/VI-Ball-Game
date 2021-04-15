using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HowToAudioScript : MonoBehaviour
{

    public TextMeshProUGUI message;

    // Start is called before the first frame update
    void Start()
    {
        //Applies the how-to text to Text-To-Speech
        var tts = GetComponent<TextToSpeechScript>();
        tts.SpeakText(message);
    }
}
