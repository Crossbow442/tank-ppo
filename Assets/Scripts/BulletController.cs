using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int bulletLife;
    [SerializeField] private LayerMask hitMask;

    private float radius = 0.0625f;
    private float bulletOffset = 0.01f;
    private float moveDistance;
    private Vector2 bulletVector;
    private Vector2 moveVector;
    private RaycastHit2D rayHit;
    private Rigidbody2D rb;
    

    public void bulletInit(Vector2 point)
    {
        bulletVector = point;
        Destroy(gameObject, bulletLife);
    }    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        moveDistance = speed * Time.fixedDeltaTime;
        rayHit = Physics2D.CircleCast(rb.position, radius, bulletVector, moveDistance, hitMask);

        if (rayHit)
        {
            if (rayHit.collider.gameObject.layer == 6)
            {
                if (rayHit.distance == 0)
                {
                    Destroy(gameObject);
                }
                else
                {
                    rb.MovePosition(rayHit.centroid + rayHit.normal * bulletOffset);
                    bulletVector = Vector2.Reflect(bulletVector, rayHit.normal);
                }
            }
            else if (rayHit.collider.gameObject.layer == 3)
            {
                rayHit.collider.gameObject.GetComponent<TankController>().tankHit();

                moveVector = rb.position + bulletVector * moveDistance;
                rb.MovePosition(moveVector);
            }
        }
        else
        {
            moveVector = rb.position + bulletVector * moveDistance;
            rb.MovePosition(moveVector);
        }
    }
}
