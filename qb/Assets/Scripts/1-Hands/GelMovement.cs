﻿using UnityEngine;

public class GelMovement : MonoBehaviour
{
    #region Attributes
    private ARShooting _ARShooting;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _ARShooting = GameObject.FindGameObjectWithTag("GameController").GetComponent<ARShooting>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    #region Trash

    //private float bulletSpeed = 100.0f;
    //private float height = 10f;
    //private Vector3 initalVel = new Vector3(0f, 1.0f, 1.0f);
    //private Transform shootPosition;
    //shootPosition = GameObject.FindWithTag("BottleHead").transform.GetChild(0).transform;
    //Calculate angle depending on screenPos

    //this.transform.position = ParabolicTrajectory(shootPosition.position, shootPosition.position + new Vector3(0, 15f, 15f), height, 1.0f * Time.deltaTime);

    //this.transform.localScale += new Vector3(0f, 0f, 0.1f * Time.deltaTime);
    //this.transform.position = _ARShooting.PosInTime(Time.deltaTime);
    //v = v0 cos(a)i + v0 sin(a)j;
    //a = -gj

    //this.transform.position += new Vector3(0f, -4.9f * Time.deltaTime * Time.deltaTime + initalVel.y * Time.deltaTime, initalVel.z * Time.deltaTime);


    //r(t) = (v0x * t + x0)i + (-1/2 * g * t * t + v0y * t + y0)j
    //private static Vector3 ParabolicTrajectory(Vector3 startPoint, Vector3 endPoint, float height, float time)
    //{
    //    Func<float, float> Function = x => -4 * height * x * x + 4 * height * x;

    //    var middlePoint = Vector3.Lerp(startPoint, endPoint, time);

    //    return new Vector3(middlePoint.x, Function(time) + Mathf.Lerp(startPoint.y, endPoint.y, time), middlePoint.z);
    //}

    #endregion
}
