using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    Camera playerScreen;
    GameObject player;
    public GameObject enemyToSpawn;

    float spawnTimer = 10f;
    float spawnTime = 10.0f;
    public int numToSpawn = 3;

    private void Start()
    {
        playerScreen = FindAnyObjectByType<Camera>();
        player = FindAnyObjectByType<PlayerScript>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTime <= spawnTimer) {
            spawnTimer = 0;
            for (int i = 0; i < numToSpawn; i++) { SpawnEnemy(); }
            Debug.Log("Spawned Wave");
        }
    }

    void SpawnEnemy() {
        GameObject enemy = Instantiate(enemyToSpawn, this.transform);
        // randomly spawn the enemy outside the screen
        enemy.transform.position = RandomSpawnPosition();
        // rotate the enemy at the player
        Vector2 directionToMove = (Vector2)player.transform.position - (Vector2)enemy.transform.position;
        // rotate the enemy to face the player using euler method
        enemy.transform.rotation = Quaternion.Euler(0,0, Mathf.Atan2(directionToMove.y, directionToMove.x) * Mathf.Rad2Deg);
        Debug.Log("Spawned One Enemy");
    }

    Vector2 RandomSpawnPosition() {
        // get the screen size from the camera and get data from there
        // multiply the actual camera height by 1.2 so the enemy will spawn out of view
        float camHeight = playerScreen.orthographicSize * 1.2f;
        float camWidth = ((float)Screen.width / (float)Screen.height) * camHeight;
        // randomly decide which side of the camera to spawn from
        int spawnLoc = Random.Range(0, 4);
        switch (spawnLoc) {
            // made it so it bases the spawn locations off the camera in case I want to make the camera move in the future.
            case 0:
                // top
                return new Vector2(playerScreen.transform.position.x + Random.Range(-camWidth, camWidth), camHeight + playerScreen.transform.position.y);
            case 1:
                // bottom
                return new Vector2(playerScreen.transform.position.x + Random.Range(-camWidth, camWidth), -camHeight + playerScreen.transform.position.y);
            case 2:
                // left
                return new Vector2(camWidth + playerScreen.transform.position.x, playerScreen.transform.position.y + Random.Range(-camHeight, camHeight));
            default:
                // right
                return new Vector2(-camWidth + playerScreen.transform.position.x, playerScreen.transform.position.y + Random.Range(-camHeight, camHeight));

        }
    }
}
