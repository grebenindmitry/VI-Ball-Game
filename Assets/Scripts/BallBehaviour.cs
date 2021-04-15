using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public GameObject ball;
    public GameObject dropButton;
    public GameObject pickButton;
    public GameObject endCanvas;
    public Camera arCamera;
    public GameObject box;
    
    private Vector3 _camPosition;
    private bool _ballReleased;
    private bool _ballPicked;
    private Rigidbody _ballRigidBody;

    public void Attach()
    {
        // if the distance 
        if (Vector3.Distance(ball.transform.position, arCamera.transform.position) <= 1.2)
        {
            ball.transform.SetParent(arCamera.transform);
            _camPosition = arCamera.transform.position;
            _camPosition.x -= 0.1f;
            _camPosition.y -= 0.1f;
            _camPosition.z += 0.3f;

            ball.transform.SetPositionAndRotation(_camPosition, arCamera.transform.rotation);

            _ballPicked = true;
            pickButton.SetActive(false);
        }
    }

    public void Detach()
    {
        ball.transform.SetParent(null);
        var boxPosition = box.transform.position;
        Physics.gravity.Set(boxPosition.x, boxPosition.y + 0.01f, boxPosition.z);
        _ballRigidBody.useGravity = true;
        _ballReleased = true;
    }

    private void Start()
    {
        _ballPicked = false;
        _ballReleased = false;
        _ballRigidBody = ball.GetComponent<Rigidbody>();
        _ballRigidBody.useGravity = false;
    }

    private void Update()
    {
        CalculateBoxCamDist();
        CalculateBoxBallDist();
        CalculateBallCamDist();
    }

    private void CalculateBoxBallDist()
    {
        if (ball.activeSelf && _ballReleased)
        {
            var boxBallDistance = Vector3.Distance(box.transform.position, ball.transform.position);
            if (boxBallDistance <= 0.001 )
            {
                endCanvas.SetActive(true);
            }
        }
    }

    private void CalculateBoxCamDist()
    {
        var boxCamDistance = Vector3.Distance(box.transform.position, arCamera.transform.position);
        dropButton.SetActive(boxCamDistance <= 1);
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
