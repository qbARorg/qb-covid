using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField] private GameObject mask;

    
    
    public void ChangeMask(bool active)
    {
        mask.gameObject.SetActive(active);
    }
}
