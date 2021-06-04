using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Flock))]
public class CustomInspectorFlock : Editor
{
    public override void OnInspectorGUI()
    {
        
        Flock flock = (Flock)target;

        DrawDefaultInspector();
        
        if(GUILayout.Button("Reset settings"))
        {
            // Drivefactor: making it go slower/faster
            flock.ResetSettings();
            
        }

        // EditorGUILayout.Space();
        // EditorGUILayout.LabelField("Here begynder customization");
        


        // for (int i = 0; i < cb.behaviours.Length; i++)
        //     {

        //         EditorGUILayout.BeginHorizontal();

        //         cb.behaviours[i] = (FlockBehaviour)EditorGUILayout.ObjectField(cb.behaviours[i], typeof(FlockBehaviour), false);

        //         cb.weights[i] = EditorGUILayout.FloatField(cb.weights[i]);
        //         EditorGUILayout.EndHorizontal();

        //     }
        //     if (EditorGUI.EndChangeCheck())
        //     {
        //         EditorUtility.SetDirty(cb);
        //     }

    }
}