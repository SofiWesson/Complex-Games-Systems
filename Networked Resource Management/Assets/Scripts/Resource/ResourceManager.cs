using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

[CreateAssetMenu(fileName = "Resource Manager", menuName = "Resource Manager/Resource Manager", order = 1)]
public class ResourceManager : ScriptableObject
{
    private Resource m_resource;
    private Attributes m_attributes;

    [SerializeField]
    protected List<Resource> resources = new List<Resource>();
    [SerializeField]
    protected List<Attributes> attributes = new List<Attributes>();

    [Serializable]
    public struct CustomVariable
    {
        [Tooltip("The type of variable your custom variable will be.")]
        public Variables.VarialbeTypes variableType;
        [Tooltip("The value of your custom variable.\nMake sure the value corrolates to the variable type selected otherwise the resource won't save.\nDon't include the f at the end for floats.")]
        public string variableValue;
    }

    [Serializable]
    public struct Attribute
    {
        [Tooltip("The type of variable the attribute is.")]
        public string variableType;
        [Tooltip("The value of the variable.")]
        public string variableValue;
    }

    [Space(10)]
    [Header("Make Attribute")]

    public int i = 0;

    [Space(10)]
    [Header("Make Resource")]

    [Tooltip("The name of the resource.")]
    public string name = "";
    [Tooltip("The sprite that can be used to represent a resource.\nIf you're not using this, leave it blank.")]
    public Sprite sprite = null;
    [Tooltip("List of customisable variables for your resource.\nYour resource doesn't need custom variables, if your not going to have any leave this empty.")]
    public List<CustomVariable> customVariables = new List<CustomVariable>();
    [Tooltip("List of the attributes that your resource will abide by.")]
    public List<Attribute> resourceAttributes;

    // resourceAttributes length != attributes length ? resourceAttributes = attributes->Attribute

    // make button in inspector to save resource to resource list 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddResource(Resource a_resource)
    {
        resources.Add(a_resource);
    }

    private void RemoveResource(Resource a_resource)
    {
        resources.Remove(a_resource);
    }

    private void AddAttribute(Attributes a_attribute)
    {
        attributes.Add(a_attribute);
    }

    private void RemoveAttribute(Attributes a_attribute)
    {
        attributes.Remove(a_attribute);
    }
}
