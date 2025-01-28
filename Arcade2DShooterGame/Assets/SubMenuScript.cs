using UnityEngine;
using UnityEngine.SceneManagement;

public class SubMenuScript : MonoBehaviour
{
    PlayerScript player;
    MidGameUiScript gameScript;
    private void OnEnable()
    {
        Time.timeScale = 0;
        gameScript = FindAnyObjectByType<MidGameUiScript>();
        player = FindAnyObjectByType<PlayerScript>();
        player.mouseTurningOn = false;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        player.mouseTurningOn = true;
    }

    public void OnResume()
    {
        gameScript.isPaused = false;
        gameObject.SetActive(false);
    }

    public void OnQuit() {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnRedo()
    {
        SceneManager.LoadScene("Game");
    }
}
