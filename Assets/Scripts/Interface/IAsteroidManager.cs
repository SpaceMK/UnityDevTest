using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAsteroidManager
{
    void LoadDependencies(IGameBoard gameBoard, IShipController shipController,IGameManager gameManager, IObjectPool pool, IPhysicsSimulator moveObject);

    void ObjectHit(IPoolComponent asteroid);

    List<IPoolComponent> Asteroids { get; }
}
