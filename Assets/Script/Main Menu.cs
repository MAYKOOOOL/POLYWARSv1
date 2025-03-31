using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject TutorialPanel;
    public GameObject OptionsPanel;
    public GameObject CreditsPanel;
    public GameObject[] tutorialPages;

    private int tutorialPageIndex = 0;

    private void Start()
    {
        OptionsPanel.SetActive(false);
        TutorialPanel.SetActive(false);
        CreditsPanel.SetActive(false);

        foreach (GameObject page in tutorialPages)
        {
            page.SetActive(false);
        }

        if (tutorialPages.Length > 0) tutorialPages[0].SetActive(true);
    }

    public void PlayGame()
    {
        PlayButtonClickSound();
/*
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayBGM(AudioManager.instance.BGMSounds);
        }
*/
        SceneManager.LoadScene("MAIN GAME");
    }

    public void OpenOptions()
    {
        PlayButtonClickSound();
        if (OptionsPanel != null)
        {
            OptionsPanel.SetActive(true);
        }
    }

    public void CloseOptions()
    {
        PlayButtonClickSound();
        if (OptionsPanel != null)
        {
            OptionsPanel.SetActive(false);
        }
    }

    public void OpenTutorial()
    {
        PlayButtonClickSound();
        if (TutorialPanel != null)
        {
            TutorialPanel.SetActive(true);
            tutorialPageIndex = 0;
            ShowTutorialPage(tutorialPageIndex);
        }
    }

    public void CloseTutorial()
    {
        PlayButtonClickSound();
        if (TutorialPanel != null)
        {
            TutorialPanel.SetActive(false);
        }
    }

    public void NextTutorialPage()
    {
        PlayButtonClickSound();
        if (tutorialPageIndex < tutorialPages.Length - 1)
        {
            tutorialPages[tutorialPageIndex].SetActive(false);
            tutorialPageIndex++;
            tutorialPages[tutorialPageIndex].SetActive(true);
        }
    }

    public void PreviousTutorialPage()
    {
        PlayButtonClickSound();
        if (tutorialPageIndex > 0)
        {
            tutorialPages[tutorialPageIndex].SetActive(false);
            tutorialPageIndex--;
            tutorialPages[tutorialPageIndex].SetActive(true);
        }
    }

    private void ShowTutorialPage(int index)
    {
        for (int i = 0; i < tutorialPages.Length; i++)
        {
            tutorialPages[i].SetActive(i == index);
        }
    }

    public void OpenCredits()
    {
        PlayButtonClickSound();
        if (CreditsPanel != null)
        {
            CreditsPanel.SetActive(true);
        }
    }

    public void CloseCredits()
    {
        PlayButtonClickSound();
        if (CreditsPanel != null)
        {
            CreditsPanel.SetActive(false);
        }
    }

    public void QuitGame()
    {
        PlayButtonClickSound();
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void PlayButtonClickSound()
    {
/*        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.buttonClickSound);
        }*/
    }
}
