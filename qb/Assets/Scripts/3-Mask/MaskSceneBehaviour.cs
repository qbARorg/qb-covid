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
    [SerializeField] private Head headInstanceComponent;
    
    private ARRaycastManager raycastManager;

    [SerializeField]
    private GameObject mask;

    private GameObject headInstance;
    private Camera mainCamera;
    private float currentTimeLeft = 0.0f;
    private float timeLeft = 0.0f;
    private const float timeLeftInitial = 20.0f;
    private int showcaseTime = 0;


    #endregion

    #region Callbacks
    #endregion
    
    
    #region Unity3D

    public override void OnDetectedStart(ARTrackedImage img)
    {
        raycastManager = GetComponent<ARRaycastManager>();
        mainCamera = Camera.main;
        mask.SetActive(true);
        mask.transform.localPosition = Vector3.zero;
        mask.transform.SetParent(mainCamera.transform);
        timeLeft = timeLeftInitial;
    }

    public override void OnDetectedUpdate(ARTrackedImage img)
    {
        if (!headInstance)
        {
            timeLeft = timeLeftInitial;
            headInstance = Instantiate(head, img.transform).gameObject;
            headInstanceComponent = headInstance.GetComponent<Head>();
            transform.localPosition = new Vector3(0f, 0f, 0f);
        }
    }

    public override void OnStoppingDetection()
    {
        mask.SetActive(false);
        Destroy(headInstance);
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
        Vector3 posMouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.2f);
        Vector3 v3 = mainCamera.ScreenToWorldPoint(posMouse);
        if (mask && mask.activeSelf)
        {
            // List<ARRaycastHit> hits = new List<ARRaycastHit>();
            // raycastManager.Raycast(new Vector2(Input.mousePosition.x, Input.mousePosition.y), hits, TrackableType.All);
            mask.transform.LookAt(mainCamera.transform);
            mask.transform.position = v3;
        }

        if (!headInstance)
        {
            return;
        }

        timeLeft -= Time.deltaTime;
        showcaseTime = (int) timeLeft;
        
        headInstanceComponent.UpdateTimeShowcase(showcaseTime);

        switch (headInstanceComponent.HeadState)
        {
            case Head.State.AwaitingMask:
            {
                Vector3 headPosition = headInstance.transform.position;
                float distanceLook = Vector3.Distance(
                    headInstance.transform.forward.normalized,
                    mask.transform.forward.normalized
                );
                
                //Debug.Log($"rotation dist: {distanceLook}");
                
                if (Vector3.Distance(headPosition, v3) <= 0.1f && distanceLook <= 0.45f)
                {
                    mask.SetActive(false);
                    headInstanceComponent.ChangeState(Head.State.AwaitingStrings);
                }

                break;
            }

            case Head.State.AwaitingStrings:
            {
                Ray ray = mainCamera.ScreenPointToRay(v3);
                if (Physics.Raycast(ray, out RaycastHit info))
                {
                    GameObject rope = info.collider.gameObject;
                    if (rope.CompareTag("Rope"))
                    {
                        Transform[] children = rope.GetComponentsInChildren<Transform>();
                        foreach (Transform ropeChild in children)
                        {
                            ropeChild.gameObject.SetActive(true);
                        }
                        headInstanceComponent.OneRope();
                    }
                }
                break;
            }

            case Head.State.NextPerson:
            {
                break;
            }

            default:
            {
                break;
            }
        }
        
  
        
    }
    
    #endregion
    
    #region Public Methods

    

    #endregion
}
