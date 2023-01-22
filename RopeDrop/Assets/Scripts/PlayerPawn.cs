using System.Collections;
using UnityEngine;

namespace RopeDropGame
{
    public class PlayerPawn : MonoBehaviour
    {
        public MapLocation CurrentLocation
        {
            get { return currentLocation; }
        }

        private MapLocation currentLocation;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Move(MapLocation location)
        {
            currentLocation = location;
            transform.position = location.Position;
        }
    }
}