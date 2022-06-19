using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShipController
{ 
    IWeaponController WeaponController { get; }

    IMovementController MovementController { get; }

    public IHealthController HealthController { get; }

    Vector3 GetShipPosition();

    void LoadDependencies(IGameManager manager, IGameBoard gameBoard,IAsteroidManager asteroidManager, IObjectPool pool, IPhysicsSimulator moveObject);
}
