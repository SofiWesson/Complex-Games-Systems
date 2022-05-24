using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Resource Manager", menuName = "Resource Manager/Resource Manager", order = 1)]
public class ResourceManager : ScriptableObject
{
    private List<char> usableChars = new List<char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '_', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

    [SerializeField]
    [Tooltip("One of the attributes.")]
    public List<Attribute.AttributeObj> attributes = new List<Attribute.AttributeObj>();
    [SerializeField]
    [Tooltip("One of the resources.")]
    public List<Resource.ResourceObj> resources = new List<Resource.ResourceObj>();

    [Space(10)]
    [Tooltip("Create custom attributes.")]
    public Attribute.AttributeObj myCustomAttribute;
    [Tooltip("Create custom resources.")]
    public Resource.ResourceObj myCustomResource;

    [Serializable]
    public struct AttributeRemovable
    {
        public string name;
        public bool removeThis;
    }

    [Space(10)]
    [NonReorderable]
    [Tooltip("Have 'Remove This' ticked then click the 'Remove Attribute' button to remove one or more attributes")]
    public List<AttributeRemovable> removeAttribute;

    public void AddAttributes()
    {
        // checks if attribute name is empty
        if (myCustomAttribute.name == null || myCustomAttribute.name == "")
        {
            Debug.LogAssertion("Attribute name can't be empty.");
            return;
        }

        if (!IsVariableValid(myCustomAttribute.variable))
            return;

        foreach (Attribute.AttributeObj attribute in attributes)
        {
            // checks if a variable alreay has name
            if (attribute.variable.name == myCustomAttribute.variable.name)
            {
                Debug.LogAssertion("Variable already has this name.");
                return;
            }

            // checks if an attribute already has name
            if (attribute.name == myCustomAttribute.name)
            {
                Debug.LogAssertion("Attribute already has this name.");
                return;
            }
        }

        attributes.Add(myCustomAttribute);
        myCustomAttribute = new Attribute.AttributeObj();
        UpdateResourceAttributesList();

        foreach (Resource.ResourceObj resource in resources)
            resource.attributes.Add(myCustomAttribute);
    }

    public void AddResource()
    {
        // checks if resource name is empty
        if (myCustomResource.name == null || myCustomResource.name == "")
        {
            Debug.LogAssertion("Resource name can't be empty.");
            return;
        }

        foreach (Variable.VariableObj variable in myCustomResource.variables)
            if (!IsVariableValid(variable))
                return;


        // check if two variables have same name
        for (int i = 0; i < myCustomResource.variables.Count; i++)
        {
            for (int j = 0; j < myCustomResource.variables.Count; j++)
            {
                if (j != i)
                {
                    if (myCustomResource.variables[j].name == myCustomResource.variables[i].name)
                    {
                        Debug.LogAssertion("Two variables on this resource have the same name, this is not allowed.");
                        return;
                    }
                }
            }
        }

        foreach (Resource.ResourceObj resource in resources)
        {
            // check if a resource already as name
            if (myCustomResource.name == resource.name)
            {
                Debug.LogAssertion("Resource already has this name.");
                return;
            } 
        }

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
        List<AttributeRemovable> tempRemoveList = new List<AttributeRemovable>();

        foreach (Attribute.AttributeObj attribute in attributes)
        {
            Attribute.AttributeObj tempAtt = new Attribute.AttributeObj();
            tempAtt.name = attribute.name;
            tempAtt.variable = attribute.variable;
            tempAttList.Add(tempAtt);

            AttributeRemovable tempRemove = new AttributeRemovable();
            tempRemove.name = attribute.name;
            tempRemove.removeThis = false;
            tempRemoveList.Add(tempRemove);
        }

        myCustomResource.attributes = tempAttList;
        removeAttribute = tempRemoveList;
    }

    bool IsVariableValid(Variable.VariableObj variable)
    {
        // checks if variable name is empty
        if (variable.name == null || variable.name == "")
        {
            Debug.LogAssertion("Variable name can't be empty.");
            return false;
        }

        // checks if variable starts with number
        for (int i = 0; i < 10; i++)
        {
            if (variable.name[0] == usableChars[i])
            {
                Debug.LogAssertion("Variable name can't start with a number");
                return false;
            }
        }

        for (int i = 0; i < variable.name.Length;)
        {
            // checks if variable name only contains '@'
            if (variable.name.Length == 1 && variable.name == "@")
            {
                Debug.LogAssertion("When using '@' in the variable name, there needs to be more then one character.");
                return false;
            }

            // checks if variable name has '@' anywhere but the start
            if (i != 0 && variable.name[i] == '@')
            {
                Debug.LogAssertion("'@' can only be at the beginning of variable.");
                return false;
            }

            // checks if variable name has a valid character
            for (int j = 0; j < usableChars.Count; j++)
            {
                if (variable.name[i].ToString().ToLower() == usableChars[j].ToString() || variable.name[0] == '@')
                {
                    i++;
                    j = usableChars.Count;
                }
                else if (j == usableChars.Count - 1)
                {
                    Debug.LogAssertion("Variable name has invalid character");
                    return false;
                }
            }
        }

        return true;
    }

    public void ClearAll()
    {
        attributes.Clear();
        resources.Clear();
        myCustomResource.attributes.Clear();
        removeAttribute.Clear();
    }
}