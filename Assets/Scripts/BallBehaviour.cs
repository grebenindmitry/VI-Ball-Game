using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public GameObject ball;
    public GameObject dropButton;
    public GameObject pickButton;
    public Camera arCamera;
    public GameObject box;
    
    private bool _ballPicked;
    private Rigidbody _ballRigidBody;
    private TextToSpeechScript _tts;

    private void Awake()
    {
        _tts = gameObject.AddComponent<TextToSpeechScript>();
    }

    public void Attach()
    {
        // if the distance 
        if (Vector3.Distance(ball.transform.position, arCamera.transform.position) <= 1.2)
        {
            ball.transform.SetParent(arCamera.transform);

            ball.transform.localPosition = new Vector3(-0.11f, -0.06f, 0.3f);

            _ballPicked = true;
            pickButton.SetActive(false);
            _tts.SpeakText("Take the ball to the box.");
        }
    }

    public void Detach()
    {
        ball.transform.SetParent(null);
        var boxPosition = box.transform.position;
        boxPosition.y += 0.3f;       
        ball.transform.position = boxPosition;
        _ballRigidBody.useGravity = true;
        dropButton.SetActive(false);        
    }

    private void Start()
    {
        var collider = box.GetComponent<Collider>();
        
        _ballPicked = false;
        _ballRigidBody = ball.GetComponent<Rigidbody>();
        _ballRigidBody.useGravity = false;
    }

    private void Update()
    {
        CalculateBoxCamDist();
        CalculateBallCamDist();
    }


    private void CalculateBoxCamDist()
    {
        var boxPos = box.transform.position;
        boxPos.y = 0;

        var cameraPos = arCamera.transform.position;
        cameraPos.y = 0;
        
        dropButton.SetActive(Vector3.Distance(boxPos, cameraPos) <= 0.6);
    }

    private void CalculateBallCamDist()
    {        
        if (ball.activeSelf && !_ballPicked)
        {
            var ballCamDistance = Vector3.Distance(ball.transform.position, arCamera.transform.position);
            pickButton.SetActive(ballCamDistance <= 1);
        }
    }
}
