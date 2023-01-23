using RopeDropGame;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private GameManager gameManager;

        [SerializeField]
        private TextMeshProUGUI currentTimeText;

        [SerializeField]
        private TextMeshProUGUI scoreText;

        [SerializeField]
        private RectTransform attractionPanel;

        [SerializeField]
        private TextMeshProUGUI attractionPanelHeader;

        [SerializeField]
        private TextMeshProUGUI attractionPanelStandbyText;

        [SerializeField]
        private TextMeshProUGUI attractionPanelGatewayText;

        [SerializeField]
        private Button walkButton;

        [SerializeField]
        private Button standbyButton;

        [SerializeField]
        private Button bookGatewayButton;

        private Attraction selectedAttraction;
        private Attraction currentAttraction;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateCurrentTime()
        {
            currentTimeText.text = gameManager.Timeline.CurrentTime.ToShortTimeString();
        }

        public void UpdateScore()
        {
            scoreText.text = gameManager.ScoringSystem.CurrentScore.ToString();
        }

        public void UpdateAttractionLabels()
        {
            attractionPanelHeader.text = selectedAttraction.name;

            attractionPanelStandbyText.text = selectedAttraction.GetStandbyWaitTime();
            attractionPanelGatewayText.text = gameManager.MagicPass.GetGatewayBookingText(selectedAttraction);

            if (gameManager.MagicPass.IsGatewayAvailable(selectedAttraction))
            {
                bookGatewayButton.gameObject.SetActive(true);
            }
            else
            {
                bookGatewayButton.gameObject.SetActive(false);
            }
        }

        public void OpenAttractionPanel(Attraction attraction)
        {
            selectedAttraction = attraction;

            UpdateCurrentTime();
            UpdateAttractionLabels();

            if (attraction != gameManager.Pawn.CurrentLocation)
            {
                attractionPanel.gameObject.SetActive(true);
                walkButton.gameObject.SetActive(true);
                standbyButton.gameObject.SetActive(false);
            }
            else
            {
                attractionPanel.gameObject.SetActive(true);
                walkButton.gameObject.SetActive(false);
                standbyButton.gameObject.SetActive(true);
            }
        }

        public void CloseAttractionPanel()
        {
            attractionPanel.gameObject.SetActive(false);
        }

        public void AttractionPanelWalkButtonOnClick()
        {
            gameManager.Pawn.Move(selectedAttraction);
            currentAttraction = (Attraction)gameManager.Pawn.CurrentLocation;

            gameManager.Map.UpdateAllAttractions();

            UpdateCurrentTime();
            UpdateAttractionLabels();

            walkButton.gameObject.SetActive(false);
            standbyButton.gameObject.SetActive(true);
        }

        public void AttractionPanelStandbyButtonOnClick()
        {
            currentAttraction.RideStandby();

            gameManager.Map.UpdateAllAttractions();

            UpdateCurrentTime();
            UpdateScore();
            UpdateAttractionLabels();
        }

        public void AttractionPanelBookGatewayButtonOnClick()
        {
            if (!gameManager.MagicPass.BookGateway(selectedAttraction))
            {
                Debug.LogError("Failed to book Gateway");
            }

            UpdateAttractionLabels();
        }
    }
}