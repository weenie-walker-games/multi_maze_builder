using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WeenieWalker
{
    public class CellBlock : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {

        [SerializeField] GameObject selectedOption;
        Renderer selectedOptionRend;
        [SerializeField] float selectedAlphaValue = 0.25f;
        [SerializeField] List<Renderer> options = new List<Renderer>();
        List<Material> optionMats = new List<Material>();

        List<CellConnector> connections = new List<CellConnector>();

        private void OnEnable()
        {
            MazeBuilderManager.OnSelectOption += SelectOption;
            MazeBuilderManager.OnColorChange += SetOptionColor;
        }

        private void OnDisable()
        {
            MazeBuilderManager.OnSelectOption -= SelectOption;
            MazeBuilderManager.OnColorChange -= SetOptionColor;
        }

        private void Start()
        {
            selectedOptionRend = selectedOption.GetComponent<Renderer>();

            for (int i = 0; i < options.Count; i++)
            {
                optionMats.Add(options[i].material);
            }

            SetOptionsActive(false, true);
        }

        private void SetOptionsActive(bool isSelectedActive, bool isOptionsActive)
        {
            selectedOption.SetActive(isSelectedActive);
            options.ForEach(g => g.gameObject.SetActive(isOptionsActive));
        }

        private void SelectOption(int selection)
        {
            if (selection == -1)
            {
                SetOptionsActive(false, true);
            }
            else
            {
                selectedOptionRend.material = optionMats[selection];
                SetOptionsActive(true, false);
            }
        }

        private void SetOptionColor(int option, Color newColor)
        {
            options[option].material.color = newColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            MazeBuilderManager.Instance.SelectedCell(this);
            Color newColor = Color.white;
            newColor = selectedOptionRend.material.color;
            newColor.a = selectedAlphaValue;
            selectedOptionRend.material.color = newColor;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //throw new System.NotImplementedException();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            //throw new System.NotImplementedException();
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
