using System;
using UnityEngine;

public class SpaceshipController : MonoBehaviour, IShipController
{

    [SerializeField] ShipFireControl combatController;
    [SerializeField] SpaceshipMovementController movementController;
    [SerializeField] ShipHealthController healthController;
    public IWeaponController WeaponController => combatController;
    public IMovementController MovementController => movementController;

    public IHealthController HealthController => healthController;

    IGameManager gameManager;
  

    public Vector3 GetShipPosition()
    {
        return this.transform.position;
    }

    public void LoadDependencies(IGameManager manager, IGameBoard gameBoard, IAsteroidManager asteroidManager, IObjectPool pool, IPhysicsSimulator move)
    {
        gameManager = manager;
        gameManager.GameOver += ShipHit;
        gameManager.StartGame += StartNewGame;
        WeaponController.LoadDependencies(move, asteroidManager, pool);
        MovementController.SetGameBoard(gameBoard);
        healthController.LoadDependencies(gameManager);
    }

    private void ShipHit()
    {
        WeaponController.EnableComponent(false);
        MovementController.EnableComponent(false);
    }

    private void StartNewGame()
    {
        WeaponController.EnableComponent(true);
        MovementController.EnableComponent(true);
    }
}
