using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float moveSpeed = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetupBullet(Vector3 position, Quaternion rotation, float speed)
    {
        transform.SetPositionAndRotation(position, rotation);
        moveSpeed = speed;
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
