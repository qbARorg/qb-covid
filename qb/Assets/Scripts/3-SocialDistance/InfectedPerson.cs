using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class InfectedPerson : MonoBehaviour
{
    public float speed = 1f;
    public float longevity = 2.5f;
    public ARTrackedImage imgTracked;

    void Start()
    {
        Destroy(gameObject, longevity);
    }

    void Update()
    {
        transform.position = -imgTracked.transform.forward * (speed * Time.deltaTime);
    }
}
