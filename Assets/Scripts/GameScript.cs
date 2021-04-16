using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class GameScript : MonoBehaviour
{
    public ARSessionOrigin aRSessionOrigin;
    
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
        
        //set the sleep timeout to never sleep
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        _sceneLoader = GetComponent<SceneLoader>();
        
        SetEyeLevel();
    }
    
    private void SetEyeLevel()
    {
        var arSessionTransform = aRSessionOrigin.transform;
        //get the player height from the prefs, subtract 10 to account for eye level and convert to meters
        var playerEyeYPos = (PlayerPrefs.GetInt("playerHeight", 175) - 10) / 100.0f;
        
        //set the height of the ARSessionOrigin to the eye level
        var arSessionOriginNewPos = arSessionTransform.position;
        arSessionOriginNewPos.y = playerEyeYPos;
        arSessionTransform.position = arSessionOriginNewPos;
    }
    
    private void Update()
    {
        //if the back button is pressed go to main menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _sceneLoader.StartLoadingScene("MainMenu");
        }
    }

    // Speak the text after 0.1 seconds
    private IEnumerator StartTts(string message)
    {
        yield return new WaitForSeconds(1f);
        _tts.SpeakText(message);
    }
}
