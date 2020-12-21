using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Vector3 = UnityEngine.Vector3;

public class ARTrackerManager : MonoBehaviour
{
    
    
    private ARTrackedImageManager imageManager;

    [SerializeField] private TrackerListener[] listeners;


    private TrackerListener currentTracker;

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
            // Handle added event
            transformTracked.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            transformTracked.position = Vector3.zero;
            currentTracker = hashListeners[trackedImage.referenceImage];
            currentTracker.gameObject.transform.SetParent(Camera.main.transform);
            currentTracker.gameObject.transform.localPosition = Vector3.zero;
            currentTracker.gameObject.SetActive(true);
            currentTracker.OnDetectedStart(trackedImage);
            currentTracker.OnDetectedUpdate(trackedImage);
            
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            currentTracker.OnDetectedUpdate(updatedImage);
        }

        foreach (var removedImage in eventArgs.removed)
        {
            // Handle removed event
            hashListeners[removedImage.referenceImage].OnStoppingDetection();
            hashListeners[removedImage.referenceImage].gameObject.SetActive(false);
            currentTracker = null;
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
        if (currentTracker)
        {
            currentTracker.ARUpdate();
        }
    }
}
