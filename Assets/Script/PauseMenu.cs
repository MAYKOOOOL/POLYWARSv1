using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject[] otherUIElements;
    private bool isPaused = false;

    private void Start()
    {
        PausePanel.SetActive(false);
        SetUIElementsActive(true); // Ensure all UI elements are active at the start
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
            PauseGame();
        else
            ResumeGame();

        PlayButtonClickSound();
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        PausePanel.SetActive(true);
        SetUIElementsActive(false); // Hide other UI elements only when pausing
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
        SetUIElementsActive(true); // Show all UI elements when resuming

        PlayButtonClickSound();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    private void SetUIElementsActive(bool state)
    {
        foreach (GameObject uiElement in otherUIElements)
        {
            if (uiElement != null)
                uiElement.SetActive(state);
        }
    }

    private void PlayButtonClickSound()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.buttonClickSound);
        }
    }
}
