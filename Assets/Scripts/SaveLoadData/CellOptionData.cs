using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeenieWalker
{
    [System.Serializable]
    public class CellOptionData
    {
        public Renderer optionRenderer;
        public bool isActive;
        public Color cellColor;

        public CellOptionData(Renderer renderer, bool active, Color color)
        {
            optionRenderer = renderer;
            isActive = active;
            cellColor = color;
        }
    }
}
