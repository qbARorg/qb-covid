using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CovidRunSceneManager : TrackerListener
{
    public CovidRunManager manager;
    private CovidRunManager managerInstance;

    public override void OnDetectedStart(ARTrackedImage img)
    {
        Debug.Log("goooolaso de maradona 😱🙇🏽‍♀️");
        managerInstance = Instantiate(manager, img.transform);
        managerInstance.ARAwake(img);
    }

    public override void OnDetectedUpdate(ARTrackedImage img)
    {
        base.OnDetectedUpdate(img);
    }

    public override void OnStoppingDetection()
    {
        Destroy(managerInstance);
        managerInstance = null;
    }

    public override void ARUpdate()
    {
        if (managerInstance)
        {
            managerInstance.ARUpdate();
        }
    }
}
