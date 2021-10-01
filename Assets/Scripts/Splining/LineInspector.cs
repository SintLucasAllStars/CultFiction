using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Line))]
public class LineInspector : Editor
{

	private void OnSceneGUI()
	{
		Line line = target as Line; //FindObjectOfType<Line>(); (own method I found since I disliked their method at the time)

		//Makes the handles not rotate with the pivot rotation (local, not global)
        Quaternion handleRotation = Tools.pivotRotation == PivotRotation.Local ? line.transform.rotation : Quaternion.identity;

		//Makes the worldspace into local space so it goes with og pivot.
		Vector3 tp0 = line.transform.TransformPoint(line.p0);
		Vector3 tp1 = line.transform.TransformPoint(line.p1);

		//Draw the line and set position handles in editor
		Handles.color = Color.red;
        Handles.DrawLine(tp0, tp1);
		Handles.DoPositionHandle(tp0, handleRotation);
		Handles.DoPositionHandle(tp1, handleRotation);

		//Change on moving
		EditorGUI.BeginChangeCheck();
        tp0 = Handles.DoPositionHandle(tp0, handleRotation);
		tp1 = Handles.DoPositionHandle(tp1, handleRotation);
		if (EditorGUI.EndChangeCheck()) line.p0 = line.transform.InverseTransformPoint(tp0);
		if (EditorGUI.EndChangeCheck()) line.p1 = line.transform.InverseTransformPoint(tp1);
	}
}
