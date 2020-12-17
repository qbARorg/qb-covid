using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.RotateAround(Camera.main.transform.position, Vector3.up, 30 * Time.deltaTime);
    }
}
