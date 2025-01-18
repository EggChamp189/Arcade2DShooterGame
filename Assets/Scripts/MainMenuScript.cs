using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.WSA;

public class MainMenuScript : MonoBehaviour
{
    public void LoadScene(string scene) { SceneManager.LoadScene(scene); }
    public void QuitGame() { UnityEngine.Application.Quit(); }
}
