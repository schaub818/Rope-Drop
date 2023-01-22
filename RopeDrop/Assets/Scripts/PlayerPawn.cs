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

        public void Move(MapLocation location, GameManager gameManager)
        {
            foreach (Path path in gameManager.Map.Paths)
            {
                if ((path.Endpoint1 == currentLocation && path.Endpoint2 == location) ||
                    (path.Endpoint1 == location && path.Endpoint2 == currentLocation))
                {
                    gameManager.Timeline.AdvanceTime((int)path.WalkTime);

                    break;
                }
            }

            currentLocation = location;
            transform.position = location.Position;
        }
    }
}