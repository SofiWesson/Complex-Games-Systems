using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Resource Manager", menuName = "Resource Manager/Resource Manager", order = 1)]
public class ResourceManager : ScriptableObject
{
    [SerializeField]
    protected List<Attribute> attributes = new List<Attribute>();
    [SerializeField]
    protected List<Resource> resources = new List<Resource>();

    [Serializable]
    public struct LocalVariable
    {
        [Tooltip("The name of your custom variable.")]
        public string variableName;
        [Tooltip("The type of variable your custom variable will be.")]
        public Variable.VarialbeTypes variableType;
        [Tooltip("The value of your custom variable.\nMake sure the value corrolates to the variable type selected otherwise the resource won't save.\nDon't include the f at the end for floats.")]
        public string variableValue;
    }

    [Space(10)]
    [Header("Make Attribute")]

    public string attributeName = "";
    public LocalVariable customAttributeVariable;

    [Space(10)]
    [Header("Make Resource")]

    [Tooltip("The name of the resource.")]
    public string resourceName = "";
    [Tooltip("The sprite that can be used to represent a resource.\nIf you're not using this, leave it blank.")]
    public Sprite resourceSprite = null;
    [Tooltip("List of variables for your resource.\nYour resource doesn't need custom variables, if your not going to have any leave this empty.")]
    public List<LocalVariable> customResourceVariables = new List<LocalVariable>();
    [Tooltip("List of the attributes that your resource will abide by.")]
    public List<Attribute> resourceAttributes = new List<Attribute>();

    public void AddAttributes()
    {
        Variable variable = new Variable();
        variable.name = customAttributeVariable.variableName;
        variable.type = customAttributeVariable.variableType;
        variable.value = customAttributeVariable.variableValue;

        Attribute attribute = new Attribute();
        attribute.name = attributeName;
        attribute.variable = variable;

        attributes.Add(attribute);

        Debug.Log("Attribute Added");

        attributeName = "";
        customAttributeVariable = new LocalVariable();
    }

    public void AddResource()
    {
        Resource resource = new Resource();
        resource.name = resourceName;
        resource.countInInventory = 0;
        resource.spirte = resourceSprite;

        for (int i = 0; i < customResourceVariables.Count; i++)
        {
            Variable variable = new Variable();
            variable.name = customResourceVariables[i].variableName;
            variable.type = customResourceVariables[i].variableType;
            variable.value = customResourceVariables[i].variableValue;

            resource.variables.Add(variable);
        }

        resource.attributes = null;

        resources.Add(resource);

        Debug.Log("Resource Added");

        resourceName = "";
        resourceSprite = null;
        customResourceVariables.Clear();
        resourceAttributes = attributes;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}