using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour
{

  public GameObject StartPanel;
  public GameObject Info1;
  public GameObject Info2;
  public GameObject Info3;
  public GameObject Info4;

  private int countInfoStart;

    void Update()
    {
        //инфо описания истории
        if (countInfoStart == 0)
        {
          StartPanel.SetActive(true);
        }
        if (countInfoStart == 1)
        {
           StartPanel.SetActive(false);
           Info1.SetActive(true);
        }
        if (countInfoStart == 2)
        {
          Info2.SetActive(true);
        }
        if (countInfoStart == 3)
        {
          Info3.SetActive(true);
        }
        if (countInfoStart == 4)
        {
          Info4.SetActive(true);
        }
        if (countInfoStart == 5)
        {
          countInfoStart = 0;
          Info1.SetActive(false);
          Info2.SetActive(false);
          Info3.SetActive(false);
          Info4.SetActive(false);
        }

  }
    public void StartGame()
    {
      SceneManager.LoadScene(0);
    }

    public void NextInfoButtonClick()
    {
      countInfoStart++;
    }
}
