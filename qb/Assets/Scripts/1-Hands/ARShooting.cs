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

    public GameObject handsPrefab;
    public GameObject gelPrefab;
    public GameObject ballParent;
    public GameObject scenePrefab;

    [SerializeField] [Range(0.001f, 0.1f)] float distanceBtwDots;

    private GameObject scene = null;
    private GameObject hands;
    private GameObject gel;
    private GameObject gelBottle;
    private GameObject gelBottleHead;
    private Transform shootPosition;
    private Vector3 offsetBottlePos;
    private bool once = true;

    private GameObject[] points;
    public int numPoints;
    public float force = 1.0f;

    private Vector3 startPoint;
    private Vector3 endPoint;
    private Vector3 direction;
    private Vector3 force3;
    private float distance;
    private bool isDragging;

    private ARTrackedImageManager imageManager;

    #region Trash Att
    private GameObject ARCam;

    private LineRenderer line;
    private LayerMask layer;
    private int lineSegment;
    private float flightTime = 1.0f;
    private Vector3 height;

    private Vector2 touchPosition;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private ARRaycastManager _arRaycastManager;

    #endregion
    #endregion

    // Start is called before the first frame update
    private void Awake()
    {
        imageManager = this.GetComponent<ARTrackedImageManager>();
        offsetBottlePos = new Vector3(0f, 0.1f, 0f);

        imageManager.trackedImagesChanged += OnChanged;

        points = new GameObject[numPoints];
        for(int i = 0; i<numPoints; i++)
        {
            points[i] = Instantiate(gelPrefab, transform.position, Quaternion.identity);
            points[i].transform.SetParent(ballParent.transform);
        }
    }

    private void OnDragStart()
    {
        startPoint = Input.mousePosition;
        if(Input.touchCount > 0) startPoint = Input.GetTouch(0).position;
    }

    private void OnDrag()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(camRay, out hit, 20f))
        {
            endPoint = hit.point;
            distance = Vector3.Distance(startPoint, endPoint);
            direction = (startPoint - endPoint).normalized;
            force3 = direction * distance * force;
        }
        //Debug
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
            scene = GameObject.FindGameObjectWithTag("MainCamera").transform.GetChild(0).gameObject;
            if (scene != null) {
                SetVariables();
            }
        }
        if (!hands)
        {
            hands = Instantiate(handsPrefab, handsPrefab.transform.position, handsPrefab.transform.rotation);
            //hands.transform.parent = GameObject.FindGameObjectWithTag("MainCamera").transform;
        }
        if (Input.GetMouseButton(0) || Input.touchCount > 0)
        {
            isDragging = true;
            OnDragStart();
            if (once)
            {
                gelBottleHead.transform.position -= offsetBottlePos;
                once = false;
            }
        }

        if(Application.platform == RuntimePlatform.Android)
        {
            if(Input.touchCount <= 0)
            {
                if (!once)
                {
                    gelBottleHead.transform.position += offsetBottlePos;
                    once = true;
                    isDragging = false;
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (!once)
                {
                    gelBottleHead.transform.position += offsetBottlePos;
                    once = true;
                    isDragging = false;
                }
            }
        }

        if (isDragging)
        {
            ballParent.SetActive(true);
            //Vector3 d = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //gelBottle.transform.position = new Vector3(d.x, gelBottle.transform.position.y, gelBottle.transform.position.z);
            OnDrag();
            for (int i = 0; i < numPoints; i++)
            {
                points[i].transform.position = PosInTime(i * distanceBtwDots);
            }
        }
        else
        {
            ballParent.SetActive(false);
        }

    }

    void OnDisable() => imageManager.trackedImagesChanged -= OnChanged;

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            if (trackedImage.trackingState != TrackingState.None)
            {
                scene.SetActive(true);
            }
        }
    }

    public Vector3 PosInTime(float t)
    {
        return shootPosition.position + (direction.normalized * force * t) + 0.5f * Physics.gravity * t * t;
    }

    #region Trash Meth
    //Update()
    //{
    //if(Input.touchCount > 0 || Input.GetMouseButtonDown(0))
    //{
    //    height = Vector3.Lerp(Input.mousePosition, handsPrefab.transform.position, 0.5f);
    //}

    //height is min (0f, 0.5f, 0f);

    ////if (!TryGetTouchPosition(out Vector2 touchPosition)) return;

    //if (Input.touchCount > 0 || Input.GetMouseButton(0))
    //{

    //    Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;

    //    if (Physics.Raycast(camRay, out hit, 100f, layer))
    //    {

    //        Vector3 initialVelocity = CalculateVelocty(hit.point, shootPosition.position, flightTime);

    //        Visualize(initialVelocity, hit.point + Vector3.up * 0.1f); //we include the cursor position as the final nodes for the line visual position

    //        transform.rotation = Quaternion.LookRotation(initialVelocity);
    //    }

    //    //Vector3.MoveTowards(gelBottleHead.transform.position, gelBottleHead.transform.position - new Vector3(0f, 0.1f, 0f), 0.01f * Time.deltaTime);
    //    if (once)
    //    {
    //        gelBottleHead.transform.position -= offsetBottlePos;
    //        once = false;
    //    }
    //    //Shooting
    //    if(gel == null)
    //    {
    //        gel = Instantiate(gelPrefab, gelPrefab.transform.position, gelPrefab.transform.rotation);
    //        gel.GetComponent<GelMovement>().shootPosition = shootPosition;
    //    }

    //}
    //else
    //{
    //    gelBottleHead.transform.position = gelBottleHead.transform.parent.transform.position;
    //    once = true;
    //}

    //if (_arRaycastManager.Raycast(touchPosition, hits))
    //{
    //    var hitPose = hits[0].pose;
    //    Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(touchPosition);

    //}
    //}


    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }

    private void Visualize(Vector3 initialVel, Vector3 finalPos)
    {
        for (int i = 0; i < lineSegment; i++)
        {
            Vector3 pos = CalculatePosInTime(initialVel, (i / (float)lineSegment) * flightTime);
            line.SetPosition(i, pos);
        }

        line.SetPosition(lineSegment, finalPos);
    }


    private Vector3 CalculateVelocty(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distance2D = new Vector3(distance.x, 0f, distance.z);

        float sY = distance.y;
        float sXZ = distance2D.magnitude;

        float vXZ = sXZ / time;
        float vY = (sY / time) + (0.5f * Mathf.Abs(Physics.gravity.y) * time);

        Vector3 result = distance2D.normalized;
        result *= vXZ;
        result.y = vY;

        return result;
    }

    private Vector3 CalculatePosInTime(Vector3 initialVel, float time)
    {
        Vector3 vXZ = initialVel;
        vXZ.y = 0f;

        Vector3 result = shootPosition.position + initialVel * time;
        float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (initialVel.y * time) + shootPosition.position.y;

        result.y = sY;

        return result;
    }

    #endregion
}
