using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ClassRoom
{
    public class ClassroomCanvasController : MonoBehaviour
    {
        #region Attributes
        private ClassroomController mainController = null;
        [Header("Door")]
        [SerializeField]
        private Text doorButtonText;
        [SerializeField]
        private string doorClosedtext = "Door: Closed";
        [SerializeField]
        private string doorOpenedtext = "Door: Opened";
        [SerializeField]
        private ParticleSystem doorParticles;

        [Header("Window 1")]
        [SerializeField]
        private Text window1ButtonText;
        [SerializeField]
        private string window1Closedtext = "Window 1: Closed";
        [SerializeField]
        private string window1Openedtext = "Window 1: Opened";
        [SerializeField]
        private ParticleSystem w1Particles;

        [Header("Window 2")]
        [SerializeField]
        private Text window2ButtonText;
        [SerializeField]
        private string window2Closedtext = "Window 2: Closed";
        [SerializeField]
        private string window2Openedtext = "Window 2: Opened";
        [SerializeField]
        private ParticleSystem w2Particles;

        [Header("Window 3")]
        [SerializeField]
        private Text window3ButtonText;
        [SerializeField]
        private string window3Closedtext = "Window 3: Closed";
        [SerializeField]
        private string window3Openedtext = "Window 3: Opened";
        [SerializeField]
        private ParticleSystem w3Particles;
        #endregion

        #region Main Methods
        void Start()
        {
            mainController = this.GetComponent<ClassroomController>();
            doorButtonText.text = doorClosedtext;
            window1ButtonText.text = window1Closedtext;
            window2ButtonText.text = window2Closedtext;
            window3ButtonText.text = window3Closedtext;
            doorParticles.Stop();
            w1Particles.Stop();
            w2Particles.Stop();
            w3Particles.Stop();
        }
        #endregion

        #region Custom Methods
        public void doorButtonClick()
        {
            if(mainController.getDoorState())
            {
                mainController.setDoorState(false);
                doorButtonText.text = doorClosedtext;
                doorParticles.Stop();
            }
            else
            {
                mainController.setDoorState(true);
                doorButtonText.text = doorOpenedtext;
                doorParticles.Play();
            }
        }

        public void window1ButtonClick()
        {
            if (mainController.getWindow1State())
            {
                mainController.setWindow1State(false);
                window1ButtonText.text = window1Closedtext;
                w1Particles.Stop();
            }
            else
            {
                mainController.setWindow1State(true);
                window1ButtonText.text = window1Openedtext;
                w1Particles.Play();
            }
        }

        public void window2ButtonClick()
        {
            if (mainController.getWindow2State())
            {
                mainController.setWindow2State(false);
                window2ButtonText.text = window2Closedtext;
                w2Particles.Stop();
            }
            else
            {
                mainController.setWindow2State(true);
                window2ButtonText.text = window2Openedtext;
                w2Particles.Play();
            }
        }

        public void window3ButtonClick()
        {
            if (mainController.getWindow3State())
            {
                mainController.setWindow3State(false);
                window3ButtonText.text = window3Closedtext;
                w3Particles.Stop();
            }
            else
            {
                mainController.setWindow3State(true);
                window3ButtonText.text = window3Openedtext;
                w3Particles.Play();
            }
        }
        #endregion
    }
}