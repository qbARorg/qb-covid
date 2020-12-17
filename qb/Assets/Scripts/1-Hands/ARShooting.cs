using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class ARShooting : MonoBehaviour
{
    public GameObject handsPrefab;
    public GameObject gelPrefab;

    private GameObject hands;
    private GameObject gel;
    private GameObject gelBottleHead;
    private Vector3 offsetBottlePos;
    private Transform shootPosition;
    private bool once = true;

    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Start is called before the first frame update
    void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
        offsetBottlePos = new Vector3(0f, 0.1f, 0f);
        gelBottleHead = GameObject.FindWithTag("BottleHead");
        shootPosition = gelBottleHead.transform.GetChild(0).transform;
    }


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

    // Update is called once per frame
    void Update()
    {
        if (hands == null)
        {
            hands = Instantiate(handsPrefab, handsPrefab.transform.position, handsPrefab.transform.rotation);
        }

        //if (!TryGetTouchPosition(out Vector2 touchPosition)) return;

        if ((Input.touchCount > 0 || Input.GetMouseButton(0)))
        {
            //Vector3.MoveTowards(gelBottleHead.transform.position, gelBottleHead.transform.position - new Vector3(0f, 0.1f, 0f), 0.01f * Time.deltaTime);
            if (once)
            {
                gelBottleHead.transform.position -= offsetBottlePos;
                once = false;
            }
            //Shooting
            if(gel == null)
            {
                gel = Instantiate(gelPrefab, gelPrefab.transform.position, gelPrefab.transform.rotation);
                gel.GetComponent<GelMovement>().shootPosition = shootPosition;
            }

        }
        else
        {
            gelBottleHead.transform.position = gelBottleHead.transform.parent.transform.position;
            once = true;
        }

        if (_arRaycastManager.Raycast(touchPosition, hits))
        {
            var hitPose = hits[0].pose;
            Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(touchPosition);

        }
    }
}
 