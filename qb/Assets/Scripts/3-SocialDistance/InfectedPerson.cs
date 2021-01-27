using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedPerson : MonoBehaviour
{
    public float speed = 0.1f;
    public float longevity = 2.5f;

    void Start()
    {
        Destroy(gameObject, longevity);
    }

    void Update()
    {
        transform.position = -transform.parent.forward * (speed * Time.deltaTime);
    }
}
