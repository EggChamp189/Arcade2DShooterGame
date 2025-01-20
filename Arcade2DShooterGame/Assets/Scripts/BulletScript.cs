using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float moveSpeed = 0f;
    // hiding this value since changing it from the inspector will do nothing.
    [HideInInspector]
    public float damage = 0f;

    public void SetupBullet(Vector3 position, Quaternion rotation, float speed, float damage)
    {
        transform.SetPositionAndRotation(position, rotation);
        moveSpeed = speed;
        this.damage = damage;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveSpeed * Time.deltaTime * Vector3.up);
    }

    private void OnBecameInvisible()
    {
        Debug.Log("Is currently Invisible");
        Destroy(gameObject);
    }
}
