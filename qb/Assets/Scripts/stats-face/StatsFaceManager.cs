using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class StatsFaceManager : TrackerListener
{
    [SerializeField]
    private TextMesh text;
    private TextMesh textInstance;
    
    public override void OnDetectedStart(ARTrackedImage img)
    {
        textInstance = Instantiate(text, img.transform);
    }

    public override void OnStoppingDetection()
    {
            
    }

    public override void ARUpdate()
    {
        base.ARUpdate();
    }
}
