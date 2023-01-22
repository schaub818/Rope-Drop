using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RopeDropGame
{
    public class Attraction : MapLocation
    {
        public Tier Tier
        {
            get { return tier; }
        }

        public int StandbyWait
        {
            get { return standbyWait; }
        }

        [SerializeField]
        private Tier tier = Tier.E;

        [SerializeField]
        private int crowdVarianceRange;

        private int standbyWait;
        private int nextAvailableGateway;

        public Attraction(string attractionName, Tier attractionTier) : base(attractionName)
        {
            tier = attractionTier;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public string GetStandbyWaitTime(GameManager gameManager)
        {
            return string.Format("{0} minutes", standbyWait * gameManager.Timeline.TimeChunkSize);
        }

        public string GetNextGatewayText(GameManager gameManager)
        {
            if (nextAvailableGateway > -1)
            {
                if (gameManager.Timeline.IsFutureTimePastParkClose(nextAvailableGateway))
                {
                    nextAvailableGateway = -1;

                    return "No Gateways currently available";
                }
                else
                {
                    return gameManager.Timeline.GetFutureTime(nextAvailableGateway).ToString();
                }
            }
            else
            {
                return "No Gateways currently available";
            }
        }

        public int GetNextGatewayChunk(GameManager gameManager)
        {
            if (nextAvailableGateway > -1)
            {
                if (gameManager.Timeline.IsFutureTimePastParkClose(nextAvailableGateway))
                {
                    nextAvailableGateway = -1;
                }
            }

            return nextAvailableGateway;
        }

        public void RideStandby(GameManager gameManager)
        {
            gameManager.ScoringSystem.AddScore((int)tier);

            gameManager.Timeline.AdvanceTime(standbyWait);
        }

        public bool RideGateway(GameManager gameManager)
        {
            if (nextAvailableGateway >= 0)
            {
                gameManager.ScoringSystem.AddScore((int)tier);

                gameManager.Timeline.AdvanceTime(1);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}