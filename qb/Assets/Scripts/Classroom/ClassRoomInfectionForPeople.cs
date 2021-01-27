using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClassRoom
{
    public class ClassRoomInfectionForPeople : MonoBehaviour
    {
        [SerializeField]
        private Material infectedMat;
        [SerializeField]
        private MeshRenderer [] tochange;

        public void getInfectedBoi()
        {
            foreach(MeshRenderer meshy in tochange)
            {
                meshy.material = infectedMat;
            }
        }
    }
}