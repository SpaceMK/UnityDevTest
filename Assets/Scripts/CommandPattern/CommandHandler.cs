using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandHandler : ICommand
{
    public void ScreenInteraction(ShootRaycast shootRaycast, IInteractionController interactionController,Vector3 position)
    {
        interactionController.SetObjectOfInterest(shootRaycast.RaycastHit(position));
    }
}
