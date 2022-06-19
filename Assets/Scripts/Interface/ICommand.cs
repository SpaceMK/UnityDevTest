using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    void ShootCommand(IShipController shipController);

    void MoveCommand(float axisX,float axisY, IShipController shipController);

    void RotateCommand(IShipController shipController , float mouseXAxis);
}

