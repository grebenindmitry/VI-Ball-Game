using UnityEngine;
using TMPro;
public class HowToAudioScript : MonoBehaviour
{

    public TMP_Text message;

    // Start is called before the first frame update
    void Start()
    {
        var tts = GetComponent<TextToSpeechScript>();
        tts.SpeakText(message.text);
    }
}
