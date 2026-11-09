using UnityEngine;

//git-exo-checkid001

public class TankController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float revMultiplier;
    [SerializeField] private float turnRate;
    [SerializeField] private float muzzleOffset;
    [SerializeField] private float shotCooldown;
    [SerializeField] private bool trainingMode;
    [SerializeField] private GameObject explosionParticles;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private LayerMask walls;

    private float throttle;
    private float turn;
    private float tankRotation;
    private float timeSinceShot;
    private float radius = 0.3f;
    private float wallOffset = 0.01f;
    private float moveDistance;
    private bool shot;
    private Vector2 lookVector;
    private Vector2 moveVector;
    private Vector2 bulletPos;
    private RaycastHit2D rayHit;
    private Rigidbody2D rb;

    public void tankHit()
    {
        Debug.Log("Tank Hit");
        if (!trainingMode)
        {
            Instantiate(explosionParticles, transform.position, Quaternion.identity);
        }
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
        moveDistance = speed * Time.fixedDeltaTime * throttle;

        rayHit = Physics2D.CircleCast(rb.position, radius, lookVector, moveDistance, walls);
        if (throttle > 0)
        {
            if (rayHit)
            {
                rb.MovePosition(rayHit.centroid + rayHit.normal * wallOffset);
            }
            else
            {
                moveVector = lookVector * moveDistance;
            }
        }
        else
        {
            if (rayHit)
            {
                moveVector = new Vector2(0f, 0f);
                rb.MovePosition(rayHit.centroid + rayHit.normal * wallOffset);
            }
            else
            {
                moveVector = lookVector * revMultiplier * moveDistance;
            }
        }

        if (!rayHit)
        {
            rb.MovePosition(rb.position + moveVector);
        }

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
