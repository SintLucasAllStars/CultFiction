using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(InteractTrigger))]
[CanEditMultipleObjects]
public class InteractTriggerEditor : Editor {
	
	SerializedProperty needsButtonPressProperty;
	SerializedProperty needItemProperty;
	SerializedProperty itemIdProperty;
	SerializedProperty whatToDoWithObjectProperty;
	SerializedProperty animationNameProperty;
	SerializedProperty animationNameSecondProperty;
	SerializedProperty objectsToChangeProperty;
	SerializedProperty textInteractProperty;
	SerializedProperty failTextProperty;

	void OnEnable () {
		needsButtonPressProperty = serializedObject.FindProperty ("needsButtonPress");
		needItemProperty = serializedObject.FindProperty ("needItem");
		itemIdProperty = serializedObject.FindProperty ("itemId");
		whatToDoWithObjectProperty = serializedObject.FindProperty ("whatToDoWithObject");
		animationNameProperty = serializedObject.FindProperty ("animationName");
		objectsToChangeProperty = serializedObject.FindProperty ("objectsToChange");
		animationNameSecondProperty = serializedObject.FindProperty ("animationNameSecond");
		textInteractProperty = serializedObject.FindProperty ("interactText");
		failTextProperty = serializedObject.FindProperty ("failText");
	}

	public override void OnInspectorGUI() {

		serializedObject.Update ();

		EditorGUILayout.PropertyField (needsButtonPressProperty);
		EditorGUILayout.PropertyField (needItemProperty);
		if (needItemProperty.boolValue)
		{
			EditorGUILayout.PropertyField (itemIdProperty);
			EditorGUILayout.PropertyField (failTextProperty);
			EditorGUILayout.PropertyField (textInteractProperty);
		}
		EditorGUILayout.PropertyField (whatToDoWithObjectProperty);
		if (whatToDoWithObjectProperty.intValue == 3)
		{
			EditorGUILayout.PropertyField (animationNameProperty);
		}
		if (whatToDoWithObjectProperty.intValue == 4)
		{
			EditorGUILayout.PropertyField (animationNameProperty);
			EditorGUILayout.PropertyField (animationNameSecondProperty);
		}

		EditorGUILayout.PropertyField (objectsToChangeProperty,true);
		serializedObject.ApplyModifiedProperties ();
	}

}
