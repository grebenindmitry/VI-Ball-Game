using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class ImageRecogniser : MonoBehaviour
{
    private ARTrackedImageManager aRTrackedImagemanager;

    private void Awake()
    {
        aRTrackedImagemanager = FindObjectOfType<ARTrackedImageManager>();
    }

    public void OnEnable()
    {
        aRTrackedImagemanager.trackedImagesChanged += OnImageChanged;
    }

    public void OnDisable()
    {
        aRTrackedImagemanager.trackedImagesChanged -= OnImageChanged;

    }


    public void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImage in args.added)
        {
            Debug.Log(trackedImage.name);
        }
    }



}
