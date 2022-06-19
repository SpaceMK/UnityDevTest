using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour, IInputHandler
{


    private ICommand commandHandler = new CommandHandler(); //This can be loaded with DI 

    public IShipController ShipController { get; set; }

    void Update()
    {
        if (ShipController == null)
            return;

        if (Input.GetMouseButtonDown(0))
        {
             commandHandler.ShootCommand(ShipController);
        }

        commandHandler.MoveCommand(0,Input.GetAxis("Vertical"),ShipController);

        commandHandler.RotateCommand(ShipController, Input.GetAxis("Horizontal"));
    }

}
