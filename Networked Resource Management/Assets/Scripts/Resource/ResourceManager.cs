using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Resource Manager", menuName = "Resource Manager/Resource Manager", order = 1)]
public class ResourceManager : ScriptableObject
{
    [SerializeField]
    protected List<Attribute.AttributeObj> attributes = new List<Attribute.AttributeObj>();
    [SerializeField]
    protected List<Resource.ResourceObj> resources = new List<Resource.ResourceObj>();

    public List<LocalAttribute> viewAttributes = new List<LocalAttribute>();
    public List<LocalResource> viewResources = new List<LocalResource>();

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

    [Serializable]
    public struct LocalAttribute
    {
        public string attributeName;
        public LocalVariable attributeVariable;
    }

    [Serializable]
    public struct LocalResource
    {
        public string name;
        public int countInInventory;
        public Sprite spirte;
        [NonReorderable]
        public List<LocalVariable> variables;
        [NonReorderable]
        public List<LocalAttribute> attributes;
    }

    [Space(10)]
    [Header("MAKE ATTRIBUTE")]

    public string attributeName = "";
    public LocalVariable customAttributeVariable;

    [Space(10)]
    [Header("MAKE RESOURCE")]

    [Tooltip("The name of the resource.")]
    public string resourceName = "";
    [Tooltip("The sprite that can be used to represent a resource.\nIf you're not using this, leave it blank.")]
    public Sprite resourceSprite = null;
    [Tooltip("List of variables for your resource.\nYour resource doesn't need custom variables, if your not going to have any leave this empty.")]
    public List<LocalVariable> customResourceVariables = new List<LocalVariable>();
    [Tooltip("List of the attributes that your resource will abide by.")]
    public List<Attribute.AttributeObj> resourceAttributes = new List<Attribute.AttributeObj>();
  
    public void AddAttributes()
    {
        Variable.VariableObj variable = new Variable.VariableObj();
        variable.name = customAttributeVariable.variableName;
        variable.type = customAttributeVariable.variableType;
        variable.value = customAttributeVariable.variableValue;

        Attribute.AttributeObj attribute = new Attribute.AttributeObj();
        attribute.name = attributeName;
        attribute.variable = variable;

        attributes.Add(attribute);

        Debug.Log("Attribute Added");

        EditViewableAttribute();

        attributeName = "";
        customAttributeVariable = new LocalVariable();
    }

    void EditViewableAttribute()
    {
        viewAttributes.Clear();

        foreach (Attribute.AttributeObj attribute in attributes)
        {
            LocalAttribute temp = new LocalAttribute();

            temp.attributeName = attribute.name;
            temp.attributeVariable.variableName = attribute.variable.name;
            temp.attributeVariable.variableType = attribute.variable.type;
            temp.attributeVariable.variableValue = attribute.variable.value;

            viewAttributes.Add(temp);
        }
    }

    public void AddResource()
    {
        Resource.ResourceObj resource = new Resource.ResourceObj();
        resource.name = resourceName;
        resource.countInInventory = 0;
        resource.spirte = resourceSprite;

        resource.variables = new List<Variable.VariableObj>();
        for (int i = 0; i < customResourceVariables.Count; i++)
        {
            Variable.VariableObj variable = new Variable.VariableObj();
            variable.name = customResourceVariables[i].variableName;
            variable.type = customResourceVariables[i].variableType;
            variable.value = customResourceVariables[i].variableValue;

            resource.variables.Add(variable);
        }

        resource.attributes = new List<Attribute.AttributeObj>();
        for (int i = 0; i < resourceAttributes.Count; i++)
        {
            Attribute.AttributeObj attribute = new Attribute.AttributeObj();
            attribute.name = resourceAttributes[i].name;
            attribute.variable.name = resourceAttributes[i].variable.name;
            attribute.variable.type = resourceAttributes[i].variable.type;
            attribute.variable.value = resourceAttributes[i].variable.value;

            resource.attributes.Add(attribute);
        }

        resources.Add(resource);

        Debug.Log("Resource Added");

        EditViewableResources();

        resourceName = "";
        resourceSprite = null;
        customResourceVariables.Clear();
        resourceAttributes = attributes;
    }

    void EditViewableResources()
    {
        viewResources.Clear();

        foreach (Resource.ResourceObj resource in resources)
        {
            LocalResource temp = new LocalResource();

            temp.name = resource.name;
            temp.countInInventory = 0;
            temp.spirte = resource.spirte;

            temp.variables = new List<LocalVariable>();
            foreach (Variable.VariableObj variable in resource.variables)
            {
                LocalVariable tempVar = new LocalVariable();
                tempVar.variableName = variable.name;
                tempVar.variableType = variable.type;
                tempVar.variableValue = variable.value;

                temp.variables.Add(tempVar);
            }

            temp.attributes = new List<LocalAttribute>();
            foreach (Attribute.AttributeObj attribute in resource.attributes)
            {
                LocalAttribute tempAtt = new LocalAttribute();
                tempAtt.attributeName = attribute.name;
                tempAtt.attributeVariable.variableName = attribute.variable.name;
                tempAtt.attributeVariable.variableType = attribute.variable.type;
                tempAtt.attributeVariable.variableValue = attribute.variable.value;
            
                temp.attributes.Add(tempAtt);
            }

            viewResources.Add(temp);
        }
    }

    // make lists non editable

    // Start is called before the first frame update
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
        viewAttributes.Clear();
        viewResources.Clear();
    }
}