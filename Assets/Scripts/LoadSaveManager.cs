using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace WeenieWalker
{
    public class LoadSaveManager : MonoSingleton<LoadSaveManager>
    {

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }

        public void LoadGrid()
        {
            string destination = Application.persistentDataPath + "/save.dat";
            FileStream file;

            if (File.Exists(destination)) file = File.OpenRead(destination);
            else
            {
                Debug.LogError("File Not Found");
                return;
            }

            BinaryFormatter bf = new BinaryFormatter();
            LevelData data = (LevelData)bf.Deserialize(file);
            file.Close();

            MazeBuilderManager.Instance.LoadData(data);


        }

        public void SaveGrid(List<CellData> cellData)
        {
            Debug.Log(Application.persistentDataPath);

            string destination = Application.persistentDataPath + "/save.dat";
            FileStream file;

            if (File.Exists(destination)) file = File.OpenWrite(destination);
            else file = File.Create(destination);

            LevelData data = new LevelData(cellData);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, data);
            file.Close();
        }
    }
}
