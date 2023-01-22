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

        public void Initialize(GameManager gameManager)
        {
            gatewayUsage = new Dictionary<MapLocation, int>();

            foreach (MapLocation location in gameManager.Map.Locations)
            {
                if (location is Attraction)
                {
                    gatewayUsage.Add(location, -1);
                }
            }
        }

        public bool BookGateway(Attraction attraction, GameManager gameManager)
        {
            int bookTime = attraction.GetNextGatewayChunk(gameManager);

            if (gatewayUsage[attraction] < 0 && bookTime > -1)
            {
                gatewayUsage[attraction] = bookTime;
                timeLastBooked = gameManager.Timeline.CurrentTimeChunk;

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CancelGateway(Attraction attraction)
        {
            if (gatewayUsage[attraction] >= 0)
            {
                gatewayUsage[attraction] = -1;
                timeLastBooked = -1;

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UsePortal(Attraction attraction, GameManager gameManager)
        {
            if (portalsUsed > 0)
            {
                portalsUsed--;

                gameManager.Pawn.Move(attraction);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}