using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ARShooting : MonoBehaviour
{
    #region Attributes
    [Header("Prefabs")]
    public GameObject handsPrefab;
    public GameObject gelPrefab;
    public GameObject gelParent;
    public GameObject scenePrefab;

    private GameObject scene = null;
    private GameObject gelBottle;
    private GameObject gelBottleHead;
    private Transform shootPosition;

    [Header("Bottle animation")]
    private Vector3 offsetBottlePos;
    private enum Animate { up = 1, down = 0 };
    private bool finishDownAnimation = false;

    [Header("Gel")]
    public int numPoints;
    private GameObject[] points;
    [SerializeField] [Range(4f, 10f)] private float force = 6.0f;
    [SerializeField] [Range(0.001f, 0.1f)] private float distanceBtwPoints;

    private Vector3 startPoint;
    private Vector3 endPoint;
    private Vector3 direction;

    private bool isDragging;

    private ARTrackedImageManager imageManager;
    
    #endregion

    // Start is called before the first frame update
    private void Awake()
    {
        offsetBottlePos = new Vector3(0f, 0.1f, 0f);

        points = new GameObject[numPoints];
        for(int i = 0; i<numPoints; i++)
        {
            points[i] = Instantiate(gelPrefab, transform.position, Quaternion.identity);
            points[i].transform.SetParent(gelParent.transform);
        }
    }

    private void OnDragStart()
    {
        endPoint = Input.mousePosition;
        endPoint.z = 10;
        endPoint = Camera.main.ScreenToWorldPoint(endPoint);
        if(Input.touchCount > 0) endPoint = Input.GetTouch(0).position;
    }

    private void OnDrag()
    {
        direction = endPoint - startPoint;
        //Debug
        Debug.Log($"start {startPoint}, end {endPoint}");
        Debug.DrawLine(startPoint, endPoint);
    }

    private void SetVariables()
    {
        gelBottleHead = GameObject.FindGameObjectWithTag("BottleHead");
        gelBottle = gelBottleHead.transform.parent.gameObject;
        shootPosition = gelBottleHead.transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!scene)
        {
            scene = Camera.main.transform.GetChild(0).gameObject;
            if (scene != null) {
                SetVariables();
            }
        }
        if (Input.GetMouseButton(0) || Input.touchCount > 0)
        {
            isDragging = true;
            OnDragStart();

            //Animate bottle
            if (!finishDownAnimation) AnimateBottle(Animate.down);
        }

        if (Input.GetMouseButtonUp(0)) /*if (Input.touchCount <= 0)*/
        {
            if (finishDownAnimation) AnimateBottle(Animate.up);
            isDragging = false;
        }

        if (isDragging)
        {
            //Show points
            gelParent.SetActive(true);
            OnDrag();
            //Calculate each points' position
            for (int i = 0; i < numPoints; i++)
            {
                points[i].transform.position = PosInTime(i * distanceBtwPoints);
            }
        }
        else gelParent.SetActive(false);
    }

    public Vector3 PosInTime(float t)
    {
        return shootPosition.position + (direction.normalized * force * t) + 0.5f * Physics.gravity * t * t;
    }

    private void AnimateBottle(Animate direction)
    {
        if (direction == Animate.up)
        {
            gelBottleHead.transform.position += offsetBottlePos;
            finishDownAnimation = false;
        }
        else if (direction == Animate.down)
        {
            gelBottleHead.transform.position -= offsetBottlePos;
            finishDownAnimation = true;
        }
    }
}
