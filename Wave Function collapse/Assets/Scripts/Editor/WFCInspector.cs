using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Test))]
public class WFCInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Test test = (Test) target;
        if (GUILayout.Button("Create tilemap"))
        {
            test.CreateWFC();
            test.CreateTileMap();
        }

        if (GUILayout.Button("Save tilemap"))
        {
            test.SaveTilemap();
        }
    }
}
