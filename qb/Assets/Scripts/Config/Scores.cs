using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Scores
{
    public int maximumScore;

    public float maximumScoreFloat;

    public Scores(int maxScore)
    {
        maximumScore = maxScore;
    }
    
    public Scores(float maxScore)
    {
        maximumScoreFloat = maxScore;
    }
}
