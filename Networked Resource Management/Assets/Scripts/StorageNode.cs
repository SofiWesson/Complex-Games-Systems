using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StorageNode : MonoBehaviour
{
    protected Dictionary<Resource, int> resources = new Dictionary<Resource, int>();
    protected Dictionary<CollectionMethod, int> collectionMethods = new Dictionary<CollectionMethod, int>();

    private Resource m_resource;
    private CollectionMethod m_collectionMethod;

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
