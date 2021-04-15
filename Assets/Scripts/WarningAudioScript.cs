using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WarningAudioScript : MonoBehaviour
{
    public TextMeshProUGUI message;
    public GameObject okButton;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = Time.time;
        var tts = GetComponent<TextToSpeechScript>();
        tts.SpeakText(message);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.time - timer) > 5)
        {
            okButton.SetActive(true);
        }
    }
}
