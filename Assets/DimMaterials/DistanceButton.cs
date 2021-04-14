using System;
using UnityEngine;

public class DistanceButton : MonoBehaviour
{
    Camera cam;
    public Transform hitTransform;
    public Transform ballPos;
    private float distance;
    public GameObject ball;
    
    

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 fwd = transform.TransformDirection(Vector3.forward); to shoot forward

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "ButtonPush")
            {
                hitTransform = hit.transform;
                CalculateDistance();
            }            
        }             

    }

    private void CalculateDistance()
    {
        distance = Vector3.Distance(hitTransform.position, cam.transform.position);
        if (distance < 0.5)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                PlaceObject();
            }
            else
            {
                print(hitTransform.transform.name + " is too far away ");
            }
        }
    }

    private void PlaceObject()
    {
       Instantiate(ball, ballPos.position, ballPos.rotation);
    }
}
