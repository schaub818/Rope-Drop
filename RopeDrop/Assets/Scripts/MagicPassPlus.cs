using RopeDropGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class MagicPassPlus : MonoBehaviour
    {
        public int TotalPortals
        {
            get { return totalPortals; }
        }

        public int PortalsUsed
        {
            get { return portalsUsed; }
        }

        public int TimeLastBooked
        {
            get { return timeLastBooked; }
        }

        [SerializeField]
        private int totalPortals = 1;

        private int portalsUsed = 0;
        private int timeLastBooked = -1;
        private Dictionary<MapLocation, int> gatewayUsage;

        // Update is called once per frame
        void Update()
        {

        }

        public void Initialize(Map map)
        {
            gatewayUsage = new Dictionary<MapLocation, int>();

            foreach (MapLocation location in map.Locations)
            {
                if (location is Attraction)
                {
                    gatewayUsage.Add(location, -1);
                }
            }
        }

        public bool BookGateway(Attraction attraction, Timeline timeline)
        {
            int bookTime = attraction.GetNextGatewayChunk(timeline);

            if (gatewayUsage[attraction] < 0 && bookTime > -1)
            {
                gatewayUsage[attraction] = bookTime;

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UsePortal(Attraction attraction, PlayerPawn pawn)
        {
            if (portalsUsed > 0)
            {
                portalsUsed--;

                pawn.Move(attraction);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}