using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParentTransfers : MonoBehaviour
{
    private Rigidbody ballRigidBody;
    public GameObject ball;
    public GameObject dropButton;
    public GameObject endCanvas;
    public Transform cam;
    public Transform box;
    public Vector3 camPosition;
    public Quaternion camRotation;
    bool btnPressed;

    public void Attach()
    {
        if (Vector3.Distance(ball.transform.position, transform.position) <= 1)
        {
            ball.transform.SetParent(cam, true);
            camPosition = cam.position;
            camPosition.x -= 0.01f;
            camPosition.y -= 0.01f;
            camPosition.z += 0.025f;
            camRotation = cam.rotation;

            ball.transform.SetPositionAndRotation(cam.position, cam.rotation);
        }
    }

    public void Detach()
    {
        ball.transform.SetParent(null);
        Physics.gravity.Set(box.position.x, (box.position.y + 0.01f), box.position.z);
        ballRigidBody.useGravity = true;
        btnPressed = true;

    }

    private void Start()
    {
        btnPressed = false;
        ballRigidBody = ball.GetComponent<Rigidbody>();
        ballRigidBody.useGravity = false;
    }

    private void Update()
    {
        float boxCamDistance = Vector3.Distance(box.position, cam.position);
        if (boxCamDistance <= 1)
        {
            dropButton.SetActive(true);
        } else
        {
            dropButton.SetActive(false);
        }

        CalculateBoxBallDist();
    }

    private void CalculateBoxBallDist()
    {

        float boxBallDistance = Vector3.Distance(box.position, ball.transform.position);
        if (boxBallDistance <= 0.001 && btnPressed)
        {
            endCanvas.SetActive(true);
        }
    }
}
