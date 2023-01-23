using RopeDropGame;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class UIManager : MonoBehaviour
    {
        public bool UIActive
        {
            get { return uiActive; }
        }

        [SerializeField]
        private GameManager gameManager;

        [SerializeField]
        private RectTransform appPanel;

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

        [SerializeField]
        private Button rideGatewayButton;

        [SerializeField]
        private RectTransform parkClosedPanel;

        [SerializeField]
        private TextMeshProUGUI finalScoreText;

        private Attraction selectedAttraction;
        private Attraction currentAttraction;

        private bool uiActive;

        // Use this for initialization
        void Start()
        {
            uiActive = true;
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
            scoreText.text = gameManager.ScoringSystem.CurrentScoreText;
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

            if (selectedAttraction == gameManager.Pawn.CurrentLocation &&
                gameManager.MagicPass.IsGatewayBooked(selectedAttraction) &&
                !gameManager.MagicPass.IsGatewayUsed(selectedAttraction) &&
                gameManager.MagicPass.GetGatewayBooking(selectedAttraction) <= gameManager.Timeline.CurrentTimeChunk)
            {
                rideGatewayButton.gameObject.SetActive(true);
            }
            else
            {
                rideGatewayButton.gameObject.SetActive(false);
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

        public void OpenParkClosedPanel()
        {
            finalScoreText.text = string.Format("You had {0} points of fun today!", gameManager.ScoringSystem.CurrentScoreText);

            parkClosedPanel.gameObject.SetActive(true);
        }

        public void CloseParkClosedPanel()
        {
            parkClosedPanel.gameObject.SetActive(false);
        }

        public void CloseApp()
        {
            appPanel.gameObject.SetActive(false);

            uiActive = false;
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

        public void AttractionPanelRideGatewayButtonOnClick()
        {
            gameManager.MagicPass.UseGateway(currentAttraction);

            gameManager.Map.UpdateAllAttractions();

            UpdateCurrentTime();
            UpdateScore();
            UpdateAttractionLabels();
        }

        public void ParkClosedPanelRestartGameButtonOnClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}