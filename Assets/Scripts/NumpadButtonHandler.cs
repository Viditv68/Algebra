using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumpadButtonHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem boomVfx;

    [SerializeField] Animator btnAnim;
    [SerializeField] GameManager gameManager;

    public void HandleButtonClick(int value)
    {
       
        gameManager.GetButtonInput(value);
        boomVfx.Play();
        btnAnim.SetTrigger("Pressed");
        //GameManager.Instance.audioManager.PlayAudioClipByKey(SoundKey.ButtonClick);
    }
}
