using System.Collections;
using UnityEngine;

namespace RopeDropGame
{
    public class ParkEntrance : MapLocation
    {
        [SerializeField]
        private GameManager gameManager;

        public ParkEntrance(string entranceName) : base(entranceName)
        {

        }

        // Use this for initialization
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
    }
}