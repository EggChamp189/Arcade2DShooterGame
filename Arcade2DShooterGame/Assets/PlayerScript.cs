using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Movement Variables")]
    public float speedChange = 6f;
    public float maxSpeed = 15f;
    Vector2 desiredDirection = Vector2.zero;

    [Header("Bullet Variables")]
    public float fireRate = 1.0f;
    public float lastFired = 0;

    [Header("Refrences and Dependencies")]
    public List<GameObject> firingPositions;
    public GameObject bullet;
    public Transform orientation;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        lastFired += Time.deltaTime;
        // get the desired move direction
        desiredDirection = orientation.up * Input.GetAxisRaw("Vertical") + orientation.right * Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.Q)) {
            transform.Rotate(Vector3.forward, 1.5f);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.forward, -1.5f);
        }

        if (lastFired >= fireRate) {
            lastFired = 0;
            foreach (GameObject pos in firingPositions) {
                BulletScript curBullet = Instantiate(bullet).GetComponent<BulletScript>();
                curBullet.SetupBullet(pos.transform.position, transform.rotation, 5f);
            }
        }
    }

    private void FixedUpdate()
    {
        // move when desired direction is not zero
        if (desiredDirection.magnitude > 0 || desiredDirection.magnitude < 0)
        {
            rb.AddForce(desiredDirection * speedChange);
            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }
        // else, rapidly lower the speed
        else {
            rb.linearVelocity = rb.linearVelocity * 0.95f;
        }
    }
}
