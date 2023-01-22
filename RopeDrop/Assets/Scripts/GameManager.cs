using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RopeDropGame
{
    public class GameManager : MonoBehaviour
    {
        public Timeline Timeline
        {
            get { return timeline; }
            set { timeline = value; }
        }

        public Map Map
        {
            get { return map; }
            set { map = value; }
        }

        public PlayerPawn Pawn
        {
            get { return pawn; }
            set { pawn = value; }
        }

        public ParkCrowd Crowd
        {
            get { return crowd; }
            set { crowd = value; }
        }

        public MagicPassPlus MagicPass
        {
            get { return magicPass; }
            set { magicPass = value; }
        }

        public ScoringSystem ScoringSystem
        {
            get { return scoringSystem; }
            set { scoringSystem = value; }
        }

        [SerializeField]
        private Timeline timeline;

        [SerializeField]
        private Map map;

        [SerializeField]
        private PlayerPawn pawn;

        [SerializeField]
        private ParkCrowd crowd;

        [SerializeField]
        private MagicPassPlus magicPass;

        [SerializeField]
        private ScoringSystem scoringSystem;

        // Use this for initialization
        void Start()
        {
            timeline.Intialize();
            crowd.Intialize();
            magicPass.Initialize(this);

            crowd.SetDayCrowdLevels(this);

            foreach (MapLocation location in map.Locations)
            {
                if (location is ParkEntrance)
                {
                    pawn.Move(location, this);

                    break;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}