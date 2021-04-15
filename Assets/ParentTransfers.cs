using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParentTransfers : MonoBehaviour
{
    private Rigidbody ballRigidBody;
    public GameObject ball;
    public GameObject dropButton;
    public GameObject pickButton;
    public GameObject endCanvas;
    public Transform cam;
    public Transform box;
    public Vector3 camPosition;
    private bool ballReleaced;
    private bool ballPicked;

    public void Attach()
    {
        // if the distance 
        if (Vector3.Distance(ball.transform.position, transform.position) <= 1.2)
        {
            ball.transform.SetParent(cam, true);
            camPosition = cam.position;
            camPosition.x -= 0.01f;
            camPosition.y -= 0.01f;
            camPosition.z += 0.2f;
            

            ball.transform.SetPositionAndRotation(camPosition, cam.rotation);

            ballPicked = true;
            pickButton.SetActive(false);
        }
    }

    public void Detach()
    {
        ball.transform.SetParent(null);
        Physics.gravity.Set(box.position.x, (box.position.y + 0.01f), box.position.z);
        ballRigidBody.useGravity = true;
        ballReleaced = true;
    }

    private void Start()
    {
        ballPicked = false;
        ballReleaced = false;
        ballRigidBody = ball.GetComponent<Rigidbody>();
        ballRigidBody.useGravity = false;
    }

    private void Update()
    {
        CalculateBoxCamDist();
        CalculateBoxBallDist();
        CalculateBallCamDist();
    }

    private void CalculateBoxBallDist()
    {
        if (ball.activeSelf && ballReleaced)
        {
            float boxBallDistance = Vector3.Distance(box.position, ball.transform.position);
            if (boxBallDistance <= 0.001 )
            {
                endCanvas.SetActive(true);
            }
        }
    }

    private void CalculateBoxCamDist()
    {
        float boxCamDistance = Vector3.Distance(box.position, cam.position);
        if (boxCamDistance <= 1)
        {
            dropButton.SetActive(true);
        }
        else
        {
            dropButton.SetActive(false);
        }                  
    }

    private void CalculateBallCamDist()
    {        
        if (ball.activeSelf && !ballPicked)
        {
            float ballCamDistance = Vector3.Distance(ball.transform.position, cam.position);
            if (ballCamDistance <= 1 )
            {
                pickButton.SetActive(true);                
            }
            else
            {
                pickButton.SetActive(false);               
            }
        }
    }
}
