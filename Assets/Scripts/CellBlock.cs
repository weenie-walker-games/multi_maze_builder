using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WeenieWalker
{
    public class CellBlock : MonoBehaviour, IPointerClickHandler
    {

        [HideInInspector] public CellData cellData;
        [SerializeField] Renderer selectedOptionRend;
        Color defaultSelectedOptionColor;
        [SerializeField] List<Renderer> options = new List<Renderer>();
        int currentlySelectedOption = -1;
        public Vector2 CellLocation { get; set; }

        public List<CellOptionData> cellOptionData = new List<CellOptionData>();

        private void OnEnable()
        {
            MazeBuilderManager.OnSelectOption += SelectOption;
            MazeBuilderManager.OnColorChange += SetOptionColor;

            cellOptionData.Clear();

            for (int i = 0; i < options.Count; i++)
            {
                cellOptionData.Add(new CellOptionData(options[i], false, Color.black));
            }
        }

        private void OnDisable()
        {
            MazeBuilderManager.OnSelectOption -= SelectOption;
            MazeBuilderManager.OnColorChange -= SetOptionColor;
        }

        private void Start()
        {
            defaultSelectedOptionColor = selectedOptionRend.material.color;
            SetOptionsActive(false, true);
        }

        private void SetOptionsActive(bool isSelectedActive, bool isOptionsActive)
        {
            selectedOptionRend.gameObject.SetActive(isSelectedActive);
            for (int i = 0; i < options.Count; i++)
            {
                bool toActivate = isOptionsActive && cellOptionData[i].isActive;
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
                if (cellOptionData[selection].isActive)
                {
                    selectedOptionRend.material.color = cellOptionData[selection].cellColor;
                }
                else
                {
                    selectedOptionRend.material.color = defaultSelectedOptionColor;
                }

                SetOptionsActive(true, false);
            }
        }

        private void SetOptionColor(int option, Color newColor)
        {
            options[option].material.color = newColor;
            cellOptionData[option].cellColor = newColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //Don't do anything if an option isn't active
            if (currentlySelectedOption == -1)
                return;
            
            //On left-click, set the cell active for this option
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                MazeBuilderManager.Instance.SelectedCell(this);
                selectedOptionRend.material.color = cellOptionData[currentlySelectedOption].cellColor;
                cellOptionData[currentlySelectedOption].isActive = true;
            }
            //On right click, set the cell inactive for this option
            else if(eventData.button == PointerEventData.InputButton.Right)
            {
                selectedOptionRend.material.color = defaultSelectedOptionColor;
                cellOptionData[currentlySelectedOption].isActive = false;
            }

        }


    }
}
