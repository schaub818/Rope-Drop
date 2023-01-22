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

        [SerializeField]
        private GameManager gameManager;

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
            bool pathFound = false;

            foreach (Path path in gameManager.Map.Paths)
            {
                if ((path.Endpoint1 == currentLocation && path.Endpoint2 == location) ||
                    (path.Endpoint1 == location && path.Endpoint2 == currentLocation))
                {
                    gameManager.Timeline.AdvanceTime((int)path.WalkTime);
                    pathFound = true;

                    break;
                }
            }

            if (pathFound)
            {
                currentLocation = location;
                transform.position = location.Position;

                // TODO: Remove debug lines
                if (location is Attraction)
                {
                    Attraction attraction = (Attraction)location;

                    attraction.UpdateStandbyWaitTime();
                }
            }
            else
            {
                Debug.LogError(string.Format("Path for {0} to {1} not found", currentLocation.LocationName, location.LocationName));
            }

        }

        public void Warp(MapLocation location)
        {
            currentLocation = location;
            transform.position = location.Position;
        }
    }
}