using System.Collections;
using UnityEngine;
using UnityEditor;
using cam_manager;
using System;
using System.Runtime.CompilerServices;

[CustomEditor(typeof(camera_look)), CanEditMultipleObjects]
public class look_editor : Editor
{
    Vector3 newCameraPosition;
    Vector3 newTargetPosition;

    void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }


    protected virtual void OnSceneGUI(SceneView sceneView)
    {
        camera_look my_look = (camera_look)target;

        Handles.color = Color.blue;
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;
        style.fontSize = 40;

        EditorGUI.BeginChangeCheck();
        newCameraPosition = Handles.PositionHandle(my_look.camera_position, Quaternion.identity);
        Handles.Label(my_look.camera_position + Vector3.up * 2, "Camera", style);
        Handles.SphereHandleCap(0, newCameraPosition, Quaternion.identity, 1, EventType.Repaint);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(my_look, "Change Look At Target Position");
            my_look.camera_position = newCameraPosition;
            //dirty looks huehuehuehue
            EditorUtility.SetDirty(my_look);
        }

        EditorGUI.BeginChangeCheck();
        newTargetPosition = Handles.PositionHandle(my_look.camera_target, Quaternion.identity);
        Handles.Label(my_look.camera_target + Vector3.up * 2, "Target", style);
        Handles.SphereHandleCap(1, newTargetPosition, Quaternion.identity, 1, EventType.Repaint);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(my_look, "Change Look At Target Position");
            my_look.camera_target = newTargetPosition;
            EditorUtility.SetDirty(my_look);
        }

        Handles.DrawLine(newCameraPosition, newTargetPosition);
    }

    void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }
}