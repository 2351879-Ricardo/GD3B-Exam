using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPnl;

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverPnl.SetActive(true);
    }

    public void RestartArena()
    {
        SceneManager.LoadScene("Arena");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
