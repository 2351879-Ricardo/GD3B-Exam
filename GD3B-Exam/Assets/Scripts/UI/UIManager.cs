using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPnl;
    [SerializeField] private GameObject winScreenPnl;

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverPnl.SetActive(true);
    }

    public void RestartArena()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void WinScreen()
    {
        winScreenPnl.SetActive(true);
        FindObjectOfType<PlayerInput>().enabled = false;
        Time.timeScale = 0.01f;
    }
}
