using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioClip dragSound;
    [SerializeField] private AudioClip killSound;
    [SerializeField] private AudioClip unKilledSound;
    
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
    public void DragSound() => PlaySound(dragSound);
    public void KillSound() => PlaySound(killSound);
    public void UnKilledSound() => PlaySound(unKilledSound);
}
