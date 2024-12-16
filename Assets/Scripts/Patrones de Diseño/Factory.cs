using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private List<FactoryItem> factoryItems;

    private Dictionary<PoolObjectType, GameObject> prefabDictionary;

    private void Awake()
    {
        prefabDictionary = new Dictionary<PoolObjectType, GameObject>();
        foreach (var item in factoryItems)
        {
            if(!prefabDictionary.ContainsKey(item.type))
            {
                prefabDictionary.Add(item.type, item.prefab);
            }
        }
    }

    public GameObject CreateObject(PoolObjectType type, Transform parent = null)
    {
        if(prefabDictionary.TryGetValue(type, out GameObject prefab)) 
        {
            return Instantiate(prefab, parent);
        }

        Debug.LogWarning($"No prefab found for type {type}");
        return null;
    }
}

[System.Serializable]
public class FactoryItem
{
    public PoolObjectType type;
    public GameObject prefab;
}
