using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField]
    //　ポーズした時に表示するUI
    private GameObject pauseUI;
    public GameObject[] Tips;

    public void StopGame()
    {
        pauseUI.SetActive(true);
        Tips[Random.Range(0, 10)].SetActive(true);
        Time.timeScale = 0f;
    }
    public void ReStartGame()
    {
        pauseUI.SetActive(false);
        Tips[0].SetActive(false);
        Tips[1].SetActive(false);
        Tips[2].SetActive(false);
        Tips[3].SetActive(false);
        Tips[4].SetActive(false);
        Tips[5].SetActive(false);
        Tips[6].SetActive(false);
        Tips[7].SetActive(false);
        Tips[8].SetActive(false);
        Tips[9].SetActive(false);
        Tips[10].SetActive(false);
        Time.timeScale = 1f;

    }
}
    
