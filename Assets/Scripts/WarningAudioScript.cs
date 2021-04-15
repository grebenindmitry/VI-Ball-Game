using System.Collections;
using UnityEngine;
using TMPro;

public class WarningAudioScript : MonoBehaviour
{
    public TextMeshProUGUI message;
    public GameObject okButton;
    private float _timer;

    private TextToSpeechScript _tts;

    // Start is called before the first frame update
    private void Start()
    {
        //Holds the current time and the script for the text to speech
        _timer = Time.time;
        StartCoroutine(StartTts());
        _tts = GetComponent<TextToSpeechScript>();
    }

    private IEnumerator StartTts()
    {
        //Waits for 0.5 seconds in order for the TTS to initialise and work
        yield return new WaitForSeconds(0.5f);
        _tts.SpeakText(message);
    }
    
    // Update is called once per frame
    private void Update()
    {   
        //Waits for 5 seconds before displaying the OK button
        if (Time.time - _timer > 5)
        {
            okButton.SetActive(true);
        }
    }
}
