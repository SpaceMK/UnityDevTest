using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour,IAsteroidManager
{
    IGameBoard gameBoard;
    IShipController shipController;
    IGameManager gameManager;
    IObjectPool astoroidPool;

    [Header("Asteroid Paramaters")]
    [SerializeField] float asteroidSpeed = 1f;

    [Header("Spawning Paramaters")]
    [SerializeField] float spawnTimer;
    [SerializeField] float spawnDistance = 12f;
    [SerializeField] int amountPerSpawn = 1;
    [Range(0f, 45f)]
    [SerializeField] float trajectoryVariance = 15f;

    float timerCountDown;
    List<IPoolComponent> asteroids = new List<IPoolComponent>();
    bool isOperational = false;
    IPhysicsSimulator moveObject;

    public List<IPoolComponent> Asteroids => asteroids;

    public void LoadDependencies(IGameBoard board, IShipController controller, IGameManager manager, IObjectPool pool, IPhysicsSimulator move)
    {
        gameManager = manager;
        gameBoard = board;
        shipController = controller;
        astoroidPool = pool;
        moveObject = move;
        gameManager.GameOver += ResetComponent;
        gameManager.StartGame += EnableComponent;
        moveObject.RemoveFromCirculation += RemoveAsteroidFromCirculation;
        astoroidPool.CreateObject(PoolObjectType.Asteroid,50);
        isOperational = true;
    }


    private void Update()
    {
        if (!isOperational)
            return;

        RunSpawnTimer();
    }

    private void RunSpawnTimer()
    {
        timerCountDown -= Time.deltaTime;
        if (timerCountDown < 0)
        {
            Spawn();
            timerCountDown = spawnTimer;
        }
    }

    private void Spawn()
    {
        for (int i = 0; i < amountPerSpawn; i++)
        {
            Vector2 spawnDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnPoint = spawnDirection * spawnDistance;
            spawnPoint += transform.position;
    
            float variance = Random.Range(-trajectoryVariance, trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            var asteroid = astoroidPool.FetchObjectFromPool(PoolObjectType.Asteroid);
            asteroid.SceneGameObject.SetActive(true);
            asteroid.SceneGameObject.transform.SetParent(this.transform);
            asteroid.SceneGameObject.transform.position = spawnPoint;
          
            asteroid.Speed = asteroidSpeed;
            Vector2 trajectory = rotation * -spawnDirection;
            asteroid.Trajectory =trajectory;
            asteroids.Add(asteroid);
         
        }

        moveObject.AddToList(asteroids);
    }

    private void RemoveAsteroidFromCirculation(IPoolComponent asteroid)
    {
        if (asteroid.ObjectType == PoolObjectType.Projectile)
            return;

        asteroid.Trajectory = Vector3.zero;
        asteroid.Speed = 0;
        asteroid.SceneGameObject.transform.SetParent(null);
        asteroid.SceneGameObject.SetActive(false);
        asteroids.Remove(asteroid);
        astoroidPool.ReturnObjectToPool(PoolObjectType.Asteroid, asteroid);
    }

    public void ObjectHit(IPoolComponent asteroid)
    {
        RemoveAsteroidFromCirculation(asteroid);
    }

    private void ResetComponent()
    {
        for (int i = asteroids.Count-1; i >= 0; i--)
        {
            RemoveAsteroidFromCirculation(asteroids[i]);
        }
        isOperational = false;
        asteroids.Clear();
    }

    private void EnableComponent()
    {
        isOperational = true;
        timerCountDown = spawnTimer;
        Spawn();
    }
}


