﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARTrackerClassRoom : TrackerListener
{
    [SerializeField] private GameObject scene;

    private GameObject sceneInstance;
    
    public override void OnDetectedStart(ARTrackedImage img)
    {
        sceneInstance = Instantiate(scene, img.gameObject.transform);
        sceneInstance.transform.localScale *= 5;
    }

    public override void OnDetectedUpdate(ARTrackedImage img)
    {
        base.OnDetectedUpdate(img);
    }

    public override void OnStoppingDetection()
    {
        Destroy(sceneInstance);
    }
}
