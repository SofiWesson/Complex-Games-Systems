using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;

public class StorageNode : NetworkBehaviour
{
    protected Dictionary<int, Resource.ResourceObj> resources = new Dictionary<int, Resource.ResourceObj>();
    protected Dictionary<int, CollectionMethod.CollectionMethodObj> collectionMethods = new Dictionary<int, CollectionMethod.CollectionMethodObj>();

    public ResourceManager m_resourceManager;
    public CollectionMethodManager m_collectionMethodManager;

    private void Awake()
    {
        List<Resource.ResourceObj> resourceObjs = new List<Resource.ResourceObj>();
        resourceObjs = m_resourceManager.GetResources();

        int id = 0;
        if (resourceObjs.Count > 0)
        {
            foreach (Resource.ResourceObj resourceObj in resourceObjs)
            {
                resources.Add(id, resourceObj);
                id++;
            }
        }

        id = 0;

        List<CollectionMethod.CollectionMethodObj> collectionMethodObjs = new List<CollectionMethod.CollectionMethodObj>();

        if (m_collectionMethodManager != null)
            collectionMethodObjs = m_collectionMethodManager.GetCollectionMethods();

        if (collectionMethodObjs.Count > 0)
        {
            foreach (CollectionMethod.CollectionMethodObj collectionMethodObj in collectionMethodObjs)
            {
                collectionMethods.Add(id, collectionMethodObj);
                id++;
            }
        }
    }

    [ClientCallback]
    public float GetItemAmount(Resource.ResourceObj item)
    {
        return item.countInInventory;
    }

    [ClientCallback]
    public float GetItemAmount(CollectionMethod.CollectionMethodObj item)
    {
        return item.countInInventory;
    }

    [ClientCallback]
    public Dictionary<int, Resource.ResourceObj> GetResources()
    {
        return resources;
    }

    public Dictionary<int, CollectionMethod.CollectionMethodObj> GetCollectionMethods()
    {
        return collectionMethods;
    }

    [Command]
    public void SetItemAmount(Resource.ResourceObj item, float amount)
    {
        item.countInInventory = amount;
    }

    [Command]
    public void SetItemAmount(CollectionMethod.CollectionMethodObj item, float amount)
    {
        item.countInInventory = amount;
    }
}