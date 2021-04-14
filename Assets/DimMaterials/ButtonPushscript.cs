using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ButtonPushscript : MonoBehaviour
{

    public GameObject ball;
    public GameObject button;  
    private float distance;
    public Camera arCamera;
    Touch touch;

    Vector2 touchPos = default;

    // Update is called once per frame
    void Update()
    {
        // Calculate distance
        distance = Vector3.Distance(button.transform.position, arCamera.transform.position);

        // if the distance is less than one meter
        if (distance <= 1)
        {
            GetTouchPosition();
        } 
    }

    // Shoots a Ray torwards based on the coordinates of the touch in the screen
    private void ShootRayTorwardsButton()
    {   
        // initialises the Ray
        Ray ray = arCamera.ScreenPointToRay(touch.position);
        // initializes the object that is hit
        RaycastHit hitObject;

        // If there is a hit
        if (Physics.Raycast(ray, out hitObject))
        {
            // Gets the PlacementObject class if it has one
            PlacementObject placementObject = hitObject.transform.GetComponent<PlacementObject>();

            // if it has the class
            if (placementObject != null)
            {
                //reveals the ball and hides the button
                ball.SetActive(true);
                button.SetActive(false);
            }
        }
    }

    // Method that checks if there is touch input and calculates the porsition of the touch
    private void GetTouchPosition()
    {      
        // if there are inputs
        if (Input.touchCount > 0)
        {
            // Get the touch
            touch = Input.GetTouch(0);

            // Get the position of the position
            touchPos = touch.position;

            // 
            if (touch.phase == TouchPhase.Began)
            {
                // Shots the ray torwards the button
                ShootRayTorwardsButton();
            }
        }
    }
}
