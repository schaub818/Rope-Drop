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
        private RectTransform attractionPanel;

        [SerializeField]
        TextMeshProUGUI attractionPanelHeader;

        [SerializeField]
        TextMeshProUGUI attractionPanelStandby;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OpenAttractionPanel(Attraction attraction)
        {
            attractionPanelHeader.text = attraction.name;
            attractionPanelStandby.text = string.Format("Standby wait: {0}", attraction.GetStandbyWaitTime());

            attractionPanel.gameObject.SetActive(true);
        }

        public void CloseAttractionPanel()
        {
            attractionPanel.gameObject.SetActive(false);
        }
    }
}