using UnityEngine;

public class MidGameUiScript : MonoBehaviour
{
    public GameObject deathScreen;
    public GameObject pauseScreen;
    public bool isPaused = false;
    bool isDead = false;

    // Update is called once per frame
    void Update()
    {
        // set escape to be the button that 
        if (Input.GetKeyDown(KeyCode.Escape) && !isDead) { 
            isPaused = !isPaused;
            pauseScreen.SetActive(isPaused);
        }
    }

    public void Die() {
        deathScreen.SetActive(true);
        isDead = true;
    }
}
