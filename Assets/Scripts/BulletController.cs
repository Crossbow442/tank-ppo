using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int bulletLife;

    private Vector2 bulletVector;
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
}
