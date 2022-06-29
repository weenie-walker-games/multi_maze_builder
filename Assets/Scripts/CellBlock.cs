using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeenieWalker
{
    public class CellBlock : MonoBehaviour
    {

        [SerializeField] GameObject selectedOption;
        Renderer selectedOptionRend;
        [SerializeField] List<GameObject> options = new List<GameObject>();
        List<Material> optionMats = new List<Material>();

        List<CellConnector> connections = new List<CellConnector>();

        private void OnEnable()
        {
            MazeBuilderManager.OnSelectOption += SelectOption;
        }

        private void OnDisable()
        {
            MazeBuilderManager.OnSelectOption -= SelectOption;
        }

        private void Start()
        {
            selectedOptionRend = selectedOption.GetComponent<Renderer>();

            for (int i = 0; i < options.Count; i++)
            {
                optionMats.Add(options[i].GetComponent<Renderer>().material);
            }

            SetOptionsActive(false, false);
        }

        private void SetOptionsActive(bool isSelectedActive, bool isOptionsActive)
        {
            selectedOption.SetActive(isSelectedActive);
            options.ForEach(g => g.SetActive(isOptionsActive));
        }

        private void SelectOption(int selection)
        {
            selectedOptionRend.material = optionMats[selection];
            SetOptionsActive(true, false);

        }
    }

    [System.Serializable]
    public struct CellConnector
    {
        public int optionNumber;
        public Vector3 startPointConnection;
        public Vector3 endPointConnection;

        public CellConnector(int option, Vector3 startPoint, Vector3 endPoint)
        {
            optionNumber = option;
            startPointConnection = startPoint;
            endPointConnection = endPoint;
        }
    }
}
