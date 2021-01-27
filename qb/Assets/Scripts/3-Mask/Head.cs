using UnityEngine;


public class Head : MonoBehaviour
{
    public enum State 
    {
        AwaitingMask,
        AwaitingStrings,
        NextPerson,
        Lost
    }

    #region Private

    [SerializeField] private TextMesh currentScore;
    [SerializeField] private TextMesh maxScore;

    private int numberOfRopes = 0;

    [SerializeField] private TextMesh timeUI;
    
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
        state = nextState;
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

            case State.Lost:
            {
                timeUI.text = "You lost! Reset the game with the button above!";
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
            ChangeState(State.NextPerson);
        }
    }

    public void SetMaxScore(int maxScoreValue)
    {
        maxScore.text = $"Max Score: {maxScoreValue}";
    }

    public void SetScore(int score)
    {
        currentScore.text = $"Current Score: {score}";
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
