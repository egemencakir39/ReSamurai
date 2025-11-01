using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMG : MonoBehaviour
{
  public void MainMenu()
  {
    UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
  }

  public void LevelScene()
  {
    UnityEngine.SceneManagement.SceneManager.LoadScene("WorkSpaceLevel");
  }

  public void InfoScene()
  {
    UnityEngine.SceneManagement.SceneManager.LoadScene("InfoScene");
  }

  public void LevelSelectScene()
  {
    UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelect");
  }
  
  public void OpenLevelScene(int levelID)
  {
    string levelName = "Level " + levelID;
    SceneManager.LoadScene(levelName);
  }

  public void NextLevelScene(int levelID)
  {
    string levelName = SceneManager.GetActiveScene().name;
    string levelNumber = new string(levelName.Where(char.IsDigit).ToArray());
    int currentLevel = int.Parse(levelNumber);
    
    int nextLevel = currentLevel + 1;
    SceneManager.LoadScene("Level "+nextLevel);
    
  }
  public void UnlockNextLevel()
  {
    string currentScene = SceneManager.GetActiveScene().name;
    string numberPart = new string(currentScene.Where(char.IsDigit).ToArray());
    int currentLevel = int.Parse(numberPart);
    int nextLevel = currentLevel + 1;

    PlayerPrefs.SetInt("Level_" + nextLevel + "_Unlocked", 1);
  }

  public void ExitGame()
  {
    Application.Quit();
  }
}
