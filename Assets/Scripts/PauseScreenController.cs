using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseScreenController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI muteText;

    private void Start()
    {
        muteText.gameObject.SetActive(gameManager.isGameMute);
    }

    public void ResumeGame()
    {
        gameManager.isTouchEnabled = true;
        gameManager.SlideIn();
        this.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnMuteButtonClicked()
    {
        if (gameManager.isGameMute)
        {
            gameManager.audioManager.UnmuteAllAudio();
            muteText.gameObject.SetActive(false);
            gameManager.isGameMute = false;
        }
        else
        {
            gameManager.audioManager.MuteAllAudio();
            muteText.gameObject.SetActive(true);
            gameManager.isGameMute = true;
        }

    }
}
