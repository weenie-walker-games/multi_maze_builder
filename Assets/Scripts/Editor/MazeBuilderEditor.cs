using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace WeenieWalker
{
    [CustomEditor(typeof(MazeBuilderManager))]
    public class MazeBuilderEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            MazeBuilderManager myScript = (MazeBuilderManager)target;
            if(GUILayout.Button("Build Grid"))
            {
                myScript.BuildGrid();
            }
            if (GUILayout.Button("Clear Grid"))
            {
                myScript.ClearGrid();
            }
            if (GUILayout.Button("Set Option 0"))
            {
                myScript.SelectOption(0);
            }
            if (GUILayout.Button("Set Option 1"))
            {
                myScript.SelectOption(1);
            }


        }

    }
}
