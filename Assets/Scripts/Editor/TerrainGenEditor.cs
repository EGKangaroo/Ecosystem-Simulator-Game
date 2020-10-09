using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;

[CustomEditor(typeof(TerrainGen))]
public class TerrainGenEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        TerrainGen gen = (TerrainGen)target;

        if (GUILayout.Button("Generate"))
        {
            gen.Generate();
        }
    }
}
