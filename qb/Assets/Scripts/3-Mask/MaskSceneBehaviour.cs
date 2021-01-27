using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;


public class MaskSceneBehaviour : TrackerListener
{
    
    #region Public

    public const string ConfigFileName = "Mask_Scene_Score";
    
    #endregion
    
    #region Private
    
    [SerializeField] private Head head;
    [SerializeField] private Head headInstanceComponent;
    private ARRaycastManager raycastManager;
    [SerializeField]
    private LayerMask layer;
    [SerializeField]
    private GameObject mask;
    private GameObject headInstance;
    private Camera mainCamera;
    private float currentTimeLeft = 0.0f;
    private float factorTime = 1.0f;
    private float timeLeft = 0.0f;
    private const float timeLeftInitial = 20.0f;
    private int maximumScore = 0;
    private int currentScore = 0;
    private float nextPersonTimer = 0.0f;
    private float timeForNextPerson = 3.0f;
    
    private int showcaseTime = 0;


    private ARTrackedImage img = null;
    


    #endregion

    #region Callbacks
    #endregion
    
    
    #region Unity3D

    public override void OnDetectedStart(ARTrackedImage img)
    {
        factorTime = 1.0f;
        raycastManager = GetComponent<ARRaycastManager>();
        mainCamera = Camera.main;
        mask.SetActive(true);
        mask.transform.localPosition = Vector3.zero;
        mask.transform.SetParent(mainCamera.transform);
        timeLeft = timeLeftInitial;
        if (!SaveSystem.Exists(ConfigFileName))
        {
            SaveSystem.Save(ConfigFileName, new Scores(0));
            Debug.Log($"[MASK SCENE] Loaded successfully: score {maximumScore}");
        }
        else
        {
            Scores scores = SaveSystem.Load<Scores>(ConfigFileName);
            maximumScore = scores.maximumScore;
            Debug.Log($"[MASK SCENE] Loaded successfully: score {maximumScore}");
        }
    }

    public override void OnDetectedUpdate(ARTrackedImage img)
    {
        this.img = img;
        if (!headInstance)
        {            
            timeLeft = timeLeftInitial;
            headInstance = Instantiate(head, img.transform).gameObject;
            headInstanceComponent = headInstance.GetComponent<Head>();
            headInstanceComponent.SetScore(currentScore);
            headInstanceComponent.SetMaxScore(maximumScore);
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
    
    private void HandleUpdate(float dt)
    {
        timeLeft -= Time.deltaTime;
        showcaseTime = Math.Max((int) timeLeft, 0);
        
        if (!headInstance)
        {
            return;
        }

        if (headInstanceComponent.HeadState == Head.State.NextPerson)
        {
            nextPersonTimer += Time.deltaTime;
            if (nextPersonTimer >= timeForNextPerson)
            {
                nextPersonTimer = 0.0f;
                Destroy(headInstance);
                headInstance = null;
                Destroy(headInstanceComponent);
                headInstanceComponent = null;
                OnDetectedUpdate(img);
                // 😨 🥵 😳😳😳😳😳😳😳😳😳😳😳😳😳😳😳😳😳😳😳😳😳😳😳😳😳 
                factorTime += 0.41234567f;
                currentScore++;
                if (currentScore > maximumScore)
                {
                    SaveSystem.Save(ConfigFileName, new Scores(currentScore));
                    Debug.Log($"[MASK SCENE] Saved successfully score: {currentScore}");
                    maximumScore = currentScore;
                }
                timeLeft /= factorTime;
                mask.SetActive(true);
                mask.transform.localPosition = Vector3.zero;
                mask.transform.SetParent(mainCamera.transform);
                return;
            }
        }
        else if (timeLeft <= 0f)
        {
            headInstanceComponent.ChangeState(Head.State.Lost);
            return;
        }
        
        headInstanceComponent.UpdateTimeShowcase(showcaseTime);

        if (Input.touches.Length != 1) return;
        Vector3 posMouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.2f);
        Vector3 v3 = mainCamera.ScreenToWorldPoint(posMouse);
        if (mask && mask.activeSelf)
        {
            mask.transform.LookAt(mainCamera.transform);
            mask.transform.position = v3;
        }
        
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
                
                if (Vector3.Distance(headPosition, v3) <= 0.05f && distanceLook <= 0.45f)
                {
                    mask.SetActive(false);
                    headInstanceComponent.ChangeState(Head.State.AwaitingStrings);
                }

                break;
            }

            case Head.State.AwaitingStrings:
            {
                if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward), out RaycastHit info, Mathf.Infinity)) // layerMask is 8
                {
                    GameObject rope = info.collider.gameObject;
                    GameObject child = rope.transform.GetChild(0).gameObject;
                    if (child.activeSelf) return;
                    rope.transform.GetChild(0).gameObject.SetActive(true);
                    headInstanceComponent.OneRope();
                }
                break;
            }

            case Head.State.NextPerson:
            {
               
                break;
            }

            case Head.State.Lost:
            {
                headInstanceComponent.ChangeState(Head.State.Lost);
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
