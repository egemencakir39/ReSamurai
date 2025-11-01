using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButtonController : MonoBehaviour
{
    [SerializeField] private Image buttonImage;
    [SerializeField] private Sprite unlockedSprite;
    [SerializeField] private Sprite lockedSprite;
    [SerializeField] private Button levelButton;
    [SerializeField] private int levelNumber;
    [SerializeField] private GameObject levelTextObject;

    private void Start()
    {
        if (levelNumber == 1)
        {
            UnlockLevel();
            return;
        }

        if (PlayerPrefs.GetInt("Level_" + levelNumber + "_Unlocked", 0) == 1)
        {
            UnlockLevel();
        }
        else
        {
            LockLevel();
        }
    }

    void UnlockLevel()
    {
        buttonImage.sprite = unlockedSprite;
        levelButton.interactable = true;
        levelTextObject.SetActive(true);
    }

    void LockLevel()
    {
        levelTextObject.SetActive(false);
        buttonImage.sprite = lockedSprite;
        levelButton.interactable = false;
    }
}
