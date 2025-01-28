using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public Slider healthBar;
    public TMP_Text score;

    [Header("Player Stats")]
    public float maxHealth = 5f;
    public float health;
    public float bulletDamage = 5f;
    public float bulletSpeed = 7f;
    public int scoreNum = 0;

    [Header("Movement Variables")]
    public float speedChange = 6f;
    public float maxSpeed = 15f;
    Vector2 desiredDirection = Vector2.zero;
    float turnSpeed = 2.5f;
    public bool mouseTurningOn = false;

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
        TakeDamage(0);
        UpdateScore(0);
    }

    void Update()
    {
        lastFired += Time.deltaTime;
        // get the desired move direction
        desiredDirection = orientation.up * Input.GetAxisRaw("Vertical") + orientation.right * Input.GetAxisRaw("Horizontal");
        if (mouseTurningOn)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 directionToMove = (Vector2)mousePos - (Vector2)transform.position;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(directionToMove.y, directionToMove.x) * Mathf.Rad2Deg - 90);
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
        if (health <= 0) {
            FindFirstObjectByType<MidGameUiScript>().Die();
        }
    }

    public void UpdateScore(int num) {
        scoreNum += num;
        score.text = "Score: " + scoreNum;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy"))
        {
            TakeDamage(1f);
        }
    }
}
