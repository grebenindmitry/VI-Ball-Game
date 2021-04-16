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

    // Method that resolves collitions
    private void OnCollisionEnter(Collision collision)
    {       
        // anythig that collider with the boxes collider looses its gravity
        collision.rigidbody.useGravity = false;
        
        //activate canvas and sent audio message to player
        endCanvas.SetActive(true);
        _tts.SpeakText(endCanvas.GetComponentInChildren<TMP_Text>().text + " Press one of the 2 buttons to either go back to the main menu, or restart the game.");
    }
}
