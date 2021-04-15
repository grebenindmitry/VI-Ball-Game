using UnityEngine;

public class ButtonPushScript : MonoBehaviour
{

    public GameObject ball;
    public GameObject pushButton;
    public Camera arCamera;
    private float _distance;
    private Touch _touch;

    private TextToSpeechScript _oof;
    
    private void Awake()
    {
       _oof = pushButton.AddComponent<TextToSpeechScript>();
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
            _oof.ShowToast("hit");
            // Gets the PlacementObject class if it has one
            var placementObject = hitObject.transform.GetComponent<PlacementObject>();

            // if it has the class
            if (!(placementObject is null))
            {
                _oof.ShowToast("not is null");
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
