using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeenieWalker
{
    [System.Serializable]
    public class CellData
    {
        public float cellLocationX;
        public float cellLocationY;
        public List<CellOptionData> optionData = new List<CellOptionData>();


        public CellData (Vector2 location, List<CellOptionData> cellOptionData)
        {
            cellLocationX = location.x;
            cellLocationY = location.y;
            for (int i = 0; i < 4; i++)
            {
                optionData.Add(cellOptionData[i]);
            }
        }
    }
}
