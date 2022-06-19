using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameObjectPool : MonoBehaviour,IObjectPool
{
    Dictionary<PoolObjectType, List<IPoolComponent>> gameObjectPool = new Dictionary<PoolObjectType, List<IPoolComponent>>();
    Dictionary<PoolObjectType,GameObject> poolHolders = new Dictionary<PoolObjectType, GameObject>();
    [Range(1, 1000)] public int MinimalTreshold;

    public GameObject AsteroidPrefab,ProjectilePrefab;

    private GameObject GetPrefab(PoolObjectType objectType)
    {
        if (objectType == PoolObjectType.Asteroid)
            return AsteroidPrefab;
        else
            return ProjectilePrefab;
    }


    public void CreateObject(PoolObjectType objectType,int numberOfObjects)
    {
        var holder = GetPoolHolder(objectType);
        var createdPoolObjects = new List<IPoolComponent>();
        var prefab = GetPrefab(objectType);
        for (int i = 0; i < numberOfObjects; i++)
        {
           var temp = Instantiate(prefab,Vector3.zero,Quaternion.identity);
           temp.transform.SetParent(holder.transform);
           temp.name = $"{objectType}-{i}";
           IPoolComponent poolComponent = GetPoolObjectType(objectType);
           poolComponent.ObjectType = objectType;
           poolComponent.SceneGameObject = temp;
           temp.gameObject.SetActive(false);
           createdPoolObjects.Add(poolComponent);
        }
        AddToPool(createdPoolObjects,objectType);
    }

    private void AddToPool(List<IPoolComponent> toPool, PoolObjectType type)
    {
        if (gameObjectPool.ContainsKey(type))
            gameObjectPool[type].AddRange(toPool);
        else
            gameObjectPool.Add(type,toPool);
    }

    private GameObject GetPoolHolder(PoolObjectType objectType)
    {

        if (poolHolders.ContainsKey(objectType))
            return poolHolders[objectType];

        var tempHolder = new GameObject();
        tempHolder.name = $"{objectType}-Pool";
        poolHolders.Add(objectType,tempHolder);
        return poolHolders[objectType];
    }


    private IPoolComponent GetPoolObjectType(PoolObjectType poolObjectType)
    {
        if (poolObjectType == PoolObjectType.Asteroid)
            return new Asteroid();
        else
            return new Projectile();
    }

    public IPoolComponent FetchObjectFromPool(PoolObjectType type)
    {
        if (MinimalTreshold >= gameObjectPool[type].Count)
            CreateObject(type,MinimalTreshold);

        
        var availableObjects = gameObjectPool[type];
        var singleObject = availableObjects[availableObjects.Count-1];
        availableObjects.Remove(singleObject);
        return singleObject;
    }


    public void ReturnObjectToPool(PoolObjectType type, IPoolComponent component)
    {
     
        component.SceneGameObject.transform.SetParent(GetPoolHolder(type).transform);
        component.Trajectory = Vector2.zero;
        component.SceneGameObject.transform.position = new Vector2(100f,100f);
        component.SceneGameObject.transform.rotation = Quaternion.identity;
        component.Speed = 0;
        component.SceneGameObject.SetActive(false);
        if (!gameObjectPool[type].Contains(component))
        {
            gameObjectPool[type].Add(component);
        }
        else
        {
            gameObjectPool[type].RemoveAll(x => x == component);
            gameObjectPool[type].Add(component);
        }
    }
}
