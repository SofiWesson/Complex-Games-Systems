using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ResourceIdentifier : NetworkBehaviour
{
    [Tooltip("Resource Manager Scriptable Object goes here.")]
    public ResourcesManager m_resourceManager;

    [Tooltip("Input the exact name of a resource.")]
    public string resource = "";

    private Resource.ResourceObj m_resource;

    public Resource.ResourceObj GetResource()
    {
        return m_resource;
    }

    private void Awake()
    {
        List<Resource.ResourceObj> resources = m_resourceManager.GetResources();
        
        foreach (Resource.ResourceObj resourceObj in resources)
        {
            if (resourceObj.name == resource)
                m_resource = resourceObj;
        }
    }
}