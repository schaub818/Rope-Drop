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
        GameManager gameManager;

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

        protected override void OnMouseDown()
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameManager.Pawn.Move(this);
            }
        }

        public void UpdateStandbyWaitTime()
        {
            CrowdLevel crowdLevel = gameManager.Crowd.CurrentLevel;
            int tierModifier = gameManager.Crowd.TierModifier[tier];
            int timeChunkSize = gameManager.Timeline.TimeChunkSize;

            standbyWait = Mathf.Clamp(((int)crowdLevel + tierModifier + Random.Range(-crowdVarianceRange, crowdVarianceRange)) * timeChunkSize,
                0, 1000000);

            // TODO: Remove debug lines
            Debug.Log(string.Format("Current wait time for {0}: {1}", locationName, standbyWait));
            Debug.Log(string.Format("Current crowd level: {0}", crowdLevel));
        }

        public string GetStandbyWaitTime()
        {
            return string.Format("{0} minutes", standbyWait * gameManager.Timeline.TimeChunkSize);
        }

        public string GetNextGatewayText()
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

        public int GetNextGatewayChunk()
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

        public void RideStandby()
        {
            gameManager.ScoringSystem.AddScore((int)tier);

            gameManager.Timeline.AdvanceTime(standbyWait);
        }

        public bool RideGateway()
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