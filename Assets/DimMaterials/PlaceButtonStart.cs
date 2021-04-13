using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARCore;
using UnityEngine.Experimental.XR;
using System;

using UnityEngine.XR.ARSubsystems;

public class PlaceButtonStart : MonoBehaviour
{
    public GameObject objectToPlace; 
    private bool isCreated = false;
    public Vector3 positionBtn = new Vector3(0.1f, 0.2f, 0.2f);
    public Quaternion rotation = new Quaternion(0f, 0f, 0f, 0f);


    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {

        if (!isCreated)
        {
            PlaceObject();
        }
    }

    private void PlaceObject()
    {
        Instantiate(objectToPlace, positionBtn, rotation);
    }




}


