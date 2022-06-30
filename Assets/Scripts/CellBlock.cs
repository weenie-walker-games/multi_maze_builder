using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WeenieWalker
{
    public class CellBlock : MonoBehaviour, IPointerClickHandler
    {

        [SerializeField] GameObject selectedOption;
        Renderer selectedOptionRend;
        Material defaultSelectedOptionMat;
        [SerializeField] float selectedAlphaValue = 0.25f;
        [SerializeField] List<Renderer> options = new List<Renderer>();
        List<Material> optionMats = new List<Material>();
        int currentlySelectedOption;

        [SerializeField] List<CellData> cellData = new List<CellData>();

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
            defaultSelectedOptionMat = selectedOptionRend.material;

            for (int i = 0; i < options.Count; i++)
            {
                optionMats.Add(options[i].material);
            }

            SetOptionsActive(false, true);
        }

        private void SetOptionsActive(bool isSelectedActive, bool isOptionsActive)
        {
            selectedOption.SetActive(isSelectedActive);
            for (int i = 0; i < options.Count; i++)
            {
                bool toActivate = isOptionsActive && cellData[i].isActive;
                options[i].gameObject.SetActive(toActivate);
            }
        }

        private void SelectOption(int selection)
        {
            currentlySelectedOption = selection;

            if (selection == -1)
            {
                SetOptionsActive(false, true);
            }
            else
            {
                if (cellData[selection].isActive)
                {
                    selectedOptionRend.material = optionMats[selection];
                }
                else
                {
                    selectedOptionRend.material = defaultSelectedOptionMat;
                }

                SetOptionsActive(true, false);
            }
        }

        private void SetOptionColor(int option, Color newColor)
        {
            options[option].material.color = newColor;
            cellData[option].cellColor = newColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            
            //On left-click, set the cell active for this option
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                MazeBuilderManager.Instance.SelectedCell(this);
                selectedOptionRend.material = optionMats[currentlySelectedOption];
                cellData[currentlySelectedOption].isActive = true;
            }
            //On right click, set the cell inactive for this option
            else if(eventData.button == PointerEventData.InputButton.Right)
            {
                selectedOptionRend.material = defaultSelectedOptionMat;
                cellData[currentlySelectedOption].isActive = false;
            }

        }


    }
}
