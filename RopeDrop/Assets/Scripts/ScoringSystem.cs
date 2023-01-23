using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class ScoringSystem : MonoBehaviour
    {
        public int CurrentScore
        {
            get { return currentScore; }
        }

        public string CurrentScoreText
        {
            get { return currentScore.ToString("D6"); }
        }

        private int currentScore = 0;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddScore(int pointsToAdd)
        {
            currentScore += pointsToAdd;
        }
    }
}