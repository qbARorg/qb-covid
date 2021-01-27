using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public TextMesh score;
    private TextMesh scoreInstance;

    public GameObject playerPrefab;
    private GameObject playerInstance;

    public float infectedPersonAppearRate;

    private Rail currentRail;
    private float railDist;
    private float timer = 0.0f;
    private float probabilityOfCovid = 0.0f;

    private const float probDelta = 0.01f;
    private const float minDist = .5f;

    private Vector2 tap = Vector3.zero;
    private bool canTap = true;

    #endregion

    public void ARAwake(ARTrackedImage img)
    {
        imgTracker = img;
        currentRail = Rail.Center;
        railDist = 0.5f;
        playerInstance = Instantiate(playerPrefab, Vector3.left * 0.08f + Vector3.back * 0.3f, transform.rotation, transform);
        playerInstance.transform.localScale *= 0.7f;
        playerInstance.transform.Rotate(Vector3.right * 90f);
        score.transform.Rotate(Vector3.right * 90f);
    }

    public void ARUpdate()
    {
        AppearEnemies(Time.deltaTime);
        HandleTouch();
        score.text = probabilityOfCovid.ToString();
    }

    private void HandleTouch()
    {
        tap = Input.mousePosition;
        if (canTap && Input.touchCount > 0)
        {
            var middle = Screen.width / 2;
            var dir = (int)((middle - tap.x) / Mathf.Abs(middle - tap.x));
            if (dir > 0)
            {
                GoLeft();
            }
            else if (dir < 0)
            {
                GoRight();
            }
            canTap = false;
        }
        else if (!canTap && Input.touchCount == 0)
        {
            canTap = true;
        }

        CheckInfection();
    }

    private void CheckInfection()
    {
        var enemies = FindObjectsOfType<InfectedPerson>();
        foreach (var enemy in enemies)
        {
            float dist = (playerInstance.transform.position - enemy.transform.position).sqrMagnitude;
            if (dist > minDist)
            {
                probabilityOfCovid += probDelta;
            }
        }
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

    private void GoLeft()
    {
        switch (currentRail)
        {
            case Rail.Center:
                currentRail = Rail.Left;
                playerInstance.transform.position += Vector3.left * 0.2f;
                break;
            case Rail.Right:
                currentRail = Rail.Center;
                playerInstance.transform.position += Vector3.left * 0.2f;
                break;
        }
    }

    private void GoRight()
    {
        switch (currentRail)
        {
            case Rail.Left:
                currentRail = Rail.Center;
                playerInstance.transform.position += Vector3.right * 0.2f;
                break;
            case Rail.Center:
                currentRail = Rail.Right;
                playerInstance.transform.position += Vector3.right * 0.2f;
                break;
        }
    }
}
