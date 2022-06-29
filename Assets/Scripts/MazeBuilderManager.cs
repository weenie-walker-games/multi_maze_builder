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
        public int GridHeight { get { return gridHeight; } set { gridHeight = value; } }
        [Range(1, 30)]
        [SerializeField] int gridWidth = 5;
        public int GridWidth { get { return gridWidth; } set { gridWidth = value; } }

        [SerializeField] List<Color> optionColors = new List<Color>();

        int currentSelectedOption = -1;
        CellBlock currentCellClicked = null;

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

            for (int i = 0; i < optionColors.Count; i++)
            {
                OnColorChange?.Invoke(i, optionColors[i]);
            }
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

            for (int i = 0; i < optionColors.Count; i++)
            {
                OnColorChange?.Invoke(i, optionColors[i]);
            }
        }

        public void ClearGrid()
        {
            instantiatedGridPrefabs.ForEach(g => Object.DestroyImmediate(g));
        }

        public void SelectOption(int selection)
        {
            currentSelectedOption = selection == currentSelectedOption? -1 : selection;
         
            OnSelectOption?.Invoke(currentSelectedOption);
            ClearClickedCell();
        }

        public void SelectedCell(CellBlock clickedCell)
        {
            if (currentCellClicked == null)
            {
                currentCellClicked = clickedCell;
            }
            else
            {
                //Need to decide how to address what to do after a click
            }
        }

        public void ClearClickedCell()
        {
            if (currentCellClicked != null)
                currentCellClicked = null;

        }
    }
}
