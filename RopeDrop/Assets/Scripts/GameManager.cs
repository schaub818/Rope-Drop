using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RopeDropGame
{
    public class GameManager : MonoBehaviour
    {
        public UIManager UIManager
        {
            get { return uiManager; }
            set { uiManager = value; }
        }

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

        public bool ParkClosed
        {
            get { return parkClosed; }
        }

        [SerializeField]
        private UIManager uiManager;

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

        private bool parkClosed;

        // Use this for initialization
        void Start()
        {
            timeline.Intialize();
            crowd.Intialize();
            map.Initialize();
            magicPass.Initialize();

            crowd.SetDayCrowdLevels();

            parkClosed = false;

            foreach (MapLocation location in map.Locations)
            {
                if (location is ParkEntrance)
                {
                    pawn.Warp(location);

                    break;
                }
            }

            map.UpdateAllAttractions();

            uiManager.UpdateCurrentTime();
            uiManager.UpdateAppText();
            uiManager.CloseAttractionPanel();
            uiManager.CloseParkClosedPanel();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ClosePark()
        {
            parkClosed = true;

            map.ClampCameraClosedApp();

            uiManager.CloseApp();
            uiManager.OpenParkClosedPanel();
        }
    }
}