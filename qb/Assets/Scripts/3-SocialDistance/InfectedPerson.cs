using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectedPerson : MonoBehaviour
{
    public float speed = 3.0f;
    public float longevity = 2.5f;

    void Start()
    {
        Destroy(this.gameObject, longevity);
    }

    void Update()
    {
        transform.position -= Vector3.forward * (speed * Time.deltaTime);
    }
}
