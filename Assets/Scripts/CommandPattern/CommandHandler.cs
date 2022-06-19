using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandHandler : ICommand
{

    IFireCommand fireCommand = new FireCommand();
    IMoveCommand movemetCommand = new MoveCommand();
    IMoveCommand rotationCommand = new MoveCommand();
 

    public void ShootCommand(IShipController shipController)
    {
        fireCommand.WeaponFired = true;
        shipController.WeaponController.FireWeapon(fireCommand);
    }

    public void MoveCommand(float axisX, float axisY, IShipController shipController)
    {
        movemetCommand.MovementYAxis = axisY;
        shipController.MovementController.Move(movemetCommand);
    }

    public void RotateCommand(IShipController shipController, float mouseXAxis)
    {
        rotationCommand.RotationAxis = (mouseXAxis*-1);
        shipController.MovementController.Rotate(rotationCommand);
    }
}
