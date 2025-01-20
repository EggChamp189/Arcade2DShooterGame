using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public Slider healthBar;

    [Header("Player Stats")]
    public float maxHealth = 5f;
    public float health;
    // shouldn't actually affect the bullets yet
    public float bulletDamage = 5f;
    public float bulletSpeed = 7f;

    [Header("Movement Variables")]
    public float speedChange = 6f;
    public float maxSpeed = 15f;
    Vector2 desiredDirection = Vector2.zero;
    float turnSpeed = 2.5f;

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
        health = maxHealth;
        healthBar.value = health / maxHealth;
    }

    void Update()
    {
        lastFired += Time.deltaTime;
        // get the desired move direction
        desiredDirection = orientation.up * Input.GetAxisRaw("Vertical") + orientation.right * Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.Q)) {
            transform.Rotate(Vector3.forward, turnSpeed);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.forward, -turnSpeed);
        }

        if (lastFired >= fireRate) {
            lastFired = 0;
            FindFirstObjectByType<SoundManager>().PlaySound("LazerFired");
            foreach (GameObject pos in firingPositions) {
                BulletScript curBullet = Instantiate(bullet).GetComponent<BulletScript>();
                curBullet.SetupBullet(pos.transform.position, transform.rotation, bulletSpeed, bulletDamage);
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

    public void TakeDamage(float damage) {
        health -= damage;
        healthBar.value = health / maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy"))
        {
            TakeDamage(1f);
        }
    }
}
