using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Update()
    {
        // Set the cursor to be visible and unlocked
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
    }

    public void PlayGame ()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame ()
    {
        Debug.Log("Game closed down");
        Application.Quit();
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
