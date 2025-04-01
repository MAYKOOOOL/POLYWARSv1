using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionToGame : MonoBehaviour
{
    public float waitingTime = 16f;

    void Start()
    {
        StartCoroutine(WaitForIntro());
    }

    IEnumerator WaitForIntro()
    {
        yield return new WaitForSeconds(waitingTime);
        SceneManager.LoadScene("Main Menu");
    }
}
