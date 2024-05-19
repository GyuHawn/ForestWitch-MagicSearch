using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ProgressBar))]
[CanEditMultipleObjects]
public class ProgressBarEditor : Editor{
    public override void OnInspectorGUI(){
        ProgressBar progressbar = (ProgressBar)target;

        GUILayout.BeginHorizontal();
            GUILayout.Label("Mask GameObject");
            progressbar.mask = (GameObject)EditorGUILayout.ObjectField(progressbar.mask, typeof(GameObject), true);
        GUILayout.EndHorizontal();

        GUILayout.Label("Mask width", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
            GUILayout.Label("min");
            progressbar.minWidth = EditorGUILayout.FloatField(progressbar.minWidth);
            GUILayout.Label("max");
            progressbar.maxWidth = EditorGUILayout.FloatField(progressbar.maxWidth);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
            GUILayout.Label("Progress");
            progressbar.progress = EditorGUILayout.Slider(progressbar.progress, 0, 100);
        GUILayout.EndHorizontal();
    }
}
