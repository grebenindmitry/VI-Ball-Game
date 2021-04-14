using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace CO2403
{
    internal class RayContainer
    {
        private float _startTime;
        private float _prevDistance;
        public Vector3 RayOrigin { get; }
        public string Name { get; }
        private float _lastWarning;

        public RayContainer(Vector3 rayOrigin, string name)
        {
            _startTime = 0;
            RayOrigin = rayOrigin;
            Name = name;
            _lastWarning = -5;
            _prevDistance = float.MaxValue;
        }

        public bool CheckDanger(float distance)
        {
            //if less than 1m from an obstacle, warning
            if (distance < 1) return true;
            
            //if less than 5 meters and getting closer
            if (distance < 5 && _prevDistance > distance)
            {
                //if the timer is not set, set it
                if (_startTime == 0) _startTime = Time.time;
            }
            //if father than 5 meters or getting farther, reset the counter
            else _startTime = 0;
            
            //if the timer has reached 5 seconds and the last warning was more than 5 seconds later, warning
            return Time.time - _startTime > 5 && Time.time - _lastWarning > 5;
        }
    }

    public class ARTapToPlaceObject : MonoBehaviour
    {
        public GameObject objectToPlace;
        public GameObject placementIndicator;

        private ARRaycastManager _arRaycastManager;
        private TextToSpeechScript _textToSpeechScript;
        private Pose _placementPose;
        private bool _placementPoseValid;
        private readonly List<RayContainer> _rayContainers = new List<RayContainer>();

        // Start is called before the first frame update
        private void Start()
        {
            _arRaycastManager = FindObjectOfType<ARRaycastManager>();
            _textToSpeechScript = GetComponent<TextToSpeechScript>();
            
            _rayContainers.Add(new RayContainer(new Vector3(Screen.width / 10, Screen.height / 2), "Left"));
            _rayContainers.Add(new RayContainer(new Vector3(Screen.width / 10 * 9, Screen.height / 2), "Right"));
        }

        // Update is called once per frame
        private void Update()
        {
            UpdatePlacementPose();
            UpdatePlacementIndicator();
            //if the ray hit something and the screen was touched, place an object
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
            //if the ray hit something, enable the placement indicator
            if (_placementPoseValid)
            {
                placementIndicator.SetActive(true);
                placementIndicator.transform.SetPositionAndRotation(_placementPose.position, _placementPose.rotation);
            } 
            else placementIndicator.SetActive(false);
        }

        private void UpdatePlacementPose()
        {
            //cast a ray from the middle of the screen
            var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
            var hits = new List<ARRaycastHit>();
            _arRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

            //if the ray hit anything, update the pose
            _placementPoseValid = hits.Count > 0;
            if (_placementPoseValid)
            {
                _placementPose = hits[0].pose;
            }
        }
        
        private void TestProximity()
        {
            foreach (var rayOrigin in _rayContainers)
            {
                var hits = new List<ARRaycastHit>();
                _arRaycastManager.Raycast(rayOrigin.RayOrigin, hits, TrackableType.Planes);

                //if the ray didn't hit anything skip it
                if (hits.Count <= 0) continue;
                //else if the container calls for a warning, dew it
                if (rayOrigin.CheckDanger(hits[0].distance))
                {
                    _textToSpeechScript.SpeakText("Warning! Obstacle on the " + rayOrigin.Name);
                }
            }
        }
    }
}