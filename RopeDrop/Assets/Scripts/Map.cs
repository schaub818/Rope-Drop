using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RopeDropGame
{
    public class Map : MonoBehaviour
    {
        public List<MapLocation> Locations
        {
            get { return locations; }
        }

        public List<Path> Paths
        {
            get { return paths; }
        }

        [SerializeField]
        private GameManager gameManager;

        [SerializeField]
        private List<MapLocation> locations;

        [SerializeField]
        private List<Path> paths;

        [SerializeField]
        private Camera gameCamera;

        [SerializeField]
        private float zoomStep;

        [SerializeField]
        private float minimumCameraSize;

        [SerializeField]
        private float maximumCameraSize;

        private Vector3 dragOrigin;

        // Update is called once per frame
        void Update()
        {
            PanCamera();
            ZoomCamera();
        }

        public void Initialize()
        {
            foreach (MapLocation location in locations)
            {
                if (location is Attraction)
                {
                    Attraction attraction = (Attraction)location;

                    attraction.Initialize();
                }
            }
        }

        public void UpdateAllAttractions()
        {
            foreach (MapLocation location in locations)
            {
                if (location is Attraction)
                {
                    Attraction attraction = (Attraction)location;

                    attraction.UpdateStandbyWaitTime();
                    attraction.UpdateGatewayAvailability();
                }
            }
        }

        private void PanCamera()
        {
            if (gameManager.UIManager.UIActive)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    dragOrigin = gameCamera.ScreenToWorldPoint(Input.mousePosition);


                }

                if (Input.GetMouseButton(1))
                {
                    Vector3 difference = dragOrigin - gameCamera.ScreenToWorldPoint(Input.mousePosition);

                    gameCamera.transform.position += difference;
                }
            }
        }

        private void ZoomCamera()
        {
            if (gameManager.UIManager.UIActive)
            {
                float newSize = 0.0f;

                if (Input.mouseScrollDelta.y != 0.0f)
                {
                    newSize = gameCamera.orthographicSize - (zoomStep * Input.mouseScrollDelta.y);

                    gameCamera.orthographicSize = Mathf.Clamp(newSize, minimumCameraSize, maximumCameraSize);
                }
            }
        }
    }
}