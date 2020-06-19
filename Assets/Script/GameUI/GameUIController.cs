using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    GameManager gameManager;
    public GameObject centerText;
    public GameObject pauseButton;
    public GameObject nextLevelButton;
    public GameObject reviveButton;
    public GameObject pauseFront;
    public GameObject endGameFront;
    public GameObject enemiesMessageFront;

    private bool freeze = false;
    private bool endGame = false;

    void Start()
    {
        gameManager = GameManager.INSTANCE;
        gameManager.setUIController(this);
        centerText.transform.GetComponent<Text>().text = "Game Start!";
        Invoke("DisappearCenterText", 2f);
        enemiesMessageFront.transform.gameObject.SetActive(true);
        ShowPauseButton();
        ShowCenterText();
        DisappearPauseFront();
        DisappearEndGameFront();
    }
    void Update()
    {
        //显示在场敌人数
    }
    public void PauseButton()
    {
        if (endGame)
        {
            ShowPauseButton();
            return;
        }
        if (freeze)
        {
            Time.timeScale = 1f;
            freeze = false;
            DisappearPauseFront();
        }
        else
        {
            Time.timeScale = 0;
            freeze = true;
            ShowPauseFront();
        }
    }
    public void ResumeButton()
    {
        Time.timeScale = 1f;
        freeze = false;
        DisappearPauseFront();
    }
    public void RestartButton()
    {
        gameManager.LoadCurScene();
        Time.timeScale = 1f;
    }
    public void ExitButton()
    {
        gameManager.LoadGameSelectScene();
        Time.timeScale = 1f;
    }
    public void ReviveButton()
    {

    }
    public void NextLevelButton()
    {
        gameManager.LoadNextScene();
        Time.timeScale = 1f;
    }
    public void ShowPauseFront()
    {
        pauseFront.transform.gameObject.SetActive(true);
    }
    public void DisappearPauseFront()
    {
        pauseFront.transform.gameObject.SetActive(false);
    }
    public void ShowEndGameFront()
    {
        endGameFront.transform.gameObject.SetActive(true);
    }
    public void DisappearEndGameFront()
    {
        endGameFront.transform.gameObject.SetActive(false);
    }
    public void ShowCenterText()
    {
        centerText.transform.gameObject.SetActive(true);
    }
    public void SetCenterText(string message)
    {
        centerText.transform.GetComponent<Text>().text = message;
    }
    public void DisappearCenterText()
    {
        centerText.transform.gameObject.SetActive(false);
    }
    public void ShowPauseButton()
    {
        pauseButton.transform.gameObject.SetActive(true);
    }
    public void DisappearPauseButton()
    {
        pauseButton.transform.gameObject.SetActive(false);
    }
    public void ShowNextLevelButton()
    {
        nextLevelButton.transform.gameObject.SetActive(true);
    }
    public void DisappearNextLevelButton()
    {
        nextLevelButton.transform.gameObject.SetActive(false);
    }
    public void ShowReviveButton()
    {
        reviveButton.transform.gameObject.SetActive(true);
    }
    public void DisappearReviveButton()
    {
        reviveButton.transform.gameObject.SetActive(false);
    }
    public void ShowDeathEnd()
    {
        ShowEndGameFront();
        ShowReviveButton();
        DisappearNextLevelButton();
    }
    public void ShowWinEnd()
    {
        ShowEndGameFront();
        ShowNextLevelButton();
        DisappearReviveButton();
    }
}
