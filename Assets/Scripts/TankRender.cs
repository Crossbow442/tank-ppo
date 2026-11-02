using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankRender : MonoBehaviour
{
    [SerializeField] Transform visualTransform; // drag the child sprite object here
    [SerializeField] float step = 10f;          // degrees per snap increment

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void LateUpdate()
    {
        float trueHeadingDegrees = rb.rotation; // Rigidbody2D.rotation is already in degrees
        float snapped = Mathf.Round(trueHeadingDegrees / step) * step;
        visualTransform.rotation = Quaternion.Euler(0f, 0f, snapped);
    }
}
