using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneController : MonoBehaviour
{
    public float roll=0, pitch=0, yaw=0, throttle=0;
    public float rollSpeed, pitchSpeed, yawSpeed, baseThrottleSpeed,  maxThrottleSpeed, throttleSpeedMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float r = Input.GetAxis("Roll");
        float p = Input.GetAxis("Pitch");
        float y = Input.GetAxis("Yaw");
        float t = Input.GetAxis("Throttle");

        roll = r * rollSpeed * Time.deltaTime;
        pitch = p * pitchSpeed * Time.deltaTime;
        yaw = y * yawSpeed * Time.deltaTime;
        throttle = Mathf.Clamp(throttle, baseThrottleSpeed, maxThrottleSpeed);

        // check if button pressed
        if (t != 0)
        {
            throttle += t * (throttleSpeedMultiplier * Time.deltaTime);
        }
        else
        {
            throttle -= (throttleSpeedMultiplier * Time.deltaTime);
        }

        transform.position += transform.forward * (throttle * throttleSpeedMultiplier * Time.deltaTime);
        transform.Rotate(pitch, yaw, -roll);
    }
}
