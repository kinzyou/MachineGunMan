using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartEndButton : MonoBehaviour
{
    public void StartButtonDown()
    {
        ChangeScene();
    }

    public void HowToPlay()
    {
        GoHowToPlayScene();
    }

    public void EndButtonDown()
    {
        Application.Quit();
    }
    void ChangeScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    void GoHowToPlayScene()
    {
        SceneManager.LoadScene("HowToPlayScene");
    }
}
