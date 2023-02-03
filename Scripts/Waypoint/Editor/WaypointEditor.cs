using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Waypoint))]
public class WaypointEditor : Editor
{
    Waypoint WaypointTarget => target as Waypoint;

    private void OnSceneGUI()
    {
        Handles.color = Color.red;
        if (WaypointTarget.Points == null)
        {
            return;
        }

        for (int i = 0; i < WaypointTarget.Points.Length; i++)
        {
            EditorGUI.BeginChangeCheck();
            Vector3 actualPoint = WaypointTarget.ActualPosition + WaypointTarget.Points[i];
            Vector3 newPoint = Handles.FreeMoveHandle(actualPoint, Quaternion.identity, 0.7f,
                new Vector3(0.3f, 0.3f, 0.3f), Handles.SphereHandleCap);
            //create text under point
            GUIStyle text = new GUIStyle();
            text.fontStyle = FontStyle.Bold;
            text.fontSize = 16;
            text.normal.textColor = Color.black;
            Vector3 alignment = Vector3.down * 0.3f + Vector3.right * 0.3f;
            Handles.Label(WaypointTarget.ActualPosition + WaypointTarget.Points[i] + alignment, $"{i + 1}", text);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target,"Free move handle");
                WaypointTarget.Points[i] = newPoint - WaypointTarget.ActualPosition;
            }
        }
    }
}