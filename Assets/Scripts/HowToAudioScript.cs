using UnityEngine;
using TMPro;
using System.Collections;

public class HowToAudioScript : MonoBehaviour
{

    public TMP_Text message;
    private TextToSpeechScript _tts;

    // Start is called before the first frame update
    private void Start()
    {
        _tts = GetComponent<TextToSpeechScript>();
        StartCoroutine(StartTts());
    }

    //Wait for .5 seconds (to allow tts to init) and start speaking
    private IEnumerator StartTts()
    {
        yield return new WaitForSeconds(0.5f);
        _tts.SpeakText(message.text);
    }
}
