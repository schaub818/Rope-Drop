using System;

namespace RopeDropGame
{
    public struct TimelineChunk
    {
        public TimelineChunk(DateTime time)
        {
            Time = time;
        }

        public DateTime Time
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