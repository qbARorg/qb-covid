using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ARShooting : MonoBehaviour
{
    #region Attributes
    [Header("Prefabs")]
    public GameObject handsPrefab;
    public GameObject scenePrefab;

    private GameObject scene = null;
    private GameObject gelBottleHead;
    private GameObject gelBottle;
    private Transform shootPosition;

    [Header("Bottle head animation")]
    private Vector3 offsetBottlePos;
    private enum Animate { up = 1, down = 0 };
    private bool finishDownAnimation = false;

    [Header("Bottle movement")]
    private Vector3 localBottlePos;

    [Header("Gel")]
    public ParticleSystem mainParticleSyst;
    public ParticleSystem splatterParticleSyst;
    private ParticleSystem.ForceOverLifetimeModule gelForceModule;

    private bool isDragging;

    private ARTrackedImageManager imageManager;
    #endregion

    #region Main Methods
    private void Awake()
    {
        gelForceModule = mainParticleSyst.forceOverLifetime;
        offsetBottlePos = new Vector3(0f, 0.1f, 0f);
    }

    void Update()
    {
        if (!scene)
        {
            scene = Camera.main.transform.GetChild(0).gameObject;   //Set scene
            if (scene != null) SetVariables();
            else return;
        }
        //Press
        if (Input.GetMouseButton(0) || Input.touchCount > 0)
        {
            isDragging = true;
            if (!finishDownAnimation) AnimateBottle(Animate.down);  //Animate bottle
        }
        //Release
        if (Input.GetMouseButtonUp(0)) /*if (Input.touchCount <= 0)*/
        {
            isDragging = false;
            if (finishDownAnimation) AnimateBottle(Animate.up);     //Animate bottle
        }
        if (isDragging) OnDrag();
    }
    #endregion

    #region Custom Methods
    
    private void SetVariables()
    {
        gelBottleHead = GameObject.FindGameObjectWithTag("BottleHead");
        gelBottle = gelBottleHead.transform.parent.gameObject;
        shootPosition = gelBottleHead.transform.GetChild(0).transform;
        localBottlePos = gelBottle.transform.localPosition;
    }

    private void OnDrag()
    {
        //Move bottle
        Vector3 touchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f);
        touchPos = Camera.main.ScreenToWorldPoint(touchPos);
        gelBottle.transform.position = new Vector3(touchPos.x, gelBottle.transform.position.y, gelBottle.transform.position.z);
        gelBottle.transform.localPosition = new Vector3(gelBottle.transform.localPosition.x, localBottlePos.y, localBottlePos.z);

        //Emit one particle at a time
        mainParticleSyst.Emit(1);

        //Set height of mainParticleSyst
        float height = Mathf.Clamp(touchPos.y - shootPosition.position.y, 0f, 1f);
        gelForceModule.yMultiplier = Mathf.Lerp(0.001f, 15f, height);

        //Debug
        Debug.Log($"multiplier: {gelForceModule.yMultiplier}");
        Vector3 endPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 8);
        Debug.DrawLine(Vector3.zero, Camera.main.ScreenToWorldPoint(endPoint));
    }

    private void AnimateBottle(Animate direction)
    {
        if (direction == Animate.up)
        {
            gelBottleHead.transform.position += offsetBottlePos;
            finishDownAnimation = false;
        }
        else if (direction == Animate.down)
        {
            gelBottleHead.transform.position -= offsetBottlePos;
            finishDownAnimation = true;
        }
    }
    #endregion
}
