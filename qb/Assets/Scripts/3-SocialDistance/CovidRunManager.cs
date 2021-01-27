using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CovidRunManager : MonoBehaviour
{
    #region Enums

    enum Rail
    {
        Left = 0,
        Center,
        Right,
        Count = 4
    }

    #endregion

    #region Properties

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

    public void ARAwake()
    {
        currentRail = Rail.Center;
        railDist = 0.5f;
        playerInstance = Instantiate(playerPrefab, Vector3.back, transform.rotation, transform);
    }

    public void ARUpdate()
    {
        AppearEnemies(Time.deltaTime);
        var tap = Input.mousePosition;
        var middle = Screen.width;
        var dir = (int)((middle - tap.x) / Mathf.Abs(middle - tap.x));

        if (dir > 0)
        {
            playerInstance.transform.position = GoRight();
        }
        else
        {
            playerInstance.transform.position = GoLeft();
        }

        
    }

    private void CheckInfection()
    {

    }

    private void OnDisable()
    {
        foreach(Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    private void AppearEnemies(float dt)
    {
        timer += dt;
        if (timer > infectedPersonAppearRate)
        {
            timer = 0.0f;
            Rail rail = RandomRail();
            Vector3 instantiationPoint = InstantiationPointOnRail(rail);
            InstantiateEnemy(instantiationPoint);
        }
    }

    private Rail RandomRail()
    {
        return (Rail)Mathf.Floor(Random.Range(0, (int)Rail.Count));
    }

    private Vector3 InstantiationPointOnRail(Rail rail)
    {
        Vector3 position = transform.position;

        switch (rail)
        {
            case Rail.Left:
                position = new Vector3(-1.0f, 0f, 10.0f);
                break;
            case Rail.Center:
                position = new Vector3(0.0f, 0.0f, 10.0f);
                break;
            case Rail.Right:
                position = new Vector3(1.0f, 0f, 10.0f);
                break;
            default:
                position = new Vector3(0.0f, 0f, 10.0f);
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
            return Vector3.left + playerInstance.transform.position;
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
            return Vector3.right + playerInstance.transform.position;
        }
    }

    private void InstantiateEnemy(Vector3 position)
    {
        GameObject newEnemy = Instantiate(infectedPersonPrefab, position, playerInstance.transform.rotation, transform);
        newEnemy.transform.parent = transform;
    }
}
