using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int bulletLife;
    [SerializeField] private LayerMask walls;

    private float moveDistance;
    private Vector2 bulletVector;
    private Vector2 moveVector;
    private Rigidbody2D rb;
    

    public void bulletInit(Vector2 point)
    {
        bulletVector = point;
        Destroy(gameObject, bulletLife);
    }    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        moveVector = rb.position;
    }

    void FixedUpdate()
    {
        moveDistance = speed * Time.fixedDeltaTime;

        moveVector = rb.position + bulletVector * moveDistance;
        rb.MovePosition(moveVector);
    }

    //0.0625
}
