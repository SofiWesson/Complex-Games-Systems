using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StorageNode : MonoBehaviour
{
    protected Dictionary<int, Resource.ResourceObj> resources = new Dictionary<int, Resource.ResourceObj>();
    protected Dictionary<int, CollectionMethod.CollectionMethodObj> collectionMethods = new Dictionary<int, CollectionMethod.CollectionMethodObj>();

    private ResourceManager m_resourceManager;
    private CollectionMethodManager m_collectionMethodManager;

    private void Awake()
    {
        List<Resource.ResourceObj> resourceObjs = new List<Resource.ResourceObj>();
        resourceObjs = m_resourceManager.GetResources();

        int id = 0;

        foreach (Resource.ResourceObj resourceObj in resourceObjs)
        {
            resources.Add(id, resourceObj);
            id++;
        }

        id = 0;

        List<CollectionMethod.CollectionMethodObj> collectionMethodObjs = new List<CollectionMethod.CollectionMethodObj>();
        collectionMethodObjs = m_collectionMethodManager.GetCollectionMethods();

        foreach (CollectionMethod.CollectionMethodObj collectionMethodObj in collectionMethodObjs)
        {
            collectionMethods.Add(id, collectionMethodObj);
            id++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Resource GetItem(Resource listToSearch, string nameOfItem)
    {
        return listToSearch;
    }

    public CollectionMethod GetItem(CollectionMethod listToSearch, string nameOfItem)
    {
        return listToSearch;
    }

    public float GetItemAmount(Resource item)
    {
        return 0.0f;
    }

    public float GetItemAmount(CollectionMethod item)
    {
        return 0.0f;
    }

    public void SetItemAmount(Resource item, float amount)
    {

    }

    public void SetItemAmount(CollectionMethod item, float amount)
    {

    }
}
