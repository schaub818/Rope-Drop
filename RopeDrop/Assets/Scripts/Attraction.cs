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

        public string GetStandbyWaitTime(Timeline timeline)
        {
            return string.Format("{0} minutes", standbyWait * timeline.TimeChunkSize);
        }

        public string GetNextGatewayText(Timeline timeline)
        {
            if (nextAvailableGateway > -1)
            {
                if (timeline.IsFutureTimePastParkClose(nextAvailableGateway))
                {
                    nextAvailableGateway = -1;

                    return "No Gateways currently available";
                }
                else
                {
                    return timeline.GetFutureTime(nextAvailableGateway).ToString();
                }
            }
            else
            {
                return "No Gateways currently available";
            }
        }

        public int GetNextGatewayChunk(Timeline timeline)
        {
            if (nextAvailableGateway > -1)
            {
                if (timeline.IsFutureTimePastParkClose(nextAvailableGateway))
                {
                    nextAvailableGateway = -1;
                }
            }

            return nextAvailableGateway;
        }

        public void RideStandby(ScoringSystem scoringSystem, Timeline timeline)
        {
            scoringSystem.AddScore((int)tier);

            timeline.AdvanceTime(standbyWait);
        }

        public bool RideGateway(ScoringSystem scoringSystem, Timeline timeline)
        {
            if (nextAvailableGateway >= 0)
            {
                scoringSystem.AddScore((int)tier);

                timeline.AdvanceTime(1);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}