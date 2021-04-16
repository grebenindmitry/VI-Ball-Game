using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public GameObject ball;
    public GameObject dropButton;
    public GameObject pickButton;
    public Camera arCamera;
    public GameObject box;
    
    private bool _ballPicked;
    private bool _ballDropped;
    private Rigidbody _ballRigidBody;
    private TextToSpeechScript _tts;

    private void Awake()
    {
        // initialise the text to speach object
        _tts = gameObject.AddComponent<TextToSpeechScript>();
    }

    public void Attach()
    {
        // if the distance 
        if (Vector3.Distance(ball.transform.position, arCamera.transform.position) <= 1.2)
        {
            // Sets camera as parent of ball
            ball.transform.SetParent(arCamera.transform);

            // Place camera bottom left of the screen
            ball.transform.localPosition = new Vector3(-0.11f, -0.06f, 0.3f);

            // deactivate the button and sent audio message to player
            _ballPicked = true;
            pickButton.SetActive(false);
            _tts.SpeakText("Take the ball to the box.");
        }
    }

    public void Detach()
    {
        // detach ball from camera
        ball.transform.SetParent(null);

        // place the ball 1 unit(meter) above the box 
        var boxPosition = box.transform.position;
        boxPosition.y += 1.0f;       
        ball.transform.position = boxPosition;

        // activate gravity for ball
        _ballRigidBody.useGravity = true;
        _ballDropped = true;

        // deactivite the throw button
        dropButton.SetActive(false);        
    }

    private void Start()
    {
        // initialize variables       
        _ballDropped = false;
        _ballPicked = false;
        _ballRigidBody = ball.GetComponent<Rigidbody>();
        _ballRigidBody.useGravity = false;
    }

    private void Update()
    {
        // calculate distances to enable and diable objects
        CalculateBoxCamDist();
        CalculateBallCamDist();
    }


    private void CalculateBoxCamDist()
    {
        var boxPos = box.transform.position;
        boxPos.y = 0;

        var cameraPos = arCamera.transform.position;
        cameraPos.y = 0;

        //Calculate distance
        var boxCamDistance = Vector3.Distance(boxPos, cameraPos);

        // if the ball did not drop yet
        if (!_ballDropped)
        {
            // if the distance is less than 0.6 meters enebale/diable
            dropButton.SetActive((boxCamDistance <= 0.6));
        }
        
        
    }

    private void CalculateBallCamDist()
    {        
        // If ball is revealed and not picked yet
        if (ball.activeSelf && !_ballPicked)
        {
            // Calculate distance
            var ballCamDistance = Vector3.Distance(ball.transform.position, arCamera.transform.position);

            // If the distance is less than 1 meter enable button else disable it
            pickButton.SetActive(ballCamDistance <= 1);
        }
    }
}
