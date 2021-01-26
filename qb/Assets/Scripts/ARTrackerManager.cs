using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Vector3 = UnityEngine.Vector3;

public class ARTrackerManager : MonoBehaviour
{
    
    
    private ARTrackedImageManager imageManager;

    [SerializeField] private TrackerListener[] listeners;
    
    private TrackerListener currentTracker;

    [SerializeField] private Button buttonExitScene;

    private Dictionary<XRReferenceImage, TrackerListener> hashListeners;
    
    private void Awake()
    {
        
        imageManager = GetComponent<ARTrackedImageManager>();
        imageManager.trackedImagesChanged += OnChanged;
        hashListeners = new Dictionary<XRReferenceImage, TrackerListener>(listeners.Length);
    }

    /// <summary>
    /// TODO: Properly delete
    /// </summary>
    /// <param name="eventArgs"></param>
    private void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        
        foreach (var trackedImage in eventArgs.added)
        {
            Transform transformTracked = trackedImage.transform;
            transformTracked.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            transformTracked.position = Vector3.zero;
            if (trackedImage.trackingState != TrackingState.Tracking) continue;
            if (currentTracker && currentTracker.Showing) continue;
            // Handle image listener
            TrackerListener nextTracker = hashListeners[trackedImage.referenceImage];
            // Check if it's another tracked image, delete previous
            // if (currentTracker && nextTracker != currentTracker)
            // {
               //  currentTracker.OnStoppingDetection();
                // currentTracker.Showing = false;
                // currentTracker.gameObject.SetActive(false);
            // }
            
            // Update everything
            currentTracker = nextTracker;
            currentTracker.Showing = true;
            currentTracker.gameObject.transform.SetParent(Camera.main.transform);
            currentTracker.gameObject.transform.localPosition = Vector3.zero;
            currentTracker.gameObject.SetActive(true);
            currentTracker.OnDetectedStart(trackedImage);
            currentTracker.OnDetectedUpdate(trackedImage);
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            if (updatedImage.trackingState == TrackingState.Tracking)
            {
                // TODO: Put this in a function
                if (currentTracker == null || currentTracker.Showing == false)
                {
                    Transform transformTracked = updatedImage.transform;
                    transformTracked.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                    transformTracked.position = Vector3.zero;
                    currentTracker = hashListeners[updatedImage.referenceImage];
                    currentTracker.Showing = true;
                    currentTracker.gameObject.transform.SetParent(Camera.main.transform);
                    currentTracker.gameObject.transform.localPosition = Vector3.zero;
                    currentTracker.gameObject.SetActive(true);
                    currentTracker.OnDetectedStart(updatedImage);
                    currentTracker.OnDetectedUpdate(updatedImage);
                }
                currentTracker.OnDetectedUpdate(updatedImage);
            }

            
        }

        foreach (var removedImage in eventArgs.removed)
        {
            // Handle removed event
            hashListeners[removedImage.referenceImage].OnStoppingDetection();
            hashListeners[removedImage.referenceImage].gameObject.SetActive(false);
            hashListeners[removedImage.referenceImage].Showing = false;
        }
    }

    private void Start()
    {
        for (int i = 0; i < listeners.Length; i++)
        {
           XRReferenceImage img = imageManager.referenceLibrary[listeners[i].imageIndex];
           hashListeners[img] = listeners[i];
           listeners[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (currentTracker && currentTracker.Showing)
        {
            buttonExitScene.gameObject.SetActive(true);
            currentTracker.ARUpdate();
        }
        else
        {
            buttonExitScene.gameObject.SetActive(false);
        }
    }

    public void StopCurrentFace()
    {
        if (!currentTracker || !currentTracker.Showing) return;
        currentTracker.OnStoppingDetection();
        currentTracker.gameObject.SetActive(false);
        currentTracker.Showing = false;
    }
}
