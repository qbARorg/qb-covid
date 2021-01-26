using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusManager : MonoBehaviour
{
    #region Attributes

    public GameObject handsVisuals;
    public GameObject virusPrefab;

    private new Collider collider;
    private RaycastHit[] hits;
    private int numViruses = 10;
    private GameObject[] virusInstances;

    #endregion

    #region Unity3D

    private void Awake()
    {
        collider = handsVisuals.GetComponent<Collider>();
        hits = new RaycastHit[numViruses];
        for(int i = 0; i<numViruses; i++)
        {
            hits[i] = GetPointOnMesh();
        }
    }

    void Start()
    {
        virusInstances = new GameObject[hits.Length];
        for (int i = 0; i < hits.Length; i++)
        {
            Quaternion rotation = Quaternion.LookRotation(hits[i].normal);
            virusInstances[i] = Instantiate(virusPrefab, hits[i].point, rotation, this.transform);
        }
    }

    #endregion

    #region Private Methods

    private RaycastHit GetPointOnMesh()
    {
        float length = 50f;
        Vector3 direction = Random.onUnitSphere;
        Ray ray = new Ray(transform.position + (direction * length), -direction);
        collider.Raycast(ray, out RaycastHit hit, length * 2);
        return hit;
    }

    #endregion
}
