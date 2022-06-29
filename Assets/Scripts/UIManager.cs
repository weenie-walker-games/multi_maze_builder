using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace WeenieWalker
{
    public class UIManager : MonoSingleton<UIManager>
    {
        [SerializeField] List<Image> optionButtons = new List<Image>();
        [SerializeField] TMP_Text widthText;
        [SerializeField] Slider widthSlider;
        [SerializeField] TMP_Text heightText;
        [SerializeField] Slider heightSlider;

        private void OnEnable()
        {
            MazeBuilderManager.OnColorChange += SetOptionColor;

        }

        private void OnDisable()
        {
            MazeBuilderManager.OnColorChange -= SetOptionColor;

        }

        private void Start()
        {
            heightSlider.value = MazeBuilderManager.Instance.GridHeight;
            widthSlider.value = MazeBuilderManager.Instance.GridWidth;

            ChangeHeight();
            ChangeWidth();
        }

        private void SetOptionColor(int option, Color color)
        {
            optionButtons[option].color = color;
        }

        public void ChangeHeight()
        {
            heightText.text = "Height: " + heightSlider.value;
            MazeBuilderManager.Instance.GridHeight = (int)heightSlider.value;
        }

        public void ChangeWidth()
        {
            widthText.text = "Width: " + widthSlider.value;
            MazeBuilderManager.Instance.GridWidth = (int)widthSlider.value;
        }
    }
}
