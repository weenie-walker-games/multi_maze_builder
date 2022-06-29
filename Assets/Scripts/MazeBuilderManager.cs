using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeenieWalker
{
    public class MazeBuilderManager : MonoSingleton<MazeBuilderManager>
    {
        public static event System.Action<int, int> OnCameraGridMaximums;
        public static event System.Action<float, float> OnCameraToMove;
        public static event System.Action<int, Color> OnColorChange;
        public static event System.Action<int> OnSelectOption;


        [SerializeField] GameObject cellBlockPrefab;
        [SerializeField] Transform cellParent;
        List<GameObject> instantiatedGridPrefabs = new List<GameObject>();

        [Range(1, 30)]
        [SerializeField] int gridHeight = 5;
        [Range(1, 30)]
        [SerializeField] int gridWidth = 5;

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }

        private void Start()
        {
            //This is hard coded in based on the Range attribute above
            OnCameraGridMaximums?.Invoke(30, 30);
        }

        public void SetHeight(int height)
        {
            gridHeight = height;
        }

        public void SetWidth(int width)
        {
            gridWidth = width;
        }

        
        public void BuildGrid()
        {
            ClearGrid();

            Vector3 spawnLocation = Vector3.zero;

            for (int i = 0; i < gridWidth; i++)
            {
                for (int j = 0; j < gridHeight; j++)
                {
                    spawnLocation.x = i;
                    spawnLocation.z = j;

                    GameObject go = Instantiate(cellBlockPrefab, spawnLocation, Quaternion.identity, cellParent);
                    go.name = "Cell_" + i + "_" + j;
                    instantiatedGridPrefabs.Add(go);
                }
            }

            OnCameraGridMaximums?.Invoke(30, 30);
            OnCameraToMove?.Invoke(gridHeight, gridWidth);
        }

        public void ClearGrid()
        {
            instantiatedGridPrefabs.ForEach(g => Object.DestroyImmediate(g));
        }

        public void SelectOption(int selection)
        {
            OnSelectOption?.Invoke(selection);
        }
    }
}
