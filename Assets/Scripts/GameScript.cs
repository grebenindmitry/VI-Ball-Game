using System.Collections;
using UnityEngine;

public class GameScript : MonoBehaviour
{

    private SceneLoader _sceneLoader;
    private TextToSpeechScript _tts;

    private void Awake()
    {
        _tts = gameObject.AddComponent<TextToSpeechScript>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(StartTts("Find the pillar, and press the button")); 
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        _sceneLoader = GetComponent<SceneLoader>();
    }

    // Update is called once per framed
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _sceneLoader.StartLoadingScene("MainMenu");
        }
    }

    // Speak the text after 0.1 seconds
    private IEnumerator StartTts(string message)
    {
        yield return new WaitForSeconds(0.1f);
        _tts.SpeakText(message);
    }
}
