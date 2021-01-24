using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class HandsSceneManager : TrackerListener
{
    #region Attributes

    [SerializeField] private GameObject scene;
    [SerializeField] private GameObject hands;
    [SerializeField] private GameObject gelVisualizer;
    private GameObject sceneInstance;
    private GameObject handsInstance;

    #endregion

    #region Unity3D

    public override void OnDetectedStart(ARTrackedImage img)
    {
        //Activate hud
        gelVisualizer.SetActive(true);
        sceneInstance = Instantiate(scene, Camera.main.transform);
    }

    public override void OnDetectedUpdate(ARTrackedImage img)
    {
        if (img.trackingState != TrackingState.None && !handsInstance)
        {
            //Instantiate scene
            handsInstance = Instantiate(hands, img.transform).gameObject;
            handsInstance.transform.localPosition = Vector3.zero;
            handsInstance.transform.LookAt(sceneInstance.transform);
        }
    }

    public override void OnStoppingDetection()
    {
        gelVisualizer.SetActive(false);
        Destroy(sceneInstance);
    }

    #endregion
}
