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

    protected Test resourceLock;
    public struct Test
    {
        public List<Resource.ResourceObj> test;
    }
    
    [Space(10)]
    public Attribute.AttributeObj myCustomAttribute;
    public Resource.ResourceObj myCustomResource;

    [Space(10)]
    public Attribute.AttributeObj attributeToRemove;
  
    public void AddAttributes()
    {
        attributes.Add(myCustomAttribute);
        myCustomAttribute = new Attribute.AttributeObj();

        List<Attribute.AttributeObj> tempList = new List<Attribute.AttributeObj>();
        foreach (Attribute.AttributeObj attribute in attributes)
        {
            Attribute.AttributeObj tempAtt = new Attribute.AttributeObj();
            tempAtt.name = attribute.name;
            tempAtt.variable = attribute.variable;
            tempList.Add(tempAtt);
        }
        myCustomResource.attributes = tempList;
    }

    public void AddResource()
    {
        resources.Add(myCustomResource);
        myCustomResource = new Resource.ResourceObj();

        List<Attribute.AttributeObj> tempList = new List<Attribute.AttributeObj>();
        foreach (Attribute.AttributeObj attribute in attributes)
        {
            Attribute.AttributeObj tempAtt = new Attribute.AttributeObj();
            tempAtt.name = attribute.name;
            tempAtt.variable = attribute.variable;
            tempList.Add(tempAtt);
        }
        myCustomResource.attributes = tempList;
    }

    public void RemoveAttribute()
    {
        attributes.Remove(attributeToRemove);
        attributeToRemove = new Attribute.AttributeObj();
    }

    void Start()
    {
        
    }

    [ExecuteInEditMode]
    void Update()
    {
        
    }

    public void ClearAll()
    {
        attributes.Clear();
        resources.Clear();
    }
}