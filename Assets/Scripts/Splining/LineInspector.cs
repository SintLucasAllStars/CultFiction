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

		Handles.color = Color.red;
        Handles.DrawLine(tp0, tp1);
		Handles.DoPositionHandle(tp0, handleRotation);
		Handles.DoPositionHandle(tp1, handleRotation);
	}
}
