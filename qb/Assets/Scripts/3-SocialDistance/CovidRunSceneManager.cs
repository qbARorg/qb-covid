using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CovidRunSceneManager : TrackerListener
{
    public CovidRunManager manager;
    private CovidRunManager managerInstance;

    private float previousMaxScore = 100.0f;
    private float timeout = 10.0f;
    private float curr = 0f;

    public override void OnDetectedStart(ARTrackedImage img)
    {
        Debug.Log("goooolaso de maradona 😱🙇🏽‍♀️");
        managerInstance = Instantiate(manager, img.transform);
        managerInstance.ARAwake(img);
    }

    public override void OnDetectedUpdate(ARTrackedImage img)
    {
        
    }

    public override void OnStoppingDetection()
    {
        managerInstance.Dispose();
        managerInstance.enabled = false;
        Destroy(managerInstance);
    }

    public override void ARUpdate()
    {
        if (managerInstance)
        {
            managerInstance.ARUpdate();
        }
        float dt = Time.deltaTime;
        curr += dt;
        if (curr > timeout)
        {
            curr = 0.0f;
            if (SaveSystem.Exists("CovidRun"))
            {
                var scores = SaveSystem.Load<Scores>("CovidRun");
                previousMaxScore = scores.maximumScoreFloat;
            }
            managerInstance.score.text = "Time's up! You final score was " + managerInstance.probabilityOfCovid;
            if (previousMaxScore > managerInstance.probabilityOfCovid)
            {
                managerInstance.score.text += "\nNew Highscore! -> " + managerInstance.probabilityOfCovid;
                SaveSystem.Save("CovidRun", new Scores(managerInstance.probabilityOfCovid));
            }
        }
    }
}
