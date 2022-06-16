using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EasyResourceManager
{
    public class CompareFunctions
    {
        public bool CompareResources(Resource.ResourceObj resourceObj1, Resource.ResourceObj resourceObj2)
        {
            return resourceObj1.name == resourceObj2.name ? 
                resourceObj1.countInInventory == resourceObj2.countInInventory ? 
                    resourceObj1.spirte == resourceObj2.spirte ? 
                        resourceObj1.attributes == resourceObj2.attributes ? 
                            resourceObj1.variables == resourceObj2.variables ? true : false
                        : false
                    : false
                : false
            : false;
        }

        public bool CompareCollectionMethods(CollectionMethod.CollectionMethodObj collectionObj1, CollectionMethod.CollectionMethodObj collectionObj2)
        {
            return collectionObj1.name == collectionObj2.name ?
                collectionObj1.countInInventory == collectionObj2.countInInventory ?
                    collectionObj1.spirte == collectionObj2.spirte ?
                        collectionObj1.attributes == collectionObj2.attributes ?
                            collectionObj1.variables == collectionObj2.variables ? true : false
                        : false
                    : false
                : false
            : false;
        }
    }
}