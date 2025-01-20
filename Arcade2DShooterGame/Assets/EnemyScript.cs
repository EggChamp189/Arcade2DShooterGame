using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float health = 10;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(5f * Time.deltaTime * Vector3.right);
    }
}
