using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

internal class RayContainer
{
    private float _startTime;
    private float _prevDistance;
    public Vector3 RayOrigin { get; }
    public string Name { get; }

    public RayContainer(Vector3 rayOrigin, string name)
    {
        _startTime = 0;
        RayOrigin = rayOrigin;
        Name = name;
        _prevDistance = float.MaxValue;
    }

    public bool IsWarning(float distance)
    {
        //if less than 1m from an obstacle, warning
        if (distance < 2) return true;

        //if less than 5 meters and getting closer
        if (distance < 5 && _prevDistance > distance)
        {
            //if the timer is not set, set it
            if (_startTime == 0) _startTime = Time.time;
            _prevDistance = distance;
        }
        //if father than 5 meters or getting farther, reset the counter
        else _startTime = 0;

        //if the timer has reached 5 seconds, warning
        return Time.time - _startTime > 5;
    }
}

public class ARTapToPlaceObject : MonoBehaviour
{
    private ARRaycastManager _arRaycastManager;
    private TextToSpeechScript _textToSpeechScript;
    private readonly List<RayContainer> _rayContainers = new List<RayContainer>();
    private float _lastWarning;

    // Start is called before the first frame update
    private void Start()
    {
        _arRaycastManager = FindObjectOfType<ARRaycastManager>();
        _textToSpeechScript = GetComponent<TextToSpeechScript>();
        
        _rayContainers.Add(new RayContainer(new Vector3(Screen.width / 10f, Screen.height / 2f), "Left"));
        _rayContainers.Add(new RayContainer(new Vector3(Screen.width / 10f * 9, Screen.height / 2f), "Right"));
    }

    // Update is called once per frame
    private void Update()
    {
        TestProximity();
    }

    private void TestProximity()
    {
        foreach (var rayOrigin in _rayContainers)
        {
            var hits = new List<ARRaycastHit>();
            _arRaycastManager.Raycast(rayOrigin.RayOrigin, hits, TrackableType.Planes);

            //if the ray didn't hit anything skip it
            if (hits.Count <= 0) continue;
            //else if the container calls for a warning and the last warning was more than 5 secs ago, dew it
            if (rayOrigin.IsWarning(hits[0].distance) && Time.time - _lastWarning > 5)
            {
                _textToSpeechScript.SpeakText("Warning! Obstacle on the " + rayOrigin.Name);
                _lastWarning = Time.time;
            }
        }
    }
}