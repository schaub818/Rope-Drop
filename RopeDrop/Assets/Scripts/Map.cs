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

        public List<Path> Paths
        {
            get { return paths; }
        }

        [SerializeField]
        private List<MapLocation> locations;

        [SerializeField]
        private List<Path> paths;

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