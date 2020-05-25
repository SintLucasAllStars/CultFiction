using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessItemHover : MonoBehaviour
{
    public Sprite inventoryicon;

    [Header("hover properties")]
    public float rotateSpeed = 1;
    public float maxHeight = 0.1f;
    public float minHeight = -0.1f;
    public float hoverSpeed = 10.0f;

    protected float randomizeStartPoint;
    protected Vector3 intialPos;

    protected virtual void Start()
    {
        intialPos = transform.position;
        randomizeStartPoint = Random.Range(0, 100);
    }

    private void Update()
    {
        HoverTransform();
    }

    /// <summary>
    /// put this in update and it wil hover the transform
    /// </summary>
    protected void HoverTransform()
    {
        float hoverHeight = (maxHeight + minHeight) / 2.0f;
        float hoverRange = maxHeight - minHeight;

        this.transform.position = Vector3.up + new Vector3(0, hoverHeight + Mathf.Cos((randomizeStartPoint + Time.time) * hoverSpeed) * hoverRange, 0) + intialPos;
        transform.Rotate(Vector3.up * rotateSpeed, Space.World);
    }
}
