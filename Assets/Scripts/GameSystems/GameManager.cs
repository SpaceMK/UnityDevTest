using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour,IGameManager,IGameBoard
{
    public Action GameOver { get; set; }
    public Action StartGame { get; set; }
    public Action HitMade { get; set; }
    public Action<float> UpdateHealth { get; set; }
    public Camera MainCamera => gameCamera;
    public float HorizotalCameraBoundary { private set; get; }
    public float VerticallCameraBoundary { private set; get; }
    public IGameBoard GameBoard => this;

    [SerializeField] Camera gameCamera;
    [SerializeField] GameObjectPool objectPoolPrefab;
    [SerializeField] SpaceshipController spaceshipControllerPrefab;
    [SerializeField] AsteroidManager asteroidManagerPrefab;
    [SerializeField] InputHandler inputHandlerPrefab;
    [SerializeField] PhysicsSimulator moveObjectsPrefab;

    IInputHandler inputHandler;
    IShipController shipController;
    IAsteroidManager asteroidManager;
    IObjectPool pool;
    IPhysicsSimulator objectMove;

    void Start()
    {
        GameOver += RestartGame;
        CalculateCameraBounds();
        shipController = CrateSpaceShip();
        inputHandler = CreateInputHandler();
        asteroidManager = CreateAsteroidManager();
        pool = CreateGameObjectPool();
        objectMove = CreateObjectMover();
        LoadDependencies();
    }
    public void CalculateCameraBounds()
    {
        var vertExtent =MainCamera.orthographicSize;
        var horzExtent = vertExtent * Screen.width / Screen.height;

        VerticallCameraBoundary = vertExtent;
        HorizotalCameraBoundary = horzExtent;
    }


    private IShipController CrateSpaceShip()
    {
        return Instantiate(spaceshipControllerPrefab.gameObject).GetComponent<SpaceshipController>();
    }

    private IInputHandler CreateInputHandler()
    {
        return Instantiate(inputHandlerPrefab.gameObject).GetComponent<InputHandler>();
    }

    private IAsteroidManager CreateAsteroidManager()
    {
        return Instantiate(asteroidManagerPrefab.gameObject).GetComponent<AsteroidManager>();
    }

    private IObjectPool CreateGameObjectPool()
    {
        return Instantiate(objectPoolPrefab).GetComponent<GameObjectPool>();
    }

    private IPhysicsSimulator CreateObjectMover()
    {
        return Instantiate(moveObjectsPrefab).GetComponent<PhysicsSimulator>();
    }

    private void LoadDependencies()
    {
        inputHandler.ShipController = shipController;
        objectMove.LoadDependencies(this,this,shipController);
        shipController.LoadDependencies(this,this,asteroidManager,objectPoolPrefab, objectMove);
        asteroidManager.LoadDependencies(this,shipController,this,objectPoolPrefab, objectMove);
    }

    private void RestartGame()
    {
        StartCoroutine(RunTimer());
    }

    IEnumerator RunTimer()
    {
        yield return new WaitForSeconds(2f);
        StartGame?.Invoke();
    }
}
