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
    public GameObject rhythmText;

    private static float rhythmCount;
    private float maxRhythmCount;
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
        rhythmCount = 0;
        maxRhythmCount = 0;
        DisappearRythmText();
    }
    void Update()
    {
        //节奏数随时间减少
        rhythmCount -= Time.deltaTime * 4;
        if (rhythmCount < 0)
        {
            rhythmCount = 0;
            DisappearCenterText();
        }
        //SetCenterText(rhythmCount.ToString());
        SetRhythmText(rhythmCount);

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
    public void ShowRhythmText()
    {
        rhythmText.transform.gameObject.SetActive(true);
    }
    public void SetRhythmText(float level)
    {
        if (level <= 0)
        {
            DisappearRythmText();
        }
        else if (level > 0 && level <= 10)
        {
            rhythmText.GetComponent<Text>().text = "D";
            ShowRhythmText();
        }
        else if (level > 10  && level <= 25)
        {
            rhythmText.GetComponent<Text>().text = "C";
            ShowRhythmText();
        }
        else if (level > 25 && level <= 45)
        {
            rhythmText.GetComponent<Text>().text = "B";
            ShowRhythmText();
        }
        else if (level > 45 && level <= 70)
        {
            rhythmText.GetComponent<Text>().text = "A";
            ShowRhythmText();
        }
        else if (level > 70 && level <= 100)
        {
            rhythmText.GetComponent<Text>().text = "S";
            ShowRhythmText();
        }
        else if (level > 100 && level <= 135)
        {
            rhythmText.GetComponent<Text>().text = "SS";
            ShowRhythmText();
        }
        else if (level > 135 && level <= 175)
        {
            rhythmText.GetComponent<Text>().text = "SSS";
            ShowRhythmText();
        }
        else
        {
            rhythmText.GetComponent<Text>().text = "Devil May Cry!!!";
            ShowRhythmText();
        }
    }
    public void DisappearRythmText()
    {
        rhythmText.transform.gameObject.SetActive(false);
    }
    static public void AddRythmCount(float count)
    {
        rhythmCount += count;
    }
    static public void SubtractRythmCount(float count)
    {
        rhythmCount -= count;
    }
    public void ShowDeathEnd()
    {
        Time.timeScale = 0;
        ShowEndGameFront();
        ShowReviveButton();
        DisappearNextLevelButton();
    }
    public void ShowWinEnd()
    {
        Time.timeScale = 0;
        ShowEndGameFront();
        ShowNextLevelButton();
        DisappearReviveButton();
    }
}
