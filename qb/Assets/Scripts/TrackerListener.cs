using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TrackerListener : MonoBehaviour
{
    public int imageIndex;

    public virtual void OnDetectedUpdate(ARTrackedImage img) {}
    
    public virtual void OnStoppingDetection() {}
    
    public virtual void ARUpdate() {}
    
    public virtual void Start() {}

    public virtual void OnDetectedStart(ARTrackedImage img)
    {
        
    }
    
}
