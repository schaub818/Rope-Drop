﻿using System.Collections;
using UnityEngine;

namespace RopeDropGame
{
    public class MapLocation : MonoBehaviour
    {
        public Vector3 Position
        {
            get { return transform.position; }
        }

        public string LocationName
        {
            get { return locationName; }
        }

        [SerializeField]
        protected string locationName;

        protected MapLocation(string name)
        {
            locationName = name;
        }
    }
}