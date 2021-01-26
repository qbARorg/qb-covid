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

    public float infectedPersonAppearRate;

    private Rail currentRail;
    private float railDist;
    private float timer = 0.0f;

    #endregion

    void Awake()
    {
        currentRail = Rail.Center;
        railDist = 0.5f;
    }

    void Update()
    {
        AppearEnemies(Time.deltaTime);
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
        foreach(Transform child in this.transform)
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
            Vector3 instantiationPoint = instantiationPointOnRail(rail);
            InstantiateEnemy(instantiationPoint);
        }
    }

    private Rail RandomRail()
    {
        return (Rail)Mathf.Floor(Random.Range(0, (int)Rail.Count));
    }

    private Vector3 instantiationPointOnRail(Rail rail)
    {
        Vector3 position = Vector3.zero;

        switch (rail)
        {
            case Rail.Left:
                position = new Vector3(-1.0f, -1.0f, 10.0f);
                break;
            case Rail.Center:
                position = new Vector3(0.0f, -1.0f, 10.0f);
                break;
            case Rail.Right:
                position = new Vector3(1.0f, -1.0f, 10.0f);
                break;
            default:
                position = new Vector3(0.0f, -1.0f, 10.0f);
                break;
        }

        return position;
    }

    private void InstantiateEnemy(Vector3 position)
    {
        GameObject newEnemy = Instantiate(infectedPersonPrefab, position, Quaternion.identity);
        newEnemy.transform.parent = this.transform;
    }
}
