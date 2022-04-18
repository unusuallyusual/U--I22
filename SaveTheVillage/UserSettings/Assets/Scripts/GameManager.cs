using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public ImageCyclesTimer HarvestTimer;
  public ImageCyclesTimer EatingTimer;
  public Image RaidTimerImg;
  public Image PeasantTimerImg;
  public Image WarriorTimerImg;

  public Button peasantButton;
  public Button warriorButton;

  public Text resourcesText;
  public Text resourcesText2;
  public Text enemyText;
  public Text resultText;
  public Text resultWinText;

  public GameObject EnemyPlus;
  public GameObject GamePanel;
  public GameObject PausePanel;
  public GameObject GameOverScreen;
  public GameObject WinScreen;

  public GameObject Info1;
  public GameObject Info2;
  public GameObject Info3;
  public GameObject Info4;
  public GameObject NextInfoButton;

  public GameObject firstBonus;
  public GameObject secondBonus;
  public GameObject thirdBonus;

  public GameObject noteImage;
  public GameObject musicButtonOff;
  public GameObject musicButtonOn;
  public GameObject soundButtonOff;
  public GameObject soundButtonOn;

  public AudioSource gameMusic;
  public AudioSource peasantSound;
  public AudioSource warriorSound;
  public AudioSource battleSound;
  public AudioSource eatingSound;
  public AudioSource employeeHiredSound;

  public int peasantCount;
  public int warriorsCount;
  public int wheatCount;

  public int wheatPerPeasant;
  public int wheatToWarriors;

  public int peasantCost;
  public int warriorCost;

  public int raidIncrease;
  public int nextRaid;

  public int bonus1;
  public int bonus2;
  public int bonus3;

  public float peasantCreateTime;
  public float warriorCreateTime;
  public float raidMaxTime;

  private int raidCount;
  private int killedEnemiesCount;
  private int countInfoMenu;

  private float peasantTimer = -2;
  private float warriorTimer = -2;
  private float raidTimer;
 
  private bool buttonPause = false;

  void Start()
    {
        UpdateText();
        Time.timeScale = 1;
        raidTimer = raidMaxTime;
        MusicOn();
        AudioListener.volume = 1;
  }

  void Update()
    {
        raidTimer -= Time.deltaTime;
        RaidTimerImg.fillAmount = raidTimer / raidMaxTime;

        //время набега врага
        if (raidTimer <= 0)
        {
            raidTimer = raidMaxTime;
            warriorsCount -= nextRaid;
            killedEnemiesCount += nextRaid;
            nextRaid += raidIncrease;
            raidCount += 1;
            battleSound.Play();
        }

        //предостережение в начале игры
        if (raidTimer > 25 && raidTimer < 29 && raidCount < 2)
          noteImage.SetActive(true);
        else
          noteImage.SetActive(false);

        //цикл сбора урожая
        if (HarvestTimer.Tick)
        {
            wheatCount += peasantCount * wheatPerPeasant;
        }

        //цикл еды
        if (EatingTimer.Tick)
        {
            wheatCount -= warriorsCount * wheatToWarriors;
            eatingSound.Play();
        }

        if (peasantTimer > 0)
        {
            peasantTimer -= Time.deltaTime;
            PeasantTimerImg.fillAmount = peasantTimer / peasantCreateTime;
        }
        else if (peasantTimer > -1)
        {
            PeasantTimerImg.fillAmount = 1;
            peasantButton.interactable = true;
            peasantCount += 1;
            peasantTimer = -2;
            employeeHiredSound.Play();
    }


        if (warriorTimer > 0)
        {
            //условия бонусов
            if (wheatCount > bonus1)
            {
              warriorTimer -= 2 * Time.deltaTime;
              firstBonus.SetActive(true);
            }
            if (wheatCount > bonus2)
            {
              warriorTimer -= 5 * Time.deltaTime;
              secondBonus.SetActive(true);
            }
            if (wheatCount > bonus3)
            {
              warriorTimer -= 7 * Time.deltaTime;
              thirdBonus.SetActive(true);
              raidIncrease = 3;
              EnemyPlus.SetActive(true);
            }
            else
            {
              warriorTimer -= Time.deltaTime;
            }

            WarriorTimerImg.fillAmount = warriorTimer / warriorCreateTime;
        }
        else if (warriorTimer > -1)
        {
          WarriorTimerImg.fillAmount = 1;
          warriorButton.interactable = true;
          warriorsCount += 1;
          warriorTimer = -2;
          employeeHiredSound.Play();
    }

        UpdateText();

        if (peasantCount <= 0)
              peasantCount = 0;

        if (buttonPause)
        {
            PausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            PausePanel.SetActive(false);
            Time.timeScale = 1;
        }

        //условия победы
        if (peasantCount >= 55 || wheatCount >= 5000)
        {
          Time.timeScale = 0;
          WinScreen.SetActive(true);
        }

        //условия поражения
        if (warriorsCount < 0 || wheatCount < 0)
            {
              GameOverScreen.SetActive(true);
              Time.timeScale = 0;
            }

        //инфо описания истории
        if (countInfoMenu == 0)
        {
          Info1.SetActive(false);
          Info2.SetActive(false);
          Info3.SetActive(false);
          Info4.SetActive(false);
          NextInfoButton.SetActive(false);
        }
        if (countInfoMenu == 1)
        {
          NextInfoButton.SetActive(true);
          Info1.SetActive(true);
        }
        if (countInfoMenu == 2)
        {
          Info2.SetActive(true);
        }
        if (countInfoMenu == 3)
        {
          Info3.SetActive(true);
        }
        if (countInfoMenu == 4)
        {
          Info4.SetActive(true);
        }
        if (countInfoMenu == 5)
        {
          countInfoMenu = 0;
        }
  }


    public void CreatePeasant()
    {
    wheatCount -= peasantCost;
    peasantTimer = peasantCreateTime;
    peasantButton.interactable = false;
    peasantSound.Play();
    }

    public void CreateWarrior()
    {
    wheatCount -= warriorCost;
    warriorTimer = warriorCreateTime;
    warriorButton.interactable = false;
    warriorSound.Play();
    }

    public void RestartGame()
    {
        GameOverScreen.SetActive(false);
        WinScreen.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void PauseGame()
    {
    buttonPause = !buttonPause;
    countInfoMenu = 0;
  }

    public void MusicOff()
    {
      gameMusic.Stop();
      musicButtonOff.SetActive(false);
      musicButtonOn.SetActive(true);
    }
    public void MusicOn()
    {
      gameMusic.Play();
      musicButtonOn.SetActive(false);
      musicButtonOff.SetActive(true);
    }

    public void SoundOff()
    {
      AudioListener.volume = 0;
      soundButtonOff.SetActive(false);
      soundButtonOn.SetActive(true);
    }
    public void SoundOn()
    {
      AudioListener.volume = 1;
      soundButtonOff.SetActive(true);
      soundButtonOn.SetActive(false);
    }

    public void NextInfoButtonClick()
    {
      countInfoMenu++;
    }

    private void UpdateText()
      {
        resourcesText.text = peasantCount + "\n" + warriorsCount + "\n\n" + wheatCount;
        resourcesText2.text = Convert.ToString(wheatCount);
        enemyText.text = Convert.ToString(nextRaid);
        resultText.text = peasantCount + "\n\n\n" + wheatCount + "\n\n\n" + raidCount + "\n\n\n" + (killedEnemiesCount - nextRaid + raidIncrease);
        resultWinText.text = resultText.text;
      }
}
