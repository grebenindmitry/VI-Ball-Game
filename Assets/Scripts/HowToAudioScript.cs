using UnityEngine;
using TMPro;
public class HowToAudioScript : MonoBehaviour
{

    public TMP_Text message;

    // Start is called before the first frame update
    private void Start()
    {
        //Applies the how-to text to Text-To-Speech
        var tts = GetComponent<TextToSpeechScript>();
        tts.SpeakText(message.text);
    }
}
