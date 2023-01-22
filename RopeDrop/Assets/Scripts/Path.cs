using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RopeDropGame
{
    public class Path : MonoBehaviour
    {
        public MapLocation Endpoint1
        {
            get { return endpoint1; }
        }

        public MapLocation Endpoint2
        {
            get { return endpoint2; }
        }

        public WalkTime WalkTime
        {
            get { return walkTime; }
        }

        [SerializeField]
        private MapLocation endpoint1;

        [SerializeField]
        private MapLocation endpoint2;

        [SerializeField]
        private WalkTime walkTime;

        public override string ToString()
        {
            return string.Format("{0} minutes", (int)walkTime);
        }
    }
}