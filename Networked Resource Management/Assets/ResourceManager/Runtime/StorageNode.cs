using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace EasyResourceManager
{
    public class StorageNode : NetworkBehaviour
    {
        private List<Resource.ResourceObj> resources = new List<Resource.ResourceObj>();
        private List<CollectionMethod.CollectionMethodObj> collectionMethods = new List<CollectionMethod.CollectionMethodObj>();

        public ResourcesManager m_resourceManager;
        public CollectionMethodManager m_collectionMethodManager;

        private void Awake()
        {
            List<Resource.ResourceObj> resourceObjs = new List<Resource.ResourceObj>();
            resourceObjs = m_resourceManager.GetResources();

            if (resourceObjs.Count > 0)
            {
                foreach (Resource.ResourceObj resourceObj in resourceObjs)
                {
                    resources.Add(resourceObj);
                }
            }

            List<CollectionMethod.CollectionMethodObj> collectionMethodObjs = new List<CollectionMethod.CollectionMethodObj>();

            if (m_collectionMethodManager != null)
                collectionMethodObjs = m_collectionMethodManager.GetCollectionMethods();

            if (collectionMethodObjs.Count > 0)
            {
                foreach (CollectionMethod.CollectionMethodObj collectionMethodObj in collectionMethodObjs)
                {
                    collectionMethods.Add(collectionMethodObj);
                }
            }
        }

        [ServerCallback]
        private Resource.ResourceObj GetResource(string resourceName)
        {
            Resource.ResourceObj resourceObj = new Resource.ResourceObj();

            foreach (Resource.ResourceObj resource in resources)
            {
                if (resource.name == resourceName)
                {
                    resourceObj = resource;
                    continue;
                }
            }

            return resourceObj;
        }

        [ServerCallback]
        private CollectionMethod.CollectionMethodObj GetCollectionMethod(string collectionName)
        {
            CollectionMethod.CollectionMethodObj collectionObj = new CollectionMethod.CollectionMethodObj();

            foreach (CollectionMethod.CollectionMethodObj collectionMethod in collectionMethods)
            {
                if (collectionObj.name == collectionName)
                {
                    collectionObj = collectionMethod;
                    continue;
                }
            }

            return collectionObj;
        }

        [ClientCallback]
        public float GetItemAmount(string itemName)
        {
            float returnAmount = -1;

            CompareFunctions compare = new CompareFunctions();

            Resource.ResourceObj emptyResouce = new Resource.ResourceObj();
            CollectionMethod.CollectionMethodObj emptyCollectionMethod = new CollectionMethod.CollectionMethodObj();

            Resource.ResourceObj resource = GetResource(itemName);
            CollectionMethod.CollectionMethodObj collectionMethod = GetCollectionMethod(itemName);

            if (!compare.CompareResources(resource, emptyResouce))
                returnAmount = resource.countInInventory;
            else if (!compare.CompareCollectionMethods(collectionMethod, emptyCollectionMethod))
                returnAmount = collectionMethod.countInInventory;

            return returnAmount;
        }

        //[ClientCallback]
        // public Dictionary<int, Resource.ResourceObj> GetResources()
        // {
        //     return resources;
        // }
        // 
        // public Dictionary<int, CollectionMethod.CollectionMethodObj> GetCollectionMethods()
        // {
        //     return collectionMethods;
        // }

        [Command]
        public void SetItemAmount(string itemName, float amount) // make work with CollectionMethod
        {
            Resource.ResourceObj resourceObj = GetResource(itemName);
            resourceObj.countInInventory = amount;

            for (int i = 0; i < resources.Count; i++)
            {
                if (resources[i].name == resourceObj.name)
                {
                    resources[i] = resourceObj;
                }
            }
        }

        [Command]
        public void SetItemAmount(CollectionMethod.CollectionMethodObj item, float amount)
        {
            item.countInInventory = amount;
        }

        [Command]
        public void AddItemAmount(Resource.ResourceObj item, float amount)
        {

        }

        [Command]
        public void AddItemAmount(CollectionMethod.CollectionMethodObj item, float amount)
        {

        }
    }
}