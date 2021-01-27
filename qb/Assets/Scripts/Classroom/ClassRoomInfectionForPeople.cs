using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassRoom
{
    public class ClassRoomInfectionForPeople : MonoBehaviour
    {
        #region Attributes
        [SerializeField]
        private Material infectedMat;
        [SerializeField]
        private MeshRenderer [] tochange;
        private bool infected = false;
        #endregion

        #region Custom Methods
        public void getInfectedBoi()
        {
            foreach(MeshRenderer meshy in tochange)
            {
                meshy.material = infectedMat;
            }
            infected = true;
        }

        public bool getInfectedValue()
        {
            return infected;
        }
        #endregion
    }
}