using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTrackerManager : MonoBehaviour
{
    private ARTrackedImageManager _arTrackedImageManager;
    private GameObject _covidRunPrefab;

    private void Awake()
    {
        _arTrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        _covidRunPrefab = _arTrackedImageManager.trackedImagePrefab;
    }

    public void OnEnable()
    {
        _arTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }

    void Start()
    {
        _arTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs args)
    {
        // Do the thingity thingyties
        Debug.Log("xdd");

    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            // image is tracking or tracking with limited state, show visuals and update it's position and rotation
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                _covidRunPrefab.SetActive(true);
                _covidRunPrefab.transform.SetPositionAndRotation(trackedImage.transform.position, trackedImage.transform.rotation);
            }
            // image is no longer tracking, disable visuals TrackingState.Limited TrackingState.None
            else
            {
                _covidRunPrefab.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
