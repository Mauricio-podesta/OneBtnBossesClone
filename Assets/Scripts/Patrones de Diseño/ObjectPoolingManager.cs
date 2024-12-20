using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.EditorTools;
using UnityEditor;

public enum PoolObjectType
{
    EnemyBullet,
    PlayerBullet,
    Triangle,
    Square
}

[Serializable]
public class PoolInfo
{
    public PoolObjectType type;
    public int amount = 0;
    public GameObject prefab;
    public GameObject container;

    [HideInInspector] public List<GameObject> pool = new();
}

public class ObjectPoolingManager : MonoBehaviour
{

    [SerializeField] public List<PoolInfo> listOfPools;
    [SerializeField] public Vector3 defaultObjectPosition;

    [SerializeField] public Factory objectFactory;

    public static ObjectPoolingManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void Start()
    {
        foreach (var poolInfo in listOfPools)
        {
            FillPool(poolInfo);
        }
    }
    private void FillPool(PoolInfo poolInfo)
    {
        for (int i = 0; i < poolInfo.amount; i++)
        {
            GameObject objInstance = objectFactory.CreateObject(poolInfo.type, poolInfo.container.transform); 
            objInstance.SetActive(false);

            objInstance.transform.position = defaultObjectPosition;
            poolInfo.pool.Add(objInstance);
        }
    }
    public GameObject GetPooledObject(PoolObjectType type)
    {
        PoolInfo selected = GetPoolByType(type);
        List<GameObject> pool = selected.pool;

        GameObject objInstance;

        if (pool.Count > 0)
        {
            objInstance = pool[^1];
            pool.RemoveAt(pool.Count - 1);
        }
        else
        {
            objInstance = objectFactory.CreateObject(type, selected.container.transform); 
        }

        return objInstance;
    }
    public void CoolObject(GameObject obj, PoolObjectType type)
    {
        obj.SetActive(false);
        obj.transform.position = defaultObjectPosition;

        PoolInfo selected = GetPoolByType(type);

        if (!selected.pool.Contains(obj))
            selected.pool.Add(obj);  
    }
    private PoolInfo GetPoolByType(PoolObjectType type)
    {
        return listOfPools.Find(pool => pool.type == type);
    }
    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }
}
