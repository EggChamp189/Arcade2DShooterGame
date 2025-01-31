using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float health = 10;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(4f * Time.deltaTime * Vector3.right);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("PBullet"))
        {
            health -= collision.GetComponent<BulletScript>().damage;
            // add more damage effects here if wanted later
            FindFirstObjectByType<SoundManager>().PlaySound("LazerHit");
            Destroy(collision.gameObject);
        }
        CheckDead();
    }

    private void CheckDead() {
        if (health <= 0)
        {
            FindFirstObjectByType<SoundManager>().PlaySound("WeirdoAlienKilled");
            FindFirstObjectByType<PlayerScript>().UpdateScore(100);
            Destroy(gameObject);
        }
    }
}
