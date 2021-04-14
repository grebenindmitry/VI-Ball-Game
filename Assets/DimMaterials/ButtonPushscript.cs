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

    Vector2 touchPos = default;

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(button.transform.position, arCamera.transform.position);
        if (distance < 1)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                touchPos = touch.position;

                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = arCamera.ScreenPointToRay(touch.position);
                    RaycastHit hitObject;
                    if (Physics.Raycast(ray, out hitObject))
                    {
                        PlacementObject placementObject = hitObject.transform.GetComponent<PlacementObject>();
                        if (placementObject != null)
                        {
                            ball.SetActive(true);
                            button.SetActive(false);
                        }
                    }
                }
            }
        }
    }
}
