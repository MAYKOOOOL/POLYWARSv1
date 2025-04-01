using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public HealthManager playerHealthManager;
    public BossHealth bossHealthManager;

    public GameObject winPanel;
    public GameObject losePanel;

    public GameObject[] otherUIElements;

    void Start()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        Time.timeScale = 1;
    }

    void Update()
    {
        if (playerHealthManager.GetCurrentHealth() <= 0)
        {
            ShowLoseScreen();
        }

        if (bossHealthManager.GetCurrentHealth() <= 0)
        {
            ShowWinScreen();
        }
    }

    void ShowWinScreen()
    {
        ShowEndGameScreen(true);
        Time.timeScale = 0;
    }

    void ShowLoseScreen()
    {
        ShowEndGameScreen(false);
        Time.timeScale = 0;
    }

    private void ShowEndGameScreen(bool isWin)
    {
        winPanel.SetActive(isWin);
        losePanel.SetActive(!isWin);

        foreach (GameObject uiElement in otherUIElements)
        {
            uiElement.SetActive(false);
        }
    }

    void PlayAgain()
    {
        Time.timeScale = 1;
        playerHealthManager.Heal(playerHealthManager.maxHealth); 
        bossHealthManager.Heal(bossHealthManager.maxHealth);

        winPanel.SetActive(false);
        losePanel.SetActive(false);

        foreach (GameObject uiElement in otherUIElements)
        {
            uiElement.SetActive(true);
        }
    }

    void GoToMainMenu()
    { 
        SceneManager.LoadScene("Main Menu");
    }
}
