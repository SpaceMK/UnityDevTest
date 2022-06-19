using System.Collections.Generic;
using UnityEngine;

public class ShipFireControl : MonoBehaviour, IWeaponController
{

    [SerializeField] float shipFireRate;
    [SerializeField] float projectileSpeed;

    IAsteroidManager asteroidManager;
    IObjectPool projectilePool;
    IPhysicsSimulator objectMove;

    List<IPoolComponent> projectiles = new List<IPoolComponent>();

    float fireTimer;
    bool isEnabled = true;
    bool canFire = true;

    public void LoadDependencies(IPhysicsSimulator move, IAsteroidManager manager, IObjectPool pool)
    {
        asteroidManager = manager;
        projectilePool = pool;
        objectMove = move;
        fireTimer = shipFireRate;
        objectMove.RemoveFromCirculation += RemoveProjectileFromCirculation;
        projectilePool.CreateObject(PoolObjectType.Projectile, 100);
     
    }

    void Update()
    {
        if (!isEnabled)
            return;

        ShipFireTimer();
    }

    private void ShipFireTimer()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer < 0)
        {
            canFire = true;
            fireTimer = shipFireRate;
        }
    }

    public void FireWeapon(IFireCommand command)
    {
        if (!isEnabled || !canFire)
            return;

        canFire = false;
        var projectile = projectilePool.FetchObjectFromPool(PoolObjectType.Projectile);
        projectile.SceneGameObject.transform.SetParent(null);
        projectile.SceneGameObject.transform.position = this.transform.position;
        projectile.SceneGameObject.transform.rotation = this.transform.rotation;
        projectile.Trajectory = projectile.SceneGameObject.transform.up;
        projectile.Speed = projectileSpeed;
        projectile.ObjectType = PoolObjectType.Projectile;
        projectile.SceneGameObject.SetActive(true);
        projectiles.Add(projectile);

        objectMove.AddToList(projectiles);
    }

   


    private void RemoveProjectileFromCirculation(IPoolComponent projectile)
    {
        if (projectile.ObjectType == PoolObjectType.Asteroid)
            return;

       projectiles.Remove(projectile);
       projectilePool.ReturnObjectToPool(PoolObjectType.Projectile, projectile);
    }

  
    public void EnableComponent(bool toogle)
    {
        isEnabled =toogle;
        if (!toogle)
        {
            ResetProjectiles();
        }
    }

    void ResetProjectiles()
    {
        for (int i = projectiles.Count-1; i >=0 ; i--)
        {
            RemoveProjectileFromCirculation(projectiles[i]);
        }
    }
}
