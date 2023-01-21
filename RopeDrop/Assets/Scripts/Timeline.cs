using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RopeDropGame
{
    public class Timeline : MonoBehaviour
    {
        public DateTime RopeDrop
        {
            get { return ropeDrop; }
        }

        public DateTime ParkClose
        {
            get { return parkClose; }
        }

        public DateTime CurrentTime
        {
            get { return timeChunks[currentTimeIndex].Time; }
        }

        public int TimeChunkSize
        {
            get { return timeChunkSize; }
        }

        public List<TimelineChunk> TimeChunks
        {
            get { return timeChunks; }
        }

        private DateTime ropeDrop;

        private DateTime parkClose;

        [SerializeField]
        private int timeChunkSize = 5;

        private int currentTimeIndex = 0;

        private List<TimelineChunk> timeChunks;

        // Start is called before the first frame update
        void Start()
        {
            ropeDrop = new DateTime(2023, 1, 21, 7, 0, 0);
            parkClose = new DateTime(2023, 1, 21, 9, 0, 0);
            timeChunks = new List<TimelineChunk>();

            TimeSpan dayLength = parkClose - ropeDrop;

            int numTimeChunks = (int)dayLength.TotalMinutes / timeChunkSize + 1;

            for (int i = 0; i < numTimeChunks; i++)
            {
                TimeSpan minutesAdd = new TimeSpan(0, i * timeChunkSize, 0);

                timeChunks.Add(new TimelineChunk(ropeDrop.Add(minutesAdd)));
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public TimelineChunk GetFutureTime(int numChunksForward)
        {
            if (currentTimeIndex + numChunksForward < timeChunks.Count - 1)
            {
                return timeChunks[currentTimeIndex + numChunksForward];
            }
            else
            {
                Debug.LogError("Trying to get future time past park close");

                return timeChunks[currentTimeIndex];
            }
        }

        public bool IsFutureTimePastParkClose(int numChunksForward)
        {
            if (currentTimeIndex + numChunksForward > timeChunks.Count - 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AdvanceTime(int numChunksForward)
        {
            if (currentTimeIndex + numChunksForward < timeChunks.Count - 1)
            {
                currentTimeIndex += numChunksForward;

                return true;
            }
            else
            {
                Debug.LogError("Trying to advance time past park close");

                return false;
            }
        }
    }
}
