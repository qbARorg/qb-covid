using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public enum State 
    {
        AwaitingMask,
        AwaitingStrings,
        NextPerson,
    }

    #region Private

    [SerializeField]
    private GameObject[] activateOnAwaitingMask;
    [SerializeField]
    private GameObject[] activateOnAwaitingStrings;
    [SerializeField]
    private GameObject[] deactivateOnAwaitingMask;
    [SerializeField]
    private GameObject[] deactivateOnAwaitingStrings;
    [SerializeField]
    private GameObject[] activateOnFinish;
    [SerializeField]
    private GameObject[] deactivateOnFinish;

    private State state;

    [SerializeField] private GameObject mask;

    #endregion
    
    #region Public Methods

    public void ChangeState(State nextState)
    {
        switch (nextState)
        {
            case State.AwaitingMask:
            {
                SetActive(activateOnAwaitingMask, true);
                SetActive(deactivateOnAwaitingMask, false);
                break;
            }

            case State.AwaitingStrings:
            {
                SetActive(activateOnAwaitingStrings, true);
                SetActive(deactivateOnAwaitingStrings, false);
                break;
            }

            case State.NextPerson:
            {
                SetActive(activateOnFinish, true);
                SetActive(deactivateOnFinish, false);
                break;
            }
        }
        
    }

    #endregion
    
    #region Private Methods

    private void SetActive(GameObject[] toActivate, bool active)
    {
        foreach (GameObject go in toActivate)
        {
            go.SetActive(active);
        }
    }
    
    #endregion

    #region Unity3D

    private void Awake()
    {
        ChangeState(State.AwaitingMask);
    }

    #endregion
}
