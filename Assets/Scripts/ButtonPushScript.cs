using UnityEngine;

public class ButtonPushScript : MonoBehaviour
{

    public GameObject ball;
    public GameObject pushButton;
    public Camera arCamera;
    private float _distance;
    private Touch _touch;
    private TextToSpeechScript _tts;
    
    private void Awake()
    {
       _tts = gameObject.AddComponent<TextToSpeechScript>();
    }



    // Update is called once per frame
    private void Update()
    {
        // Calculate distance
        _distance = Vector3.Distance(pushButton.transform.position, arCamera.transform.position);
        
        // if the distance is less than one meter
        if (_distance <= 1)
        {            
            GetTouchPosition();
        }        
    }

    // Shoots a Ray towards based on the coordinates of the touch in the screen
    private void ShootRayTowardsButton()
    {   
        // initialises the Ray
        var ray = arCamera.ScreenPointToRay(_touch.position);

        // If there is a hit
        if (Physics.Raycast(ray, out var hitObject))
        {
            // if it has the tag
            if (hitObject.transform.CompareTag("PushButton"))
            {
                _tts.SpeakText("Grab the ball.");
                //reveals the ball and hides the button
                ball.SetActive(true);
                pushButton.SetActive(false);
            }
        }
    }

    // Method that checks if there is touch input and calculates the position of the touch
    private void GetTouchPosition()
    {      
        // if there are inputs
        if (Input.touchCount > 0)
        {
            // Get the touch
            _touch = Input.GetTouch(0);
            
            if (_touch.phase == TouchPhase.Began)
            {
                // Shots the ray towards the button
                ShootRayTowardsButton();
            }
        }
    }

  
}
