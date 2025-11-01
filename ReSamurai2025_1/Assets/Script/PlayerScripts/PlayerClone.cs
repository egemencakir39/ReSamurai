using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;


public class PlayerClone : MonoBehaviour
{
    [SerializeField] private int AttackPose = 0;
    [SerializeField] private float detectionRadius = 6f;
    private CinemachineVirtualCamera vcam;
    private SoundManager soundManager;
    
    private void Start()
    {
        vcam = FindObjectOfType<CinemachineVirtualCamera>(); 
        soundManager = FindObjectOfType<SoundManager>();
    }

    public void CheckForEnemies()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        foreach (Collider2D enemy in enemies)
        {
            bool killed = false;
            if (enemy.CompareTag("Enemy"))
            {
                soundManager.KillSound();
                CameraShake();
                enemy.GetComponent<Enemy>().TakeDamage(); 
                killed = true;
                break;
            }

            if (!killed)
            {
                soundManager.UnKilledSound();
            }
        }
        Destroy(gameObject);
    }
    public void CameraShake()
    {
        if (vcam != null)
        {
            Transform vcamTransform = vcam.transform; 
            
            vcamTransform.DOShakePosition(
                duration: 0.25f, 
                strength: 1f, 
                vibrato: 20, 
                randomness: 90, 
                fadeOut: true
            );
        }
        else
        {
            Debug.LogError("cam bulunamadı");
        }
    }

private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        
    }
}
