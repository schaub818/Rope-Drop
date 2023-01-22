using System;

namespace RopeDropGame
{
    public class TimelineChunk
    {
        public TimelineChunk(DateTime time)
        {
            Time = time;
            CrowdLevel = CrowdLevel.Light;
        }

        public TimelineChunk(DateTime time, CrowdLevel crowdLevel)
        {
            Time = time;
            CrowdLevel = crowdLevel;
        }

        public DateTime Time
        {
            get;
            set;
        }

        public CrowdLevel CrowdLevel
        {
            get;
            set;
        }

        public override string ToString()
        {
            return Time.ToShortTimeString();
        }
    }
}