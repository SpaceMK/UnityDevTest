using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealthController : MonoBehaviour, IHealthController
{
    [SerializeField] float shipHealth;
    [SerializeField] float addToHealth;
    float currentShipHealth;
    IGameManager gameManager;
    bool isActive = false;

    public void LoadDependencies(IGameManager manager)
    {
        gameManager = manager;
        gameManager.HitMade += AddToShipHealth;
        gameManager.GameOver += GameOver;
        gameManager.StartGame += RestartGame;
        gameManager.UpdateHealth?.Invoke(shipHealth);
        currentShipHealth = shipHealth;
        isActive = true;
    }

    void Update()
    {
        UpdateHealth();
    }

    void UpdateHealth()
    {
        if (!isActive)
            return;

        currentShipHealth -= Time.deltaTime;
        gameManager.UpdateHealth?.Invoke(currentShipHealth);
        if (currentShipHealth < 0)
        {
            isActive = false;
            gameManager.GameOver?.Invoke();
        }
    }

    private void AddToShipHealth()
    {
        currentShipHealth = Mathf.Clamp(currentShipHealth + addToHealth, currentShipHealth + addToHealth, shipHealth);
        gameManager.UpdateHealth?.Invoke(currentShipHealth);
 
    }

    void GameOver()
    {
        gameManager.UpdateHealth?.Invoke(0);
        isActive = false;
    }

    void RestartGame()
    {
        gameManager.UpdateHealth?.Invoke(shipHealth);
        currentShipHealth = shipHealth;
        isActive = true;
    }
}
