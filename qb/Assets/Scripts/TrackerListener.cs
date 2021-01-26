using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TrackerListener : MonoBehaviour
{
    public string imageName;

    private bool _showing = false;

    public bool Showing
    {
        get => _showing;
        set => _showing = value;
    }
    
    public virtual void OnDetectedUpdate(ARTrackedImage img) {}
    
    public virtual void OnStoppingDetection() {}
    
    public virtual void ARUpdate() {}
    
    public virtual void Start() {}

    public virtual void OnDetectedStart(ARTrackedImage img)
    {
        
    }
    
}
