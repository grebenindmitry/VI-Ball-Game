using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARCore;
using UnityEngine.Experimental.XR;
using System;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARSubsystems;

class RayTracker
{
    public int framesDangerous;
    public Vector3 rayOrigin;

    public RayTracker(int i, Vector3 j)
    {
        framesDangerous = i;
        rayOrigin = j;
    }
}

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject objectToPlace;
    public GameObject placementIndicator;

    private ARRaycastManager _arRaycastManager;
    private Pose _placementPose;
    private bool _placementPoseValid = false;
    private List<RayTracker> _rayOrigins = new List<RayTracker>();

    // Start is called before the first frame update
    void Start()
    {
        _arRaycastManager = FindObjectOfType<ARRaycastManager>();
        _rayOrigins.Add(new RayTracker(0, new Vector3(Screen.width / 10, Screen.height / 10)));
        _rayOrigins.Add(new RayTracker(0, new Vector3(Screen.width / 10, Screen.height / 10 * 9)));
        _rayOrigins.Add(new RayTracker(0, new Vector3(Screen.width / 10 * 9, Screen.height / 10 * 9)));
        _rayOrigins.Add(new RayTracker(0, new Vector3(Screen.width / 10 * 9, Screen.height / 10)));
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                Screen.sleepTimeout = SleepTimeout.SystemSetting;
            }
        }
        
        UpdatePlacementPose();
        UpdatePlacementIndicator();
        if (_placementPoseValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceObject();
        }
    }

    private void PlaceObject()
    {
        Instantiate(objectToPlace, _placementPose.position, _placementPose.rotation);
    }

    private void UpdatePlacementIndicator()
    {
        if (_placementPoseValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(_placementPose.position, _placementPose.rotation);
        } else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        for (var i = 0; i < _rayOrigins.Count; i++)
        {
            var hits = new List<ARRaycastHit>();
            _arRaycastManager.Raycast(_rayOrigins[i].rayOrigin, hits, TrackableType.Planes);

            foreach (var hit in hits)
            {
                if (hit.distance <= 1)
                {
                    //Debug.Log(i + ": " + hit.distance);
                    _rayOrigins[i].framesDangerous++;
                    _placementPose = hit.pose;
                }
                else
                {
                    _rayOrigins[i].framesDangerous = 0;
                }
            }
            
            if (_rayOrigins[i].framesDangerous > 180) Debug.Log("oof");
        }
    }
}
