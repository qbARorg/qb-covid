using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class StatsFaceManager : TrackerListener
{
    [SerializeField] private Stats prefabSampleText;

    private List<Stats> texts;

    private Stats InstantiateText()
    {
        Stats s = Instantiate(prefabSampleText, transform);
        texts.Add(s);
        s.transform.position += Vector3.down * (texts.Count * 1.5f);
        return s;
    }
    
    public override void OnDetectedStart(ARTrackedImage img)
    {
        NewScoreText("Mask Placement", MaskSceneBehaviour.ConfigFileName);
        NewScoreText("Classroom Simulation", "ClassRoomMask");
    }

    private void NewScoreText(string game, string nameConfig)
    {
        int score = 0;
        if (SaveSystem.Exists(nameConfig))
        {
            Scores scoreMaskScene = SaveSystem.Load<Scores>(nameConfig);
            score = scoreMaskScene.maximumScore;
        }
        InstantiateText().Text.text = $"Score {game} Game: {score}";
    }

    public override void OnStoppingDetection()
    {
        foreach (Stats text in texts)
        {
            Destroy(text);
        }
    }
}
