using TMPro;
using UnityEngine;

public class BoxCollider : MonoBehaviour
{

    public GameObject endCanvas;
    private TextToSpeechScript _tts;
       

    private void Awake()
    {
        _tts = gameObject.AddComponent<TextToSpeechScript>();
    }

    private void OnCollisionEnter(Collision collision)
    {       
        collision.rigidbody.useGravity = false;
        
        endCanvas.SetActive(true);
        _tts.SpeakText(endCanvas.GetComponentInChildren<TMP_Text>().text + " Press one of the 2 buttons to either go back to the main menu, or restart the game.");
    }
}
