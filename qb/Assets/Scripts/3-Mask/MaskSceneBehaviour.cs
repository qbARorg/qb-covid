using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class MaskSceneBehaviour : MonoBehaviour
{
    
    #region Public
    
    #endregion
    
    #region Private

    [SerializeField] private Head head;
    
    private ARRaycastManager raycastManager;
    private ARTrackedImageManager imageManager;
    
    [SerializeField]
    private GameObject mask;

    private GameObject maskInstance;
    private Camera mainCamera;
    
    #endregion

    #region Callbacks

    

    void OnDisable() => imageManager.trackedImagesChanged -= OnChanged;

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        Debug.Log("new event");
        
        foreach (var trackedImage in eventArgs.added)
        {
            Debug.Log($"tracked image {trackedImage.trackingState}");
            // Handle added event
            
            trackedImage.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            trackedImage.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            HandleImageUpdate(trackedImage);
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            Debug.Log($"tracked image {updatedImage.trackingState}");
            // Handle updated event
            HandleImageUpdate(updatedImage);
        }

        foreach (var removedImage in eventArgs.removed)
        {
            // Handle removed event
        }
    }
    

    #endregion
    
    
    #region Unity3D
    
    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        imageManager = GetComponent<ARTrackedImageManager>();
        imageManager.trackedImagesChanged += OnChanged;
        mainCamera = Camera.main;
    }

    
    void Update()
    {
        HandleUpdate(Time.deltaTime);
    }
    
    #endregion
    
    #region Private Methods

    private void HandleImageUpdate(ARTrackedImage trackedImage)
    {
        if (trackedImage.trackingState != TrackingState.None && !maskInstance)
        {
            //var head = FindObjectOfType<Head>();
            //if (!head) return;
            // The image extents is only valid when the image is being tracked
            //trackedImage.transform.localScale = new Vector3(trackedImage.size.x, 1f, trackedImage.size.y);
            //head.transform.position = trackedImage.transform.position;
            
            Debug.Log("INSTANTIATING");
            maskInstance = Instantiate(head, trackedImage.transform).gameObject;
            transform.localPosition = new Vector3(0f,0f, 0f);
        
        }
    }

    private void HandleUpdate(float dt)
    {
        if (Input.touches.Length != 1) return;
        Vector3 v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.2f);
        v3 = mainCamera.ScreenToWorldPoint(v3);
        mask.transform.LookAt(mainCamera.transform);
        mask.transform.position = v3;

    }
    #endregion
    
    #region Public Methods


    #endregion
}
