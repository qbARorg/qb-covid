using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : MonoBehaviour
{
    public GameObject hands;
    public GameObject virusPrefab;

    private new Collider collider;
    private RaycastHit[] hits;
    private int numHits = 10;
    private GameObject[] virusInstances;

    private void Awake()
    {
        collider = hands.GetComponent<Collider>();
        hits = new RaycastHit[numHits];
        for(int i = 0; i<numHits; i++)
        {
            hits[i] = GetPointOnMesh();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        virusInstances = new GameObject[hits.Length];
        for (int i = 0; i < hits.Length; i++)
        {
            Quaternion rotation = Quaternion.LookRotation(hits[i].normal);
            virusInstances[i] = Instantiate(virusPrefab, hits[i].point, rotation, this.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private RaycastHit GetPointOnMesh(){
        float length = 50f;
        Vector3 direction = Random.onUnitSphere;
        Ray ray = new Ray(transform.position + (direction * length), -direction);
        collider.Raycast(ray, out RaycastHit hit, length * 2);
        return hit;
    }

}
