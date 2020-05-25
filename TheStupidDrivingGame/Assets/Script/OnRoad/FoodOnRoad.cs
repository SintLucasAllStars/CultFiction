using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodOnRoad : OnRoadBase
{
    FoodManager m_foodManager;
    Driving m_driving;

    [SerializeField] int index;

    [Header("hover properties")]
    public float rotateSpeed = 1;
    public float maxHeight = 0.1f;
    public float minHeight = -0.1f;
    public float hoverSpeed = 10.0f;

    protected float randomizeStartPoint;

    private void Update()
    {
        OnRoadMovement();
    }

    protected override void Start()
    {
        base.Start();

        randomizeStartPoint = Random.Range(0, 100);
        m_foodManager = FindObjectOfType<FoodManager>();
        m_driving = FindObjectOfType<Driving>();
    }

    protected override void OnCarHit()
    {
        m_foodManager.SpawnFood(index);
        Destroy(this.gameObject);
    }

    protected override void OnRoadMovement()
    {
        float hoverHeight = (maxHeight + minHeight) / 2.0f;
        float hoverRange = maxHeight - minHeight;
        float hoverY = hoverHeight + Mathf.Cos((randomizeStartPoint + Time.time) * hoverSpeed) * hoverRange;

        this.transform.position = Vector3.up + new Vector3(transform.position.x, hoverY, transform.position.z) + Vector3.back / sensitivity;
        transform.Rotate(Vector3.up* rotateSpeed, Space.World);

        if (transform.position.z < 1f)
            Destroy(this.gameObject);
    }
}

