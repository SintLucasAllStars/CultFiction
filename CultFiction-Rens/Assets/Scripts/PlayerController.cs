using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float _rotation;
	[SerializeField] private float _startRotation;
	[SerializeField] private float _power;

	[Range(1, 100)] public int PowerChangeModifier;

	public GameObject _ball;
	private Rigidbody _ballRB;

	private void Start()
	{
		_ballRB = _ball.GetComponent<Rigidbody>();
	}

	private void Update()
	{
		_power = (Mathf.Sin(Time.time / (PowerChangeModifier / 10f)) + 1) / 2;
	}

	public void ChangeRotation(float rot)
	{
		_rotation = (rot + _startRotation) % 360;
		transform.rotation = Quaternion.Euler(0, _rotation, 0);
	}

	public void LaunchBall()
	{
		_ballRB.velocity = Vector3.zero;
		_ball.transform.position = transform.forward;
		_ballRB.AddForce((transform.forward + (transform.up*_power)) * _power * 20, ForceMode.Impulse);
	}


#if UNITY_EDITOR
	[CustomEditor(typeof(PlayerController))]
	public class PlayerEditor : Editor
	{
		private float _editorRotation = 0;

		public override void OnInspectorGUI()
		{
			PlayerController controller = (PlayerController) target;

			base.OnInspectorGUI();
			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Rotation:");
			_editorRotation = EditorGUILayout.Slider(_editorRotation, 0, 360);
			controller.ChangeRotation(_editorRotation);


			if (GUILayout.Button("Launch"))
			{
				controller.LaunchBall();
			}
		}
	}
}
#endif