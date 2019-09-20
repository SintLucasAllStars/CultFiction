using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum carStates { Idle, Accelerate, Brake }
public class CarController : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float steeringAngle;
    private bool brake;
    private bool isColliding;
    private Transform lastCheckpoint;

    public LayerMask Road;
    public Vector3 centerMass;
    public Rigidbody rg;
    public int playerNum = 0;
    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;
    public float maxSteerAngle = 30;
    public float motorForce = 500;
    public float brakeForce = 500;
    public bool OnTrack;
    public GameObject checkpoints;

    public void GetInput()
    {
        if (playerNum == 0)
        {
            horizontalInput = Input.GetAxis("P1Horizontal");
            verticalInput = Input.GetAxis("P1Vertical");
            brake = Input.GetKey(KeyCode.Space);
        }
        if (playerNum == 1)
        {
            horizontalInput = Input.GetAxis("P2Horizontal");
            verticalInput = Input.GetAxis("P2Vertical");
            brake = Input.GetKey(KeyCode.K);
        }
    }
    public void Start()
    {
        lastCheckpoint = transform;
        rg.centerOfMass = centerMass;
        GameObject g = Instantiate(checkpoints);
        g.GetComponent<CheckpointManager>().playerNum = playerNum;
    }

    private void Steer()
    {
        steeringAngle = maxSteerAngle * horizontalInput;
        frontDriverW.steerAngle = steeringAngle;
        frontPassengerW.steerAngle = steeringAngle;
    }

    private void Accelerate()
    {
        rearDriverW.motorTorque = verticalInput * motorForce;
        rearPassengerW.motorTorque = verticalInput * motorForce ;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontDriverW, frontDriverT);
        UpdateWheelPose(frontPassengerW, frontPassengerT);
        UpdateWheelPose(rearDriverW, rearDriverT);
        UpdateWheelPose(rearPassengerW, rearPassengerT);
    }

    private void UpdateWheelPose(WheelCollider collider, Transform transform)
    {
        Vector3 pos = transform.position;
        Quaternion _quat = transform.rotation;

        collider.GetWorldPose(out pos, out _quat);

        transform.position = pos;
        transform.rotation = _quat;
    }

    private void FixedUpdate()
    {
         GetInput();
         Steer();
         Accelerate();
         UpdateWheelPoses();
    }

    private void Update()
    {
        Collider[] c = Physics.OverlapSphere(transform.position, 3, Road);
        OnTrack = c.Length > 0;
        rg.centerOfMass = centerMass;
        if (verticalInput > 0)
        {
            CarManager.instance.carState = carStates.Accelerate;
        }
        if (verticalInput < 0)
        {
            CarManager.instance.carState = carStates.Brake;
        }
        if (verticalInput == 0)
        {
            CarManager.instance.carState = carStates.Idle;
        }

        if (brake)
        {
            rearDriverW.brakeTorque = brakeForce;
            rearPassengerW.brakeTorque = brakeForce;
            CarManager.instance.carState = carStates.Brake;
        }
        else
        {
            rearDriverW.brakeTorque = 0;
            rearPassengerW.brakeTorque = 0;
        }
        isColliding = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (isColliding) return;
        isColliding = true;

        if (other.gameObject.CompareTag("CheckPoint"))
        {
            CheckpointManager checkpointM = other.gameObject.GetComponentInParent<CheckpointManager>();
            if (playerNum == checkpointM.playerNum)
            {
                lastCheckpoint = other.gameObject.transform;
                checkpointM.NextCheckPoint(other.gameObject);
            }
        }

    }

}
