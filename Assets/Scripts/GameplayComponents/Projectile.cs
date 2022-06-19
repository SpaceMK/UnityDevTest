using UnityEngine;

public class Projectile : IPoolComponent
{
    public GameObject SceneGameObject { get; set; }
    public Vector3 Trajectory { get; set; }
    public float Speed { get; set; }
    public PoolObjectType ObjectType { get; set; }
}
