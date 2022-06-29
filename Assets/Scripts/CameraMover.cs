using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeenieWalker
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] Camera cam;
        [SerializeField] float minZoom = 2;
        [SerializeField] float maxZoom = 16;
        int maxHeight;
        int maxWidth;

        private void OnEnable()
        {
            MazeBuilderManager.OnCameraGridMaximums += GetMaxHeightWidth;
            MazeBuilderManager.OnCameraToMove += CameraMove;
        }

        private void OnDisable()
        {
            MazeBuilderManager.OnCameraGridMaximums -= GetMaxHeightWidth;
            MazeBuilderManager.OnCameraToMove -= CameraMove;
        }

        private void GetMaxHeightWidth(int height, int width)
        {
            maxHeight = height;
            maxWidth = width;
        }

        private void CameraMove(float height, float width)
        {
            Vector3 newLocation = Vector3.zero;
            newLocation.z = height / 2 - 0.5f; //subtract out the initial half to center the camera
            newLocation.x = width / 2 - 0.5f; //subtract out the initial half to center the camera
            newLocation.y = 10; //set this to a random value above the grid


            ///The math below for the percentages isn't quite right. I would need to adjust everything by 1 to get it to match properly
            float heightPercentage = height / maxHeight;
            float widthPercentage = width / maxWidth;
            float maxNeeded = Mathf.Max(heightPercentage, widthPercentage);

            float newZoom = maxNeeded * (maxZoom - minZoom) + minZoom;
            cam.orthographicSize = newZoom;
            transform.position = newLocation;

        }
    }
}
