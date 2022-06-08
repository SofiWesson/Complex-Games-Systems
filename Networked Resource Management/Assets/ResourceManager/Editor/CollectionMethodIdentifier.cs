using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionMethodIdentifier : MonoBehaviour
{
    [Tooltip("Collection Method Manager Scriptable Object goes here.")]
    public CollectionMethodManager m_collectionMethodManager;

    [Tooltip("Input the exact name of a collection method.")]
    public string collectionMethod = "";

    private CollectionMethod.CollectionMethodObj m_collectionMethod;

    public CollectionMethod.CollectionMethodObj GetResource()
    {
        return m_collectionMethod;
    }

    private void Awake()
    {
        List<CollectionMethod.CollectionMethodObj> collectionMethods = m_collectionMethodManager.GetCollectionMethods();

        foreach (CollectionMethod.CollectionMethodObj collectionMethodObj in collectionMethods)
        {
            if (collectionMethodObj.name == collectionMethod)
                m_collectionMethod = collectionMethodObj;
        }
    }
}