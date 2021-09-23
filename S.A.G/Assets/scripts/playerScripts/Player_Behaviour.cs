using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Behaviour : MonoBehaviour
{
    [Header("controls")]
    public KeyCode powerbutton;
    public string axisName;
    [Header("animation pivot")]
    public Transform pivot;
    [Space(10)]

    [Header("ball Spawning")]
    public GameObject ballPrefab;
    public float resetTime;
    private Rigidbody ballRB;

    [Header("player calculations")]
    [Space(10)]
    private float angle = 0;
    private float power = 0;
    public float maxAngle, minAngle;
    public float maxPower;
    public float powerIncrease;
    public float angleIncrease;

    [Header("animation control")]
    [Space(10)]
    public float swingApex;
    public float swingSpeed, resetSwingSpeed;
    private bool shooting;

    private Quaternion startRotation;
    private Quaternion targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        SpawnBall();
        startRotation = pivot.localRotation;
        targetRotation = CopyQuaternion(startRotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (!shooting)
        {
            if (Input.GetKey(powerbutton))
            {
                power += powerIncrease * Time.deltaTime;
                power = Mathf.Min(power, maxPower);
            }
            else
            {
                angle += Input.GetAxis(axisName) * angleIncrease * Time.deltaTime;
                angle = Mathf.Clamp(angle, minAngle, maxAngle);

                if (Input.GetKeyUp(powerbutton))
                {
                    Shoot();
                }
            }
        }

        Animate();
    }

    #region shooting
    public void Shoot()
    {
        shooting = true;
        CalculateSwingAngle(-power);
    }

    public void FinishShot()
    {
        shooting = false;
        power = 0;
        angle = 0;
        StartCoroutine(ResetTimer());
    }
    #endregion

    #region reset and ball spawning
    private IEnumerator ResetTimer()
    {
        yield return new WaitForSeconds(resetTime);
        SpawnBall();
    }

    private void SpawnBall()
    {
        GameObject ball = Instantiate(ballPrefab, new Vector3(transform.position.x + 1.8f, transform.position.y - 0.55f, transform.position.z + .25f), Quaternion.identity);
        ballRB =  ball.GetComponent<Rigidbody>();
    }
    #endregion

    #region animation
    public void Animate()
    {
        float speed = 0;
        if (!shooting)
        {
            CalculateSwingAngle(power);
            speed = resetSwingSpeed;
        }
        else
        {
            speed = swingSpeed;
        }

        float angleDiff = Quaternion.Angle(pivot.localRotation, targetRotation);
        pivot.localRotation = Quaternion.Slerp(pivot.localRotation, targetRotation, angleDiff * (speed * 0.0001f));

        if (shooting && angleDiff < 10f)
        {
            FinishShot();
        }
    }
    #endregion

    #region untils
    private void CalculateSwingAngle(float percentage)
    {
        float a = percentage * swingApex;
        targetRotation = CopyQuaternion(startRotation);
        targetRotation *= Quaternion.AngleAxis(a, -Vector3.right);
    }

    private Quaternion CopyQuaternion(Quaternion q)
    {
        return new Quaternion(q.x, q.y, q.z, q.w);
    }
    #endregion
}
