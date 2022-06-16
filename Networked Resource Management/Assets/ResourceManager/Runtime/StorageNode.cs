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

        // used in functions for comparasion
        private Resource.ResourceObj m_emptyResouce = new Resource.ResourceObj();
        private CollectionMethod.CollectionMethodObj m_emptyCollectionMethod = new CollectionMethod.CollectionMethodObj();
        
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

        public List<Resource.ResourceObj> GetResources()
        {
            return resources;
        }

        public List<CollectionMethod.CollectionMethodObj> GetCollectionMethods()
        {
            return collectionMethods;
        }

        public Tuple<List<Resource.ResourceObj>, List<CollectionMethod.CollectionMethodObj>> GetContents()
        {
            Tuple<List<Resource.ResourceObj>, List<CollectionMethod.CollectionMethodObj>> tuple =
                new Tuple<List<Resource.ResourceObj>, List<CollectionMethod.CollectionMethodObj>>(resources, collectionMethods);

            return tuple;
        }

        /// <summary>
        /// Returns -1 if no item found.
        /// </summary>
        /// <returns></returns>
        [ClientCallback]
        public float GetItemAmount(string itemName)
        {
            float returnAmount = -1;

            CompareFunctions compare = new CompareFunctions();

            Resource.ResourceObj resource = GetResource(itemName);
            CollectionMethod.CollectionMethodObj collectionMethod = GetCollectionMethod(itemName);

            if (!compare.CompareResources(resource, m_emptyResouce))
                returnAmount = resource.countInInventory;
            else if (!compare.CompareCollectionMethods(collectionMethod, m_emptyCollectionMethod))
                returnAmount = collectionMethod.countInInventory;

            return returnAmount;
        }

        [ClientCallback]
        public void AddItemAmount(string itemName, float amount)
        {
            CompareFunctions compare = new CompareFunctions();

            Resource.ResourceObj resource = GetResource(itemName);
            CollectionMethod.CollectionMethodObj collectionMethod = GetCollectionMethod(itemName);

            if (!compare.CompareResources(resource, m_emptyResouce))
            {
                resource.countInInventory += amount;

                for (int i = 0; i < resources.Count; i++)
                {
                    if (resources[i].name == resource.name)
                    {
                        resources[i] = resource;
                    }
                }
            }
            else if (!compare.CompareCollectionMethods(collectionMethod, m_emptyCollectionMethod))
            {
                collectionMethod.countInInventory += amount;

                for (int i = 0; i < collectionMethods.Count; i++)
                {
                    if (collectionMethods[i].name == collectionMethod.name)
                    {
                        collectionMethods[i] = collectionMethod;
                    }
                }
            }
        }
    }
}