using System.Collections;
using UnityEngine;

namespace RopeDropGame
{
    public class PlayerPawn : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Move(MapLocation location)
        {
            transform.position = location.Position;
        }
    }
}