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
        private RectTransform attractionPanel;

        [SerializeField]
        private TextMeshProUGUI attractionPanelHeader;

        [SerializeField]
        private TextMeshProUGUI attractionPanelStandbyText;

        [SerializeField]
        private TextMeshProUGUI attractionPanelGatewayText;

        [SerializeField]
        private Button walkButton;

        private Attraction selectedAttraction;

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

        public void UpdateAttractionLabels()
        {
            attractionPanelHeader.text = selectedAttraction.name;

            selectedAttraction.UpdateStandbyWaitTime();
            selectedAttraction.UpdateGatewayAvailability();

            attractionPanelStandbyText.text = selectedAttraction.GetStandbyWaitTime();
            attractionPanelGatewayText.text = selectedAttraction.GetNextGatewayText();
        }

        public void OpenAttractionPanel(Attraction attraction)
        {
            if (attraction != gameManager.Pawn.CurrentLocation)
            {
                selectedAttraction = attraction;

                UpdateCurrentTime();
                UpdateAttractionLabels();

                attractionPanel.gameObject.SetActive(true);
                walkButton.gameObject.SetActive(true);
            }
            else
            {
                UpdateCurrentTime();
                UpdateAttractionLabels();

                attractionPanel.gameObject.SetActive(true);
                walkButton.gameObject.SetActive(false);
            }
        }

        public void CloseAttractionPanel()
        {
            attractionPanel.gameObject.SetActive(false);
        }

        public void AttractionPanelWalkButtonOnClick()
        {
            gameManager.Pawn.Move(selectedAttraction);

            UpdateCurrentTime();
            UpdateAttractionLabels();

            walkButton.gameObject.SetActive(false);
        }
    }
}