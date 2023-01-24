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

        private SpriteRenderer mapRenderer;

        private Vector3 dragOrigin;

        private float mapMinX;
        private float mapMinY;
        private float mapMaxX;
        private float mapMaxY;

        // Update is called once per frame
        void Update()
        {
            PanCamera();
            ZoomCamera();
        }

        private void Awake()
        {
            mapRenderer = GetComponent<SpriteRenderer>();

            mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2.0f;
            mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2.0f;

            mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2.0f;
            mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2.0f;
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

        public void ClampCameraClosedApp()
        {
            mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2.0f;

            gameCamera.orthographicSize = Mathf.Clamp(gameCamera.orthographicSize, 0.0f, 5.0f);

            float camHeight = gameCamera.orthographicSize;
            float camWidth = gameCamera.orthographicSize * gameCamera.aspect;

            float minX = mapMinX + camWidth;
            float maxX = mapMaxX - camWidth;
            float minY = mapMinY + camHeight;
            float maxY = mapMaxY - camHeight;

            float newX = Mathf.Clamp(gameCamera.transform.position.x, minX, maxX);
            float newY = Mathf.Clamp(gameCamera.transform.position.y, minY, maxY);

            gameCamera.transform.position = new Vector3(newX, newY, gameCamera.transform.position.z);
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

                    gameCamera.transform.position = ClampCamera(gameCamera.transform.position + difference);
                }
            }
        }

        private Vector3 ClampCamera(Vector3 targetPosition)
        {
            float camHeight = gameCamera.orthographicSize;
            float camWidth = gameCamera.orthographicSize * gameCamera.aspect;

            mapMaxX = (mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2.0f) * ((camHeight / 10.0f) + 1);

            float minX = mapMinX + camWidth;
            float maxX = mapMaxX - camWidth;
            float minY = mapMinY + camHeight;
            float maxY = mapMaxY - camHeight;

            float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
            float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

            return new Vector3(newX, newY, targetPosition.z);
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

                    gameCamera.transform.position = ClampCamera(gameCamera.transform.position);
                }
            }
        }
    }
}