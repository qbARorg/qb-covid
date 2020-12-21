using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class MaskSceneBehaviour : TrackerListener
{
    
    #region Public
    
    #endregion
    
    #region Private

    [SerializeField] private Head head;
    
    private ARRaycastManager raycastManager;

    [SerializeField]
    private GameObject mask;

    private GameObject headInstance;
    private Camera mainCamera;
    
    #endregion

    #region Callbacks

    /*

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
    
    */
    #endregion
    
    
    #region Unity3D

    public override void OnDetectedStart(ARTrackedImage img)
    {
        raycastManager = GetComponent<ARRaycastManager>();
        mainCamera = Camera.main;
        mask.SetActive(true);
        mask.transform.localPosition = Vector3.zero;
        mask.transform.SetParent(mainCamera.transform);
    }

    public override void OnDetectedUpdate(ARTrackedImage img)
    {
        if (img.trackingState != TrackingState.None && !headInstance)
        {
            headInstance = Instantiate(head, img.transform).gameObject;
            transform.localPosition = new Vector3(0f, 0f, 0f);
        }
    }

    public override void ARUpdate()
    {
        HandleUpdate(Time.deltaTime);
    }
    
    #endregion
    
    #region Private Methods

    private void HandleImageUpdate(ARTrackedImage trackedImage)
    {
        if (trackedImage.trackingState != TrackingState.None && !headInstance)
        {
            Debug.Log("INSTANTIATING");
            headInstance = Instantiate(head, trackedImage.transform).gameObject;
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

        if (headInstance)
        {
            Vector3 headPosition = headInstance.transform.position;
            float distanceLook = Vector3.Distance(
                    headInstance.transform.forward.normalized,
                    mask.transform.forward.normalized
            );
            Debug.Log($"rotation dist: {distanceLook}");
            if (Vector3.Distance(headPosition, v3) <= 0.1f && distanceLook <= 0.45f)
            {
                mask.SetActive(false);
                headInstance.GetComponent<Head>().ChangeState(Head.State.AwaitingStrings);
            }
        }
        
    }
    
    #endregion
    
    #region Public Methods

    

    #endregion
}
