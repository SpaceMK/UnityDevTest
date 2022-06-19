public interface IObjectPool
{
    IPoolComponent FetchObjectFromPool(PoolObjectType type);
    void CreateObject(PoolObjectType objectType, int numberOfObjects);
    void ReturnObjectToPool(PoolObjectType type, IPoolComponent component);
}
