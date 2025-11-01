using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
   [SerializeField] private GameObject pauseMenu;

   public void PauseGame()
   {
      pauseMenu.SetActive(true);
      Time.timeScale = 0f;
   }

   public void ResumeGame()
   {
      pauseMenu.SetActive(false);
      Time.timeScale = 1f;
   }

   public void MainMenu()
   {
      pauseMenu.SetActive(false);
      Time.timeScale = 1f;
      UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
   }
}
