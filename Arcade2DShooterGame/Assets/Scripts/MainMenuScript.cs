using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void LoadScene(string scene) { SceneManager.LoadScene(scene); }
    public void QuitGame() { Application.Quit(); }
}
