using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    public void GenerateNextQuestion()
    {
        if (!gameManager.pausePanel.activeInHierarchy)
        {
            gameManager.questionsPanelAnim.gameObject.SetActive(false);
            gameManager.GenerateQuestions();
        }

    }

    public void EnableTouchStatus()
    {
        gameManager.isTouchEnabled = true;
    }

    public void EnableSpeechBubble()
    {
        gameManager.speechBubbleText.transform.parent.gameObject.SetActive(true);
    }
    public void DisableSpeechBubble()
    {
        gameManager.speechBubbleText.transform.parent.gameObject.SetActive(false);
    }
}
