using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSimulator : MonoBehaviour, IPhysicsSimulator
{
    public IGameBoard GameBoard { get; set; }
    public Action<IPoolComponent> RemoveFromCirculation { get; set; }


    [SerializeField] [Range(0f, 1f)] float collisionDetection;

    List<IPoolComponent> moveableObjects = new List<IPoolComponent>();
    IShipController shipController;
    IGameManager gameManager;



    public void LoadDependencies(IGameManager manager,IGameBoard board, IShipController ship)
    {
        gameManager = manager;
        GameBoard = board;
        shipController = ship;
        gameManager.GameOver += ResetMoveObject;
    }

    private void Update()
    {
        for (int i = 0; i < moveableObjects.Count; i++)
        {
           MoveObject(moveableObjects[i]);  
        }
    }

    private void MoveObject(IPoolComponent moveableObj)
    {
        if (CheckIfPositionIsValid(moveableObj.SceneGameObject.transform.position))
        {
            if (!CheckForCollisions(moveableObj))
            {
                moveableObj.SceneGameObject.transform.position += moveableObj.Trajectory*moveableObj.Speed*Time.deltaTime;
            }
            else
            {
                if (moveableObj.ObjectType == PoolObjectType.Projectile)
                {
                    RemoveFromCirculation?.Invoke(moveableObj);
                }
                moveableObjects.Remove(moveableObj);
            }     
        }
        else
        {
            moveableObjects.Remove(moveableObj);
            RemoveFromCirculation?.Invoke(moveableObj);
        }
    }

    public void AddToList(List<IPoolComponent> addToList)
    {
        moveableObjects.AddRange(addToList);
    }
    private bool CheckForCollisions(IPoolComponent moveableObj)
    {
        var collision = false;
        switch (moveableObj.ObjectType)
        {
            case PoolObjectType.Asteroid:
                collision = CheckCollisionsWithPlayer(moveableObj);
                if (collision)
                    gameManager.GameOver?.Invoke();
                break;
            case PoolObjectType.Projectile:
                CheckCollisionsWithAsteroid(moveableObj);
                break;
            default:
                break;
        }
        return collision;
    }

    private bool CheckCollisionsWithAsteroid(IPoolComponent moveableObj)
    {
        for (int i = 0; i < moveableObjects.Count; i++)
        {
            if (moveableObjects[i].ObjectType == PoolObjectType.Asteroid)
            {
                if (Vector3.Distance(moveableObj.SceneGameObject.transform.position, moveableObjects[i].SceneGameObject.transform.position) <= collisionDetection)
                {
                    RemoveFromCirculation?.Invoke(moveableObj);
                    RemoveFromCirculation?.Invoke(moveableObjects[i]);
                    moveableObjects.Remove(moveableObj);
                    moveableObjects.Remove(moveableObjects[i]);
                    gameManager.HitMade?.Invoke();
                    return true;
                }
            } 
        }
        return false;
    }

    private bool CheckCollisionsWithPlayer(IPoolComponent moveableObj)
    {
        return Vector3.Distance(moveableObj.SceneGameObject.transform.position,shipController.GetShipPosition())<=collisionDetection;
    }

    public bool CheckIfPositionIsValid(Vector3 position)
    {
        if (position.x > GameBoard.HorizotalCameraBoundary + 5f || position.x < (GameBoard.HorizotalCameraBoundary + 5f) * -1)
            return false;
        else if ((position.y > GameBoard.VerticallCameraBoundary + 5f || position.y < (GameBoard.VerticallCameraBoundary + 5f) * -1))
            return false;
        else
            return true;
    }

    private void ResetMoveObject()
    {
        moveableObjects.Clear();
    }
}
