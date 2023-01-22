using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RopeDropGame
{
    public class GameManager : MonoBehaviour
    {
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

        // Use this for initialization
        void Start()
        {
            timeline.Intialize();
            crowd.Intialize();
            magicPass.Initialize(map);

            crowd.SetDayCrowdLevels(timeline);

            foreach (MapLocation location in map.Locations)
            {
                if (location is ParkEntrance)
                {
                    pawn.Move(location);

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