using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolComponent 
{
    PoolObjectType ObjectType { get; set; }
    GameObject SceneGameObject { get; set; }
    Vector3 Trajectory { get; set; }
    float Speed { get; set; }
}
