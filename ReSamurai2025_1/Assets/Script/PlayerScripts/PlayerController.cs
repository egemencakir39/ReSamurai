using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using Zenject;


public class PlayerController : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject player, katanaAttackEffect;
    
    private DragController dragController;
    private Queue<GameObject> cloneQueue = new Queue<GameObject>();
    private bool _isMoving = false;
    private Animator playerAnimator;
    private GameObject currentClone;
    [Inject]
    public void Construct(DragController dragController)
    {
        this.dragController = dragController;
    }
    private void Start()
    {
        playerAnimator = player.GetComponent<Animator>();
    }



    public void MoveToClones()
    {
        cloneQueue.Clear(); 

        foreach (GameObject clone in dragController.GetClones()) 
        {
            cloneQueue.Enqueue(clone); 
        }

        if (cloneQueue.Count == 0) return; 

        playerAnimator.SetTrigger("ReadyToAttack");
        _isMoving = true;

        MoveToNextClone();
    }

    void MoveToNextClone()
    {
        if (cloneQueue.Count == 0) 
        {
            _isMoving = false;
            
            FindObjectOfType<GameManager>().EndAttackPhaseCheck();
            return;
        }

       
        
        currentClone = cloneQueue.Dequeue();

        player.transform.DOMove(currentClone.transform.position, 0.2f).OnComplete(() =>
        {
            playerAnimator.SetTrigger("Attack");
        });
        
    }

    public void OnAttackAnimationEnd()
    {
        Instantiate(katanaAttackEffect,transform.position,Quaternion.identity);
        if (currentClone != null)
        {
            currentClone.GetComponent<PlayerClone>().CheckForEnemies();
        }
        playerAnimator.SetTrigger("Idle");
        MoveToNextClone();
    }
}


