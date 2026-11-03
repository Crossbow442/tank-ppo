using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//git-exo-checkid001

public class TankController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float revMultiplier;
    [SerializeField] private float turnRate;
    [SerializeField] private float muzzleOffset;
    [SerializeField] private float shotCooldown;
    [SerializeField] private bool useVelo;
    [SerializeField] private GameObject bulletPrefab;

    private float throttle;
    private float turn;
    private float tankRotation;
    private float timeSinceShot;
    private bool shot;
    private Vector2 lookVector;
    private Vector2 moveVector;
    private Vector2 bulletPos;
    private Rigidbody2D rb;

    public void tankHit()
    {
        Debug.Log("Tank Hit");
        Destroy(gameObject);
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tankRotation = rb.rotation;
        timeSinceShot = shotCooldown;
    }

    void Update()
    {
        turn = Input.GetAxisRaw("Horizontal");
        throttle = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            shot = true;
        }
    }

    void FixedUpdate()
    {
        tankRotation = tankRotation - turn * turnRate * Time.fixedDeltaTime;
        rb.MoveRotation(tankRotation);
        lookVector = new Vector2(Mathf.Cos(tankRotation * Mathf.Deg2Rad), Mathf.Sin(tankRotation * Mathf.Deg2Rad));

        if (throttle > 0)
        {
            moveVector = lookVector * speed * throttle * Time.fixedDeltaTime;
        }
        else
        {
            moveVector = lookVector * revMultiplier * speed * throttle * Time.fixedDeltaTime;
        }

        rb.MovePosition(rb.position + moveVector);

        if (shot)
        {
            if (timeSinceShot >= shotCooldown)
            {
                bulletPos = rb.position + lookVector * muzzleOffset;
                GameObject bullet = Instantiate(bulletPrefab, bulletPos, Quaternion.identity);
                bullet.GetComponent<BulletController>().bulletInit(lookVector);

                timeSinceShot = 0;
            }
            shot = false;
        }

        timeSinceShot = timeSinceShot + Time.fixedDeltaTime;
    }    
}
