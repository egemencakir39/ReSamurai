using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.R))
    {
      ReloadScene();
    }
  }
  

  public void ReloadScene()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
}
