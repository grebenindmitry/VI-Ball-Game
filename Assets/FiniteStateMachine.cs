using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FiniteStateMachine : MonoBehaviour
{
     public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
