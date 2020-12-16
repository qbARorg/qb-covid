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

    private float height = 10.0f;

    private ARRaycastManager _arRaycastManager;
    private Vector2 touchPosition;
    private AREnvironmentProbeManager _arProbeMgr;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Start is called before the first frame update
    void Awake()
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
        _arProbeMgr = GetComponent<AREnvironmentProbeManager>();
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
        if(hands == null)
        {
            hands = Instantiate(handsPrefab, handsPrefab.transform.position, handsPrefab.transform.rotation);
        }
        
        if (!TryGetTouchPosition(out Vector2 touchPosition)) return;

        if (_arRaycastManager.Raycast(touchPosition, hits))
        {
            var hitPose = hits[0].pose;
            Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(touchPosition);

            //Calculate height depending on screenPos


            //Shooting
            gel = Instantiate(gelPrefab, gelPrefab.transform.position, gelPrefab.transform.rotation);
            //gel.transform.position = Vector3.MoveTowards(gel.transform.position, touchWorldPosition, 10.0f * Time.deltaTime);
            gel.transform.position = ParabolicTrajectory(Camera.main.transform.position, touchWorldPosition, height, 10.0f * Time.deltaTime);


        }
    }

    public static Vector3 ParabolicTrajectory(Vector3 startPoint, Vector3 endPoint, float height, float time)
    {
        Func<float, float> Function = x => -4 * height * x * x + 4 * height * x;

        var middlePoint = Vector3.Lerp(startPoint, endPoint, time);

        return new Vector3(middlePoint.x, Function(time) + Mathf.Lerp(startPoint.y, endPoint.y, time), middlePoint.z);
    }
}