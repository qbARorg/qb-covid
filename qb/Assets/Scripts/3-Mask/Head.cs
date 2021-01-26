using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Head : MonoBehaviour
{
    public enum State 
    {
        AwaitingMask,
        AwaitingStrings,
        NextPerson,
    }

    #region Private

    private int numberOfRopes = 0;

    [SerializeField] private Text timeUI;
    
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

    public State HeadState => state;

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

    public void UpdateTimeShowcase(int timeInSeconds)
    {
        timeUI.text = $"You have {timeInSeconds} seconds left!";
    }

    public void OneRope()
    {
        numberOfRopes++;
        if (numberOfRopes >= 2)
        {
            state = State.NextPerson;
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
