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

        // Update is called once per frame
        void Update()
        {

        }

        public void Initialize()
        {
            foreach (MapLocation location in locations)
            {
                if (location is Attraction)
                {
                    Attraction attraction = (Attraction)location;

                    attraction.Initialize();
                }
            }
        }
    }
}