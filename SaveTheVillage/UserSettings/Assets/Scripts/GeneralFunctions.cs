using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralFunctions : MonoBehaviour
{
  public AudioSource clickButtonSound;

  public void ClickButtonSound()
  {
    clickButtonSound.Play();
  }
}
