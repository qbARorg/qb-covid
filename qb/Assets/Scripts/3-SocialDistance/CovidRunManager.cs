﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CovidRunManager : MonoBehaviour
{
    #region Enums

    enum Rail
    {
        Left = 0,
        Center,
        Right,
        Count = 3
    }

    #endregion

    #region Properties

    private ARTrackedImage imgTracker;

    public GameObject infectedPersonPrefab;

    public GameObject playerPrefab;

    private GameObject playerInstance;

    public float infectedPersonAppearRate;

    private Rail currentRail;
    private float railDist;
    private float timer = 0.0f;
    private float probabilityOfCovid = 0.0f;

    private const float probDelta = 0.01f;

    #endregion

    public void ARAwake(ARTrackedImage img)
    {
        imgTracker = img;
        currentRail = Rail.Center;
        railDist = 0.5f;
        playerInstance = Instantiate(playerPrefab, Vector3.back, transform.rotation, transform);
    }

    public void ARUpdate()
    {
        AppearEnemies(Time.deltaTime);
        Debug.Log("Player is at: " + playerInstance.transform.position);
        var tap = Input.mousePosition;
        var middle = Screen.width / 2;
        var dir = (int)((middle - tap.x) / Mathf.Abs(middle - tap.x));

        Debug.Log("User taped at: " + tap);
        Debug.Log("Middle is at: " + middle);
        Debug.Log("Dir to move player is: " + dir);

        if (dir > 0)
        {
            Debug.Log("Next thing move RIGHT");
            playerInstance.transform.position = GoRight();
        }
        else
        {
            Debug.Log("Next thing move LEFT");
            playerInstance.transform.position = GoLeft();
        }
    }

    private void CheckInfection()
    {

    }

    private void AppearEnemies(float dt)
    {
        timer += dt;
        if (timer > infectedPersonAppearRate)
        {
            timer = 0.0f;
            InstantiateEnemy();
        }
    }

    private void InstantiateEnemy()
    {
        GameObject newEnemy = Instantiate(infectedPersonPrefab, imgTracker.transform);
        InfectedPerson ip = newEnemy.GetComponent<InfectedPerson>();
        Vector3 p = InstantiationPointOnRail(RandomRail());
        newEnemy.transform.localPosition = p;
        ip.imgTracked = imgTracker;
    }
    
    private Rail RandomRail()
    {
        return (Rail)Mathf.Floor(Random.Range(0, (int)Rail.Count));
    }

    private Vector3 InstantiationPointOnRail(Rail rail)
    {
        Vector3 position = Vector3.zero;

        switch (rail)
        {
            case Rail.Left:
                position = Vector3.left * 0.2f;
                break;
            
            case Rail.Center:
                position = Vector3.zero * 0.2f;
                break;
            
            case Rail.Right:
                position = Vector3.right * 0.2f;
                break;
        }

        return position;
    }

    private Vector3 GoLeft()
    {
        if (currentRail == Rail.Left)
        {
            return playerInstance.transform.position;
        }
        else
        {
            currentRail--;
            return Vector3.left * 0.2f;
        }
    }

    private Vector3 GoRight()
    {
        if (currentRail == Rail.Right)
        {
            return playerInstance.transform.position;
        }
        else
        {
            currentRail++;
            return Vector3.right * 0.2f;
        }
    }


}
