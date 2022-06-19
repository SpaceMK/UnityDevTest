using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementController
{
    void Move(IMoveCommand moveCommand);

    void Rotate(IMoveCommand moveCommand);

    void SetGameBoard(IGameBoard board);

    void EnableComponent(bool enable);
}