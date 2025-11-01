using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   
    [Header("Timer Settings")]
    [SerializeField] private float timeLimit = 5f;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private DragController dragController;
    [SerializeField] private TextMeshProUGUI attackStartText;
    [SerializeField] private float attackStartDelay = 2f;

    [Header("Panels")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    [Header("Attack Controller")]
    [SerializeField] private PlayerController playerController;
    
    [Header("Camera Settings")]
    [SerializeField] private Cinemachine.CinemachineVirtualCamera vcam;
    private float timer;
    private bool timerActive = false;
    private bool attackPhaseStarted = false;

    private List<Enemy> aliveEnemies = new List<Enemy>();

    private void Start()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        timerText.gameObject.SetActive(true);
    }

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (timerActive && !attackPhaseStarted)
        {
            timer -= Time.deltaTime;
            timerText.text = Mathf.Ceil(timer).ToString();

            if (timer <= 0f)
            {
                StartAttackPhase();
            }
        }
    }

    public void StartTimer()
    {
        if (!timerActive)
        {
            timer = timeLimit;
            timerActive = true;
            timerText.gameObject.SetActive(true);
        }
    }

    private void StartAttackPhase()
    {
        timerActive = false;
        attackPhaseStarted = true;
        timerText.gameObject.SetActive(false);
        dragController.LockShooting();
        if (vcam != null)
        {
            vcam.Follow = null; 
            Vector3 targetPos = new Vector3(0f, 0f, vcam.transform.position.z);
            vcam.transform.DOMove(targetPos, 1f).SetEase(Ease.InOutQuad);
        }
        StartCoroutine(AttackCountdown());;
        
    }
    private IEnumerator AttackCountdown()
    {
        attackStartText.gameObject.SetActive(true);
        attackStartText.color = Color.black;
        
        Sequence flashSequence = DOTween.Sequence();
        flashSequence.Append(attackStartText.DOColor(Color.white, 0.25f));
        flashSequence.Append(attackStartText.DOColor(Color.black, 0.25f));
        flashSequence.SetLoops(-1); 

        yield return new WaitForSeconds(attackStartDelay);

        flashSequence.Kill(); 
        attackStartText.gameObject.SetActive(false);

        playerController.MoveToClones(); 
    }

    public void RegisterEnemy(Enemy enemy)
    {
        if (!aliveEnemies.Contains(enemy))
            aliveEnemies.Add(enemy);
    }

    public void UnregisterEnemy(Enemy enemy)
    {
        if (aliveEnemies.Contains(enemy))
            aliveEnemies.Remove(enemy);

        CheckIfAllEnemiesDead();
    }

    private void CheckIfAllEnemiesDead()
    {
        if (attackPhaseStarted && aliveEnemies.Count == 0)
        {
            UnlockNextLevel(); 
            PlayerPrefs.Save();
            

            winPanel.SetActive(true); 
        }
    }

    public void EndAttackPhaseCheck()
    {
        if (attackPhaseStarted && aliveEnemies.Count > 0)
        { 
            losePanel.SetActive(true);
        }
    }
    public void UnlockNextLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        string numberPart = new string(currentScene.Where(char.IsDigit).ToArray());
        int currentLevel = int.Parse(numberPart);
        int nextLevel = currentLevel + 1;

        PlayerPrefs.SetInt("Level_" + nextLevel + "_Unlocked", 1);
    }
}

