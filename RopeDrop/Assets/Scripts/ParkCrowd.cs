using RopeDropGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class ParkCrowd : MonoBehaviour
    {
        private CrowdLevel currentLevel;

        [SerializeField]
        private float morningPercent;

        [SerializeField]
        private float middayPercent;

        [SerializeField]
        private float afternoonPercent;

        [SerializeField]
        private float eveningPercent;

        // Update is called once per frame
        void Update()
        {

        }

        public void Intialize()
        {
            float totalPercent = morningPercent + middayPercent + afternoonPercent + eveningPercent;

            if (totalPercent != 1.0f)
            {
                Debug.LogError(string.Format("Crowd day percentages don't add up to 1: {0}, {1}, {2}, {3}", morningPercent, middayPercent, afternoonPercent, eveningPercent));
            }
        }

        public void SetDayCrowdLevels(GameManager gameManager)
        {
            int morningChunks = Mathf.RoundToInt(gameManager.Timeline.TimeChunks.Count * morningPercent);
            int middayChunks = Mathf.RoundToInt(gameManager.Timeline.TimeChunks.Count * middayPercent);
            int afternoonChunks = Mathf.RoundToInt(gameManager.Timeline.TimeChunks.Count * afternoonPercent);
            int eveningChunks = Mathf.RoundToInt(gameManager.Timeline.TimeChunks.Count * eveningPercent);

            for (int i = 0; i < gameManager.Timeline.TimeChunks.Count; i++)
            {
                if (i < morningChunks)
                {
                    gameManager.Timeline.TimeChunks[i].CrowdLevel = CrowdLevel.Light;
                }
                else if (i < morningChunks + middayChunks)
                {
                    gameManager.Timeline.TimeChunks[i].CrowdLevel = CrowdLevel.Heavy;
                }
                else if (i < morningChunks + middayChunks + afternoonChunks)
                {
                    gameManager.Timeline.TimeChunks[i].CrowdLevel = CrowdLevel.Medium;
                }
                else
                {
                    gameManager.Timeline.TimeChunks[i].CrowdLevel = CrowdLevel.Light;
                }

                // TODO: remove debug line
                //Debug.Log(string.Format("{0}: {1}", timeline.TimeChunks[i].ToString(), timeline.TimeChunks[i].CrowdLevel));
            }
        }
    }
}