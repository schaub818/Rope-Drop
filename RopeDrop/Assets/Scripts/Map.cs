using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RopeDropGame
{
    public class Map : MonoBehaviour
    {
        public List<MapLocation> Locations
        {
            get { return locations; }
        }

        [SerializeField]
        private List<MapLocation> locations;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}