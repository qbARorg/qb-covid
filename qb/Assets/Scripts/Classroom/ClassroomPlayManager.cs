using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ClassRoom
{
    public class ClassroomPlayManager : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField]
        GameObject[] gameUIToDisable;
        [SerializeField]
        Button playButton;
        [SerializeField]
        Slider timeLine;
        [SerializeField]
        GameObject endScreen;
        [SerializeField]
        Text scoreText;

        [Header("Particle Systems")]
        [SerializeField]
        ParticleSystem partSys1;
        [SerializeField]
        ParticleSystem partSys2;
        [SerializeField]
        ParticleSystem partSys3;
        [SerializeField]
        ParticleSystem partSys4;
        [SerializeField]
        ParticleSystem partSys5;

        [Header("End Messages")]
        [SerializeField]
        string s1 = "★☆☆☆☆";
        [SerializeField]
        string s2 = "★★☆☆☆";
        [SerializeField]
        string s3 = "★★★☆☆";
        [SerializeField]
        string s4 = "★★★★☆";
        [SerializeField]
        string s5 = "★★★★★";

        [Header("Simulation")]
        private bool startSim = false;
        private bool endedSim = false;
        [SerializeField]
        private float simulationTime = 20.0f;
        [SerializeField]
        private float infectionsStart = 10.0f;
        private float timer = 0.0f;
        [SerializeField]
        private GameObject[] people;
        private float[] peopleTimers;
        private bool peopleTimersAreSet = false;
        [SerializeField]
        private ClassroomController classController;
        [SerializeField]
        private float infectionRate = 15.0f;
        [SerializeField]
        private float ratio1 = 0.8f;
        [SerializeField]
        private float ratio2 = 0.6f;
        [SerializeField]
        private float ratio3 = 0.4f;
        [SerializeField]
        private float ratio4 = 0.2f;
        private int finalScore;

        // Start is called before the first frame update
        void Start()
        {
            startSim = false;
            peopleTimersAreSet = false;
            timer = 0.0f;
            peopleTimers = new float[people.Length];
            timeLine.maxValue = simulationTime;
        }

        // Update is called once per frame
        void Update()
        {
            if (startSim && !endedSim)
            {
                if(!peopleTimersAreSet)
                {
                    setFinalInfectionRate();
                    setPeopleRandomTimers();
                }
                timer += Time.deltaTime;
                searchForInfectedPeople();
                if (timer >= simulationTime)
                {
                    timeLine.value = simulationTime;
                    endedSim = true;
                    endScreen.SetActive(true);
                    if(this.gameObject.name == "ClassRoom") SaveSystem.Save("ClassRoomMask", new Scores(finalScore));
                    else if (this.gameObject.name == "ClassRoomNoMasks") SaveSystem.Save("ClassRoomNoMask", new Scores(finalScore));
                }
                else timeLine.value = timer;
            }
        }

        public void setStartSimulation()
        {
            startSim = true;
        }

        private void setPeopleRandomTimers()
        {
            for (int i = 0; i < peopleTimers.Length; i++)
            {
                peopleTimers[i] = infectionsStart + Random.Range(0.0f, infectionRate);
            }
            peopleTimersAreSet = true;
        }

        private void setFinalInfectionRate()
        {
            if (!classController.getDoorState() && classController.getWindow1State()
                        && classController.getWindow2State() && classController.getWindow3State())
            {
                partSys3.Play();
                infectionRate *= ratio2;
                setEndScore(s3);
            }
            else if (classController.getDoorState() && !classController.getWindow1State()
                        && !classController.getWindow2State() && !classController.getWindow3State())
            {
                partSys2.Play();
                setEndScore(s2);
                infectionRate *= ratio3;
            }

            else
            {
                switch (classController.getThingsOpened())
                {
                    case 1:
                        partSys1.Play();
                        infectionRate *= ratio4;
                        setEndScore(s2);
                        break;
                    case 2:
                        partSys5.Play();
                        setEndScore(s3);
                        infectionRate *= ratio3;
                        break;
                    case 3:
                        partSys5.Play();
                        infectionRate *= ratio1;
                        setEndScore(s4);
                        break;
                    case 4:
                        partSys4.Play();
                        setEndScore(s5);
                        break;
                    default:
                        partSys1.Play();
                        setEndScore(s1);
                        break;
                }
            }
        }

        private void searchForInfectedPeople()
        {
            for (int i = 0; i < peopleTimers.Length; i++)
            {
                if(timer >= peopleTimers[i])
                {
                    if(people[i] != null && people[i].GetComponent<ClassRoomInfectionForPeople>() != null)
                    {
                        people[i].GetComponent<ClassRoomInfectionForPeople>().getInfectedBoi();
                    }
                }
            }
        }

        private void setEndScore(string s)
        {
            scoreText.text = s;
            if(s == "★★★★★")
            {
                finalScore = 5;
            }
            else if (s == "★★★★☆")
            {
                finalScore = 4;
            }
            else if (s == "★★★☆☆")
            {
                finalScore = 3;
            }
            else if (s == "★★☆☆☆")
            {
                finalScore = 2;
            }
            else
            {
                finalScore = 1;
            }
        }
    }
}
