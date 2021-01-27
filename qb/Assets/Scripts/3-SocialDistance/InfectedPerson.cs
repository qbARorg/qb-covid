using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class InfectedPerson : MonoBehaviour
{
    public float speed = 1.2f;
    public float longevity = 2.5f;
    public ARTrackedImage imgTracked;

    void Start()
    {
        Destroy(gameObject, longevity);
    }

    void Update()
    {
        // Vector3 up because we are children of the target image
        transform.localPosition += Vector3.up * (speed * Time.deltaTime);
    }
}
