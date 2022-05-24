using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Resource Manager", menuName = "Resource Manager/Resource Manager", order = 1)]
public class ResourceManager : ScriptableObject
{
    [SerializeField]
    public List<Attribute.AttributeObj> attributes = new List<Attribute.AttributeObj>();
    [SerializeField]
    public List<Resource.ResourceObj> resources = new List<Resource.ResourceObj>();
    
    [Space(10)]
    public Attribute.AttributeObj myCustomAttribute;
    public Resource.ResourceObj myCustomResource;

    [Serializable]
    public struct AttributeRemovable
    {
        [NonReorderable] public string name;
        public bool removeThis;
    }

    [Space(10)]
    [NonReorderable]
    public List<AttributeRemovable> removeAttribute;

    public void AddAttributes()
    {
        attributes.Add(myCustomAttribute);
        myCustomAttribute = new Attribute.AttributeObj();
        UpdateResourceAttributesList();
        // needs to add to resources
    }

    public void AddResource()
    {
        resources.Add(myCustomResource);
        myCustomResource = new Resource.ResourceObj();
        UpdateResourceAttributesList();
    }

    public void RemoveAttribute()
    {
        foreach (AttributeRemovable removableAttribute in removeAttribute)
        {
            for (int i = attributes.Count - 1; i >= 0; i--) //Attribute.AttributeObj attribute in attributes)
            {
                if (attributes[i].name == removableAttribute.name && removableAttribute.removeThis)
                {
                    attributes.Remove(attributes[i]);
                    continue;
                }
            }

            foreach (Resource.ResourceObj resource in resources)
            {
                for (int i = resource.attributes.Count - 1; i >= 0; i--)
                {
                    if (resource.attributes[i].name == removableAttribute.name && removableAttribute.removeThis)
                    {
                        resource.attributes.Remove(resource.attributes[i]);
                        continue;
                    }
                }
            }
        }

        UpdateResourceAttributesList();
    }

    void UpdateResourceAttributesList()
    {
        List<Attribute.AttributeObj> tempAttList = new List<Attribute.AttributeObj>();
        List<AttributeRemovable> tempRemList = new List<AttributeRemovable>();
        foreach (Attribute.AttributeObj attribute in attributes)
        {
            Attribute.AttributeObj tempAtt = new Attribute.AttributeObj();
            tempAtt.name = attribute.name;
            tempAtt.variable = attribute.variable;
            tempAttList.Add(tempAtt);

            AttributeRemovable tempRem = new AttributeRemovable();
            tempRem.name = attribute.name;
            tempRem.removeThis = false;
            tempRemList.Add(tempRem);
        }
        myCustomResource.attributes = tempAttList;
        removeAttribute = tempRemList;
    }

    public void ClearAll()
    {
        attributes.Clear();
        resources.Clear();
        myCustomResource.attributes.Clear();
        removeAttribute.Clear();
    }
}