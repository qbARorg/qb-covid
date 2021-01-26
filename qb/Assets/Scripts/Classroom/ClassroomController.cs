using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassRoom
{
    public class ClassroomController : MonoBehaviour
    {
        #region Attibutes
        [Header("Control Variables")]
        [SerializeField]
        private bool doorOpen = false;
        [SerializeField]
        private bool window1Open = false;
        [SerializeField]
        private bool window2Open = false;
        [SerializeField]
        private bool window3Open = false;
        [Range(0f, 1f)]
        private float doorLerpState = 0f;
        [Range(0f, 1f)]
        private float window1LerpState = 0f;
        [Range(0f, 1f)]
        private float window2LerpState = 0f;
        [Range(0f, 1f)]
        private float window3LerpState = 0f;
        public float animationsDuration = 1f;

        [Header("Door")]
        public GameObject doorHinge;
        public float doorClosedAngle = 0f;
        public float doorOpenedAngle = 0f;
        [Header("Window 1")]
        public GameObject window11Hinge;
        public float window11ClosedAngle = 0f;
        public float window11OpenedAngle = 0f;
        public GameObject window12Hinge;
        public float window12ClosedAngle = 0f;
        public float window12OpenedAngle = 0f;
        [Header("Window 2")]
        public GameObject window21Hinge;
        public float window21ClosedAngle = 0f;
        public float window21OpenedAngle = 0f;
        public GameObject window22Hinge;
        public float window22ClosedAngle = 0f;
        public float window22OpenedAngle = 0f;
        [Header("Window 3")]
        public GameObject window31Hinge;
        public float window31ClosedAngle = 0f;
        public float window31OpenedAngle = 0f;
        public GameObject window32Hinge;
        public float window32ClosedAngle = 0f;
        public float window32OpenedAngle = 0f;

        [Header("Performance or Smooth anims")]
        public bool performanceMode = false;
        #endregion

        #region Propperties
        public bool getDoorState()
        {
            return doorOpen;
        }
        public void setDoorState(bool newState)
        {
            doorOpen = newState;
        }

        public bool getWindow1State()
        {
            return window1Open;
        }
        public void setWindow1State(bool newState)
        {
            window1Open = newState;
        }

        public bool getWindow2State()
        {
            return window2Open;
        }
        public void setWindow2State(bool newState)
        {
            window2Open = newState;
        }

        public bool getWindow3State()
        {
            return window3Open;
        }
        public void setWindow3State(bool newState)
        {
            window3Open = newState;
        }
        #endregion

        #region Main Methods
        void Update()
        {
            doorController();
            window1Controller();
            window2Controller();
            window3Controller();
        }
        #endregion

        #region Custom Methods
        public void doorController()
        {
            if(doorOpen && doorLerpState < 1)
            {
                doorLerpState += Time.deltaTime / animationsDuration;
            }
            else if(!doorOpen && doorLerpState > 0)
            {
                doorLerpState -= Time.deltaTime / animationsDuration;
            }

            if(!performanceMode)
            {
                float newAngle = (doorOpenedAngle - doorClosedAngle) / 2f - (doorOpenedAngle - doorClosedAngle) / 2f * Mathf.Cos(doorLerpState * Mathf.PI);
                doorHinge.transform.localEulerAngles = new Vector3(0, newAngle, 0);
            }
            else doorHinge.transform.localEulerAngles = Vector3.Slerp(new Vector3(0f, doorClosedAngle, 0f), new Vector3(0f, doorOpenedAngle, 0f), doorLerpState);
        }

        public void window1Controller()
        {
            if (window1Open && window1LerpState < 1)
            {
                window1LerpState += Time.deltaTime / animationsDuration;
            }
            else if (!window1Open && window1LerpState > 0)
            {
                window1LerpState -= Time.deltaTime / animationsDuration;
            }

            if (!performanceMode)
            {
                float newAngle1 = (window11OpenedAngle - window11ClosedAngle) / 2f - (window11OpenedAngle - window11ClosedAngle) / 2f * Mathf.Cos(window1LerpState * Mathf.PI);
                window11Hinge.transform.localEulerAngles = new Vector3(0, newAngle1, 0);
                float newAngle2 = (window12OpenedAngle - window12ClosedAngle) / 2f - (window12OpenedAngle - window12ClosedAngle) / 2f * Mathf.Cos(window1LerpState * Mathf.PI);
                window12Hinge.transform.localEulerAngles = new Vector3(0, newAngle2, 0);
            }
            else
            {
                window11Hinge.transform.localEulerAngles = Vector3.Slerp(new Vector3(0f, window11ClosedAngle, 0f), new Vector3(0f, window11OpenedAngle, 0f), window1LerpState);
                window12Hinge.transform.localEulerAngles = Vector3.Slerp(new Vector3(0f, window12ClosedAngle, 0f), new Vector3(0f, window12OpenedAngle, 0f), window1LerpState);
            }
        }

        public void window2Controller()
        {
            if (window2Open && window2LerpState < 1)
            {
                window2LerpState += Time.deltaTime / animationsDuration;
            }
            else if (!window2Open && window2LerpState > 0)
            {
                window2LerpState -= Time.deltaTime / animationsDuration;
            }

            if (!performanceMode)
            {
                float newAngle1 = (window21OpenedAngle - window21ClosedAngle) / 2f - (window21OpenedAngle - window21ClosedAngle) / 2f * Mathf.Cos(window2LerpState * Mathf.PI);
                window21Hinge.transform.localEulerAngles = new Vector3(0, newAngle1, 0);
                float newAngle2 = (window22OpenedAngle - window22ClosedAngle) / 2f - (window22OpenedAngle - window22ClosedAngle) / 2f * Mathf.Cos(window2LerpState * Mathf.PI);
                window22Hinge.transform.localEulerAngles = new Vector3(0, newAngle2, 0);
            }
            else
            {
                window21Hinge.transform.localEulerAngles = Vector3.Slerp(new Vector3(0f, window21ClosedAngle, 0f), new Vector3(0f, window21OpenedAngle, 0f), window2LerpState);
                window22Hinge.transform.localEulerAngles = Vector3.Slerp(new Vector3(0f, window22ClosedAngle, 0f), new Vector3(0f, window22OpenedAngle, 0f), window2LerpState);
            }
            
        }

        public void window3Controller()
        {
            if (window3Open && window3LerpState < 1)
            {
                window3LerpState += Time.deltaTime / animationsDuration;
            }
            else if (!window3Open && window3LerpState > 0)
            {
                window3LerpState -= Time.deltaTime / animationsDuration;
            }

            if (!performanceMode)
            {
                float newAngle1 = (window31OpenedAngle - window31ClosedAngle) / 2f - (window31OpenedAngle - window31ClosedAngle) / 2f * Mathf.Cos(window3LerpState * Mathf.PI);
                window31Hinge.transform.localEulerAngles = new Vector3(0, newAngle1, 0);
                float newAngle2 = (window32OpenedAngle - window32ClosedAngle) / 2f - (window32OpenedAngle - window32ClosedAngle) / 2f * Mathf.Cos(window3LerpState * Mathf.PI);
                window32Hinge.transform.localEulerAngles = new Vector3(0, newAngle2, 0);
            }
            else
            {
                window31Hinge.transform.localEulerAngles = Vector3.Slerp(new Vector3(0f, window31ClosedAngle, 0f), new Vector3(0f, window31OpenedAngle, 0f), window1LerpState);
                window32Hinge.transform.localEulerAngles = Vector3.Slerp(new Vector3(0f, window32ClosedAngle, 0f), new Vector3(0f, window32OpenedAngle, 0f), window1LerpState);
            }
        }
        #endregion
    }

}
