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
        private Tier tier;

        [SerializeField]
        private int maxWaitSwing;

        private int standbyWait;
        private int nextAvailableGateway = 0;
        private RandomDistribution random;

        public Attraction(string attractionName, Tier attractionTier) : base(attractionName)
        {
            tier = attractionTier;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Initialize()
        {
            random = GetComponent<RandomDistribution>();
        }

        protected override void OnMouseDown()
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameManager.UIManager.OpenAttractionPanel(this);
            }
        }

        public void UpdateStandbyWaitTime()
        {
            CrowdLevel crowdLevel = gameManager.Crowd.CurrentLevel;
            int tierModifier = gameManager.Crowd.TierModifier[tier];
            int timeChunkSize = gameManager.Timeline.TimeChunkSize;

            int newStandbyWait = Mathf.Clamp(((int)crowdLevel + tierModifier + random.RandomInt()),
                1, 1000000);
            int waitSwing = (standbyWait - newStandbyWait);

            if (waitSwing < 0)
            {
                waitSwing = -waitSwing;
            }

            if (waitSwing > maxWaitSwing)
            {
                if (newStandbyWait > standbyWait)
                {
                    standbyWait += maxWaitSwing;
                }
                else
                {
                    standbyWait -= maxWaitSwing;
                }
            }
            else
            {
                standbyWait = newStandbyWait;
            }
        }

        public void UpdateGatewayAvailability()
        {
            CrowdLevel crowdLevel = gameManager.Crowd.CurrentLevel;
            int tierModifier = gameManager.Crowd.TierModifier[tier];

            nextAvailableGateway = Mathf.Clamp((int)crowdLevel * (tierModifier * 2) + random.RandomInt() * 3 +
                Mathf.RoundToInt((float)standbyWait + Random.Range(0.0f, 1.0f)), 0, 100000);

            if (gameManager.Timeline.IsFutureTimePastParkClose(nextAvailableGateway))
            {
                nextAvailableGateway = -1;
            }
        }

        public string GetStandbyWaitTime()
        {
            return string.Format("Standby Line: {0} minutes", standbyWait * gameManager.Timeline.TimeChunkSize);
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
                    return string.Format("Golden Gateway available at: {0}", gameManager.Timeline.GetFutureTime(nextAvailableGateway).ToString());
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