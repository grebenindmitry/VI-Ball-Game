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
        _timer = Time.time;
        StartCoroutine(StartTts());
        _tts = GetComponent<TextToSpeechScript>();
    }

    private IEnumerator StartTts()
    {
        yield return new WaitForSeconds(0.5f);
        _tts.SpeakText(message);
    }
    
    // Update is called once per frame
    private void Update()
    {
        if (Time.time - _timer > 5)
        {
            okButton.SetActive(true);
        }
    }
}
