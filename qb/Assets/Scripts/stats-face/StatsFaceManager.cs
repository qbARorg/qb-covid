using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class StatsFaceManager : TrackerListener
{
    [SerializeField] private Stats prefabSampleText;

    private List<Stats> texts;

    [SerializeField]
    private GameObject pivot;

    private GameObject pivotInstance;
    

    public override void OnDetectedStart(ARTrackedImage img)
    {
        if (texts != null)
        {
            foreach (Stats text in texts)
            {
                Destroy(text.gameObject);
            }
        }
        texts = new List<Stats>();
        pivotInstance = Instantiate(pivot, img.transform);
        pivotInstance.transform.localPosition = Vector3.zero;
        NewScoreText("Mask Placement", MaskSceneBehaviour.ConfigFileName);
        NewScoreText("Classroom Simulation", "ClassRoomMask");
        NewScoreText("Classroom Without Masks Simulation", "ClassRoomNoMask");
    }

    private void NewScoreText(string game, string nameConfig)
    {
        int score = 0;
        if (SaveSystem.Exists(nameConfig))
        {
            Scores scoreMaskScene = SaveSystem.Load<Scores>(nameConfig);
            score = scoreMaskScene.maximumScore;
        }
        InstantiateText().Text.text = $"Record Score {game} Game: {score}";
    }
    
    private Stats InstantiateText()
    {
        Stats s = Instantiate(prefabSampleText, pivotInstance.transform);
        texts.Add(s);
        s.transform.position = Vector3.zero;
        s.transform.position += Vector3.down * (texts.Count * 0.1f);
        return s;
    }

    public override void OnStoppingDetection()
    {
        foreach (Stats text in texts)
        {
            Destroy(text.gameObject);
        }
        texts = new List<Stats>();
        Destroy(pivotInstance);
    }
}
