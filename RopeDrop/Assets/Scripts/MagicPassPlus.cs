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
        GameManager gameManager;

        [SerializeField]
        private int totalPortals = 1;

        private int portalsUsed = 0;
        private int timeLastBooked = -1;
        private Dictionary<MapLocation, bool> gatewayUsage;
        private Dictionary<MapLocation, int> gatewayBookings;

        // Update is called once per frame
        void Update()
        {

        }

        public void Initialize()
        {
            gatewayUsage = new Dictionary<MapLocation, bool>();
            gatewayBookings = new Dictionary<MapLocation, int>();

            foreach (MapLocation location in gameManager.Map.Locations)
            {
                if (location is Attraction)
                {
                    gatewayUsage.Add(location, false);
                    gatewayBookings.Add(location, -1);
                }
            }
        }

        public string GetGatewayBookingText(Attraction attraction)
        {
            if (!gatewayUsage[attraction] && gatewayBookings[attraction] >= 0)
            {
                string bookingTime = gameManager.Timeline.ChunkToText(gatewayBookings[attraction]);

                return string.Format("Gateway booked for {0}", bookingTime);
            }
            else if (gatewayBookings[attraction] < 0 && !gatewayUsage[attraction] && attraction.GetNextGateway() > -1)
            {
                string availableTime = gameManager.Timeline.ChunkToText(attraction.GetNextGateway());

                return string.Format("Golden Gateway available at: {0}", availableTime);
            }
            else
            {
                return "No Gateways currently available";
            }
        }

        public int GetGatewayBooking(Attraction attraction)
        {
            return gatewayBookings[attraction];
        }

        public bool BookGateway(Attraction attraction)
        {
            int bookTime = attraction.GetNextGateway();

            if (!gatewayUsage[attraction] && gatewayBookings[attraction] < 0 && bookTime > -1)
            {
                gatewayBookings[attraction] = bookTime;
                timeLastBooked = gameManager.Timeline.CurrentTimeChunk;

                return true;
            }
            else
            {
                Debug.LogWarning(string.Format("No Gateway for {0} available", attraction.LocationName));

                return false;
            }
        }

        public bool CancelGateway(Attraction attraction)
        {
            if (gatewayBookings[attraction] >= 0)
            {
                gatewayBookings[attraction] = -1;
                timeLastBooked = -1;

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UseGateway(Attraction attraction)
        {
            if (!gatewayUsage[attraction] && gatewayBookings[attraction] >= gameManager.Timeline.CurrentTimeChunk)
            {
                gatewayUsage[attraction] = true;
                gatewayBookings[attraction] = -1;

                attraction.RideGateway();

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsGatewayAvailable(Attraction attraction)
        {
            if (!gatewayUsage[attraction] && gatewayBookings[attraction] < 0 && attraction.GetNextGateway() > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsGatewayBooked(Attraction attraction)
        {
            return !gatewayUsage[attraction] && gatewayBookings[attraction] >= 0;
        }

        public bool IsGatewayUsed(Attraction attraction)
        {
            return gatewayUsage[attraction];
        }

        public bool UsePortal(Attraction attraction)
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