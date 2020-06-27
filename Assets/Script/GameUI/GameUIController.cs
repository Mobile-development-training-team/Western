using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIController : MonoBehaviour
{
    GameManager gameManager;
    private BraveController brave;

    public GameObject centerText;

    public GameObject enemiesMessageFront;
    public GameObject currentEnemyWavesText;
    public GameObject currentEnemyNumText;

    public GameObject braveAPText;
    public GameObject skillNumText;

    public GameObject pauseButton;
    public GameObject pauseFront;
    public GameObject braveAtkText;
    public GameObject braveHPText;
    public GameObject braveDefText;
    public GameObject[] buffs;
    //private static float braveMaxHP;
    //private static float braveHP;

    public GameObject endGameFront;
    public GameObject endGameMessageText;

    public GameObject theGetBuffFront;
    public GameObject theBuff;

    //private static float braveAP;
    private bool freeze = false;
    private bool endGame = false;


    //public GameObject nextLevelButton;
    //public GameObject reviveButton;
    //public GameObject rhythmText;
    //private static float rhythmCount;
    //private float maxRhythmCount;

    void Awake()
    {
        if (GameManager.INSTANCE == null)
        {
            SceneManager.LoadScene("LevelSelectScene");
        }
        else
        {
            gameManager = GameManager.INSTANCE;
            gameManager.setUIController(this);
        }
    }

    void Start()
    {
        brave = gameManager.getBrave();
        SetCenterText("Game Start!");
        ShowCenterText();
        Invoke("DisappearCenterText", 2f);
        ShowPauseButton();
        DisappearPauseFront();
        DisappearAllBuffListBuff();
        DisappearEndGameFront();
        DisappearGetBuffFront();
        //DisappearGetBuff();

        //rhythmCount = 0;
        //braveMaxHP = 100;
        //braveHP = 100;
        //braveAP = 100;
        //enemiesNum = 0;
        //maxRhythmCount = 0;
        //DisappearRythmText();
    }
    void Update()
    {
        //显示在场敌人数
        
        //节奏数随时间减少
        /*
        rhythmCount -= Time.deltaTime * 4;
        if (rhythmCount < 0)
        {
            rhythmCount = 0;
            DisappearCenterText();
        }
        //SetCenterText(rhythmCount.ToString());
        SetRhythmText(rhythmCount);
        SetBraveHPText();
        SetBraveAPText();
        */
        SetBraveAPText(brave.getLife().getBraveAP());
        //SetSkillNumText(brave.getSkillNum());
    }


    /////////////////////////////////////////////////////////////<外部使用>
    public void ShowDeathEnd()
    {
        Time.timeScale = 0.2f;
        endGame = true;
        ShowEndGameFront();
        DisappearPauseButton();
        SetEndGameMessageText("很遗憾！闯关失败！");
        //ShowReviveButton();
        //DisappearNextLevelButton();
    }
    public void ShowWinEnd()
    {
        Time.timeScale = 0.2f;
        endGame = true;
        ShowEndGameFront();
        DisappearPauseButton();
        SetEndGameMessageText("恭喜你！闯关成功！");
        //ShowNextLevelButton();
        //DisappearReviveButton();
    }
    public void GetBuff(int buffListIndex , int BuffIndex)
    {
        SetBuffListBuff(buffListIndex, BuffIndex);
        SetTheBuff(BuffIndex);
        ShowGetBuffFront();
        Invoke("ResumeButton", 1f);
    }
    /////////////////////////////////////////////////////////////<外部使用/>

    /////////////////////////////////////////////////////////////<button相关>
    public void PauseButton()
    {
        if (endGame)
        {
            //ShowPauseButton();
            DisappearPauseButton();
            return;
        }
        if (freeze)
        {
            Time.timeScale = 1f;
            freeze = false;
            DisappearPauseFront();
            DisappearGetBuffFront();
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
        DisappearGetBuffFront();
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
    /*
    public void NextLevelButton()
    {
        gameManager.LoadNextScene();
        Time.timeScale = 1f;
    }
    */
    /*
    public void ReviveButton()
    {

    }
    */
    /////////////////////////////////////////////////////////////<button相关/>

    /////////////////////////////////////////////////////////////<各种组件显示和隐藏>
    public void ShowCenterText()
    {
        centerText.transform.gameObject.SetActive(true);
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
    public void ShowPauseFront()
    {
        Time.timeScale = 0f;
        freeze = true;
        SetBraveAtkText(brave.getCurrAtk());
        SetBraveDefText(brave.getLife().getBraveDef());
        SetBraveHPText(brave.getLife().getBraveCurrentHP(),brave.getLife().getBraveMaxHP());
        pauseFront.transform.gameObject.SetActive(true);
    }
    public void DisappearPauseFront()
    {
        Time.timeScale = 1f;
        freeze = false;
        pauseFront.transform.gameObject.SetActive(false);
    }
    public void ShowEndGameFront()
    {
        Time.timeScale = 0.2f;
        endGameFront.transform.gameObject.SetActive(true);
    }
    public void DisappearEndGameFront()
    {
        Time.timeScale = 1f;
        freeze = false;
        endGameFront.transform.gameObject.SetActive(false);
    }
    public void ShowGetBuffFront()
    {
        Time.timeScale = 0.2f;
        freeze = true;
        theGetBuffFront.transform.gameObject.SetActive(true);
    }
    public void DisappearGetBuffFront()
    {
        Time.timeScale = 1f;
        freeze = false;
        theGetBuffFront.transform.gameObject.SetActive(false);
    }
    /*
    public void ShowGetBuff()
    {
        Time.timeScale = 0f;
        freeze = true;
        getBuff.transform.gameObject.SetActive(true);
    }
    public void DisappearGetBuff()
    {
        Time.timeScale = 1f;
        freeze = false;
        getBuff.transform.gameObject.SetActive(false);
    }
    */
    public void ShowBuffListBuff(int index)
    {
        buffs[index].transform.gameObject.SetActive(true);
    }
    public void DisappearBuffListBuff(int index)
    {
        buffs[index].transform.gameObject.SetActive(false);
    }
    public void DisappearAllBuffListBuff()
    {
        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i].transform.gameObject.SetActive(false);
        }
    }
    /*
    public void ShowNextLevelButton()
    {
        //nextLevelButton.transform.gameObject.SetActive(true);
    }
    public void DisappearNextLevelButton()
    {
        //nextLevelButton.transform.gameObject.SetActive(false);
    }
    */
    /*
    public void ShowReviveButton()
    {
        //reviveButton.transform.gameObject.SetActive(true);
    }
    public void DisappearReviveButton()
    {
        //reviveButton.transform.gameObject.SetActive(false);
    }
    */
    /*
    public void ShowRhythmText()
    {
        //rhythmText.transform.gameObject.SetActive(true);
    }
    public void DisappearRythmText()
    {
        rhythmText.transform.gameObject.SetActive(false);
    }
    */
    /////////////////////////////////////////////////////////////<各种组件显示和隐藏/>


    /////////////////////////////////////////////////////////////<信息设置>
    public void SetCenterText(string message)
    {
        centerText.GetComponent<Text>().text = message;
    }
    public void SetSkillNumText(int num)
    {
        skillNumText.GetComponent<Text>().text = "必杀次数：" + num.ToString();
    }
    public void SetCurrentEnemyWavesText(int currentNum, int totalNum) 
    {
        currentEnemyWavesText.GetComponent<Text>().text = "当前敌人波数：" + currentNum.ToString() + "/" + totalNum.ToString();
    }
    public void SetCurrentEnemyNumText(int num)
    {
        currentEnemyNumText.GetComponent<Text>().text = "当前剩余敌人：" + num.ToString();
    }
    public void SetBraveAtkText(float atk)
    {
        braveAtkText.GetComponent<Text>().text = "攻击：" + atk.ToString();
    }
    public void SetBraveHPText(float currentHp, float maxHp)
    {
        braveHPText.GetComponent<Text>().text = "血量：" + currentHp.ToString() + "/" + maxHp.ToString();
    }
    public void SetBraveDefText(float def)
    {
        braveDefText.GetComponent<Text>().text = "防御：" + def.ToString();
    }
    public void SetBuffListBuff(int index, int buffIndex)
    {

    }
    public void SetTheBuff(int buffIndex)
    {

    }
    public void SetEndGameMessageText(string message)
    {
        endGameMessageText.GetComponent<Text>().text = message;
    }
    public void SetBraveAPText(float braveAP)
    {
        braveAPText.GetComponent<Text>().text = "AP：" + ((int)(braveAP + 0.5)).ToString() + "/100";
    }

    /*
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
        else if (level > 10 && level <= 25)
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
        else if (level > 135 && level <= 200)
        {
            rhythmText.GetComponent<Text>().text = "SSS";
            ShowRhythmText();
        }
        else
        {
            rhythmText.GetComponent<Text>().text = "Devil May Cry!!!";
            if (rhythmCount > 210)
            {
                rhythmCount = 210;
            }
            ShowRhythmText();
        }
    }
    static public void AddRythmCount(float count)
    {
        rhythmCount += count;
    }
    static public void SubtractRythmCount(float count)
    {
        rhythmCount -= count;
    }
    */

    /*
    static public void SetBraveAP(float ap)
    {
        braveAP = ap;
    }
    public void SetBraveHPText()
    {
        braveHPText.GetComponent<Text>().text = "HP：" + ((int)(braveHP + 0.5)).ToString() + "/" + ((int)(braveMaxHP + 0.5)).ToString();
    }
    static public void SetBraveHP(float hp)
    {
        braveHP = hp;
    }
    static public void SetBraveMaxHP(float maxHp)
    {
        braveMaxHP = maxHp;
    }
    */

    /////////////////////////////////////////////////////////////<信息设置/>
}
