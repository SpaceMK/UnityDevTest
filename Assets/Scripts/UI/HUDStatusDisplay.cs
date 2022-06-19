using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDStatusDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI statusDisplay;
    [SerializeField] Slider healthBar;
    public void Init(IGameManager gameManager)
    {
        gameManager.GameOver += GameOverDisplay;
        gameManager.StartGame += StartGameDisplay;
        gameManager.UpdateHealth += UpdateHealthBar;
    }


    private void GameOverDisplay()
    {
        statusDisplay.text = "Starting over -- Get Ready";
    }

    private void StartGameDisplay()
    {
        statusDisplay.text = "Starting Game";
    }

    private void UpdateHealthBar(float health)
    {
        healthBar.value = health;
    }
}
