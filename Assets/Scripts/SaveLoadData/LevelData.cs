using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeenieWalker
{
    /// <summary>
    /// Adapted from https://answers.unity.com/questions/1300019/how-do-you-save-write-and-load-from-a-file.html
    /// </summary>
    [System.Serializable]
    public class LevelData
    {
        public List<CellData> cellData;

        public LevelData(List<CellData> data)
        {
            cellData = data;
        }


    }
}
