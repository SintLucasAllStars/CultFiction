using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public static PlayerController Instance;
	
	[SerializeField] private float _rotation;
	[SerializeField] private float _startRotation;
	[SerializeField] public float Power { get; private set; }

	[Range(1, 100)] public int PowerChangeModifier;

	public GameObject Ball;
	private Rigidbody _ballRB;
	
	public delegate void HitBall();
	public static event HitBall onHitBall;


	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;	
			DontDestroyOnLoad(this);
		}
		else
		{
			Destroy(this);
		}
	}

	private void Start()
	{
		_ballRB = Ball.GetComponent<Rigidbody>();
	}

	private void Update()
	{
		Power = (Mathf.Sin(Time.time / (PowerChangeModifier / 10f)) + 1) / 2;
	}

	public void ChangeRotation(float rot)
	{
		_rotation = (rot + _startRotation) % 360;
		transform.rotation = Quaternion.Euler(0, _rotation, 0);
	}

	public void SetPlayerToBall()
	{
		transform.position = Ball.transform.position;
	}

	public void LaunchBall()
	{
		if(onHitBall != null)
		onHitBall();
		
		if(_ballRB == null)
			_ballRB = Ball.GetComponent<Rigidbody>();
		
		_ballRB.velocity = Vector3.zero;
		Ball.transform.position = transform.position;
		_ballRB.AddForce((transform.right + (transform.up*Power)) * Power * 20, ForceMode.Impulse);
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
//			EditorGUILayout.LabelField("Rotation:");
//			_editorRotation = EditorGUILayout.Slider(_editorRotation, 0, 360);
//			controller.ChangeRotation(_editorRotation);


			if (GUILayout.Button("Launch"))
			{
				controller.LaunchBall();
			}
			
			EditorGUILayout.Space();


			if (GUILayout.Button("Set player to ball"))
			{
				controller.SetPlayerToBall();
			}
		}
	}
}
#endif