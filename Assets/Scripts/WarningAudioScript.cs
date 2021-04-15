using System.Collections;
using UnityEngine;
using TMPro;

public class WarningAudioScript : MonoBehaviour
{
    public TMP_Text message;
    public GameObject okButton;

    private TextToSpeechScript _tts;
    
    private void Start()
    {
        _tts = GetComponent<TextToSpeechScript>();
        StartCoroutine(StartTts());
        StartCoroutine(EnableButton());
    }

    //Wait for .5 seconds (to allow tts to init) and start speaking
    private IEnumerator StartTts()
    {
        yield return new WaitForSeconds(0.5f);
        _tts.SpeakText(message.text);
    }

    //Enable the button after 5 seconds
    private IEnumerator EnableButton()
    {
        yield return new WaitForSeconds(5);
        okButton.SetActive(true);
    }
}
