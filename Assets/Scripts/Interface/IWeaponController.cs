using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponController
{
    void FireWeapon(IFireCommand command);
    void EnableComponent(bool toogle);
    void LoadDependencies(IPhysicsSimulator move, IAsteroidManager manager, IObjectPool pool);
}
