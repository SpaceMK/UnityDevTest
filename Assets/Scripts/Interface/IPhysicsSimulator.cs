using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPhysicsSimulator
{
    void AddToList(List<IPoolComponent> addToList);
    void LoadDependencies(IGameManager manager,IGameBoard board,IShipController ship);
    Action<IPoolComponent> RemoveFromCirculation { get; set; }
    IGameBoard GameBoard { get; set; }
  
}
