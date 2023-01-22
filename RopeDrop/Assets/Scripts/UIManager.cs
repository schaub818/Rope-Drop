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

        public void OpenAttractionPanel(Attraction attraction)
        {
            UpdateCurrentTime();

            attractionPanelHeader.text = attraction.name;

            attraction.UpdateStandbyWaitTime();
            attraction.UpdateGatewayAvailability();

            attractionPanelStandbyText.text = attraction.GetStandbyWaitTime();
            attractionPanelGatewayText.text = attraction.GetNextGatewayText();

            attractionPanel.gameObject.SetActive(true);
        }

        public void CloseAttractionPanel()
        {
            attractionPanel.gameObject.SetActive(false);
        }
    }
}