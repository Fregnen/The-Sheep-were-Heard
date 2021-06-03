using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Flock))]
public class CustomInspectorFlock : Editor
{
    public override void OnInspectorGUI()
    {
        
        DrawDefaultInspector();

        Flock myScript = (Flock)target;
        if(GUILayout.Button("Reset settings"))
        {
            // Drivefactor: making it go slower/faster
            myScript.ResetSettings();
            
        }

    }
}