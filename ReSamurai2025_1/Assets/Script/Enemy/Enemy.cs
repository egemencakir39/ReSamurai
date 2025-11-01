using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject DeathStar;
    [SerializeField] private GameObject Eye1;
    [SerializeField] private GameObject Eye2;
    private Animator animator;
    
    private GameManager gameManager;

    private void Start()
    {
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        gameManager.RegisterEnemy(this);
    }

    public void TakeDamage()
    {
        animator.SetTrigger("KnockOut");
        gameManager.UnregisterEnemy(this);
    }

    public void DeathStarActive()
    {
        Eye1.SetActive(true);
        Eye2.SetActive(true);
        DeathStar.SetActive(true);
        
        Eye1.transform.DOLocalRotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);

        Eye2.transform.DOLocalRotate(new Vector3(0, 0, -360), 1.2f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }
}
