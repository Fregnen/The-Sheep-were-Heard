using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CompositeBehaviour))]
[CanEditMultipleObjects]

public class CompositeBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //setup
        CompositeBehaviour cb = (CompositeBehaviour)target;

        EditorGUILayout.Space();

        //check for behaviours
        if (cb.behaviours == null || cb.behaviours.Length == 0)
        {
            
            EditorGUILayout.HelpBox("No behaviours in array.", MessageType.Warning);

        }
        else
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Behaviours", GUILayout.MaxWidth(65));
            EditorGUILayout.LabelField(cb.behaviours.Length.ToString(), GUILayout.MaxWidth(50));

            EditorGUILayout.LabelField("Weights", GUILayout.MaxWidth(50));
            EditorGUILayout.LabelField(cb.weights.Length.ToString(), GUILayout.MaxWidth(50));
            EditorGUILayout.EndHorizontal();
            
            EditorGUI.BeginChangeCheck();

            for (int i = 0; i < cb.behaviours.Length; i++)
            {

                EditorGUILayout.BeginHorizontal();

                cb.behaviours[i] = (FlockBehaviour)EditorGUILayout.ObjectField(cb.behaviours[i], typeof(FlockBehaviour), false);

                cb.weights[i] = EditorGUILayout.FloatField(cb.weights[i]);
                EditorGUILayout.EndHorizontal();

            }
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(cb);
            }
        }

        if (GUILayout.Button("Add Behaviour"))
        {
            AddBehaviour(cb);
            EditorUtility.SetDirty(cb);
        }

        if (cb.behaviours != null && cb.behaviours.Length > 0)
        {
            if (GUILayout.Button("Remove Behaviour"))
            {
                RemoveBehaviour(cb);
                EditorUtility.SetDirty(cb);
            }
        }

    }

    #region Add or Remove behaviour
    void AddBehaviour(CompositeBehaviour cb)
    {
        int oldCount = (cb.behaviours != null) ? cb.behaviours.Length : 0;
        FlockBehaviour[] newBehaviours = new FlockBehaviour[oldCount + 1];
        float[] newWeights = new float[oldCount + 1];
        for (int i = 0; i < oldCount; i++)
        {
            newBehaviours[i] = cb.behaviours[i];
            newWeights[i] = cb.weights[i];
        }
        newWeights[oldCount] = 1f;
        cb.behaviours = newBehaviours;
        cb.weights = newWeights;
    }

    void RemoveBehaviour(CompositeBehaviour cb)
    {
        int oldCount = cb.behaviours.Length;
        if (oldCount == 1)
        {
            cb.behaviours = null;
            cb.weights = null;
            return;
        }
        FlockBehaviour[] newBehaviours = new FlockBehaviour[oldCount - 1];
        float[] newWeights = new float[oldCount - 1];
        for (int i = 0; i < oldCount - 1; i++)
        {
            newBehaviours[i] = cb.behaviours[i];
            newWeights[i] = cb.weights[i];
        }
        cb.behaviours = newBehaviours;
        cb.weights = newWeights;
    }
    #endregion

}