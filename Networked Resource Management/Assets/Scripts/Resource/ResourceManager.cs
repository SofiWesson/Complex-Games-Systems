using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Resource Manager", menuName = "Resource Manager/Resource Manager", order = 1)]
public class ResourceManager : ScriptableObject
{
    protected List<char> usableChars = new List<char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '_', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

    private List<Attribute.AttributeObj> attributesControl = new List<Attribute.AttributeObj>();
    private List<Resource.ResourceObj> resourcesControl = new List<Resource.ResourceObj>();
    
    [SerializeField]
    [Tooltip("One of the attributes.")]
    protected List<Attribute.AttributeObj> attributes = new List<Attribute.AttributeObj>();
    [SerializeField]
    [Tooltip("One of the resources.")]
    protected List<Resource.ResourceObj> resources = new List<Resource.ResourceObj>();

    [Space(10)]
    [Tooltip("Create custom attributes.")]
    [SerializeField]
    protected Attribute.AttributeObj myCustomAttribute;
    [Tooltip("Create custom resources.")]
    [SerializeField]
    protected Resource.ResourceObj myCustomResource;

    [Serializable]
    protected struct EditableAttribute
    {
        public string name;
        public Attribute.AttributeObj attribute;
        public bool editThis;
    }

    [Space(10)]
    [NonReorderable]
    [SerializeField]
    protected List<EditableAttribute> editAttribute;

    [Serializable]
    protected struct EditableResource
    {
        public string name;
        public bool editThis;
        public Resource.ResourceObj resource;
    }

    [NonReorderable]
    [SerializeField]
    protected List<EditableResource> editResource;

    [Serializable]
    protected struct Removable
    {
        public string name;
        public bool removeThis;
    }

    [Space(10)]
    [NonReorderable]
    [Tooltip("Have 'Remove This' ticked then click the 'Remove Attribute' button to remove one or more attributes")]
    [SerializeField]
    protected List<Removable> removeAttribute;
    [NonReorderable]
    [Tooltip("Have 'Remove This' ticked then click the 'Remove Resource' button to remove one or more resources")]
    [SerializeField]
    protected List<Removable> removeResource;

    // -------------------------------------------------- GETTERS --------------------------------------------------

    public List<Resource.ResourceObj> GetResources()
    {
        return resourcesControl;
    }

    public List<Attribute.AttributeObj> GetAttributes()
    {
        return attributesControl;
    }

    // -------------------------------------------------- BUTTONS --------------------------------------------------
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

        myCustomAttribute.variable.value = CorrectVariables(myCustomAttribute.variable);

        foreach (Attribute.AttributeObj attribute in attributesControl)
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

        // updates lists
        attributesControl.Add(myCustomAttribute);
        foreach (Resource.ResourceObj resource in resourcesControl)
            resource.attributes.Add(myCustomAttribute);

        ReloadLists();

        myCustomAttribute = new Attribute.AttributeObj();
    }

    public void AddResource()
    {
        // checks if resource name is empty
        if (myCustomResource.name == null || myCustomResource.name == "")
        {
            Debug.LogAssertion("Resource name can't be empty.");
            return;
        }

        for (int i = 0; i < myCustomResource.variables.Count; i++) //Variable.VariableObj variable in myCustomResource.variables)
        {
            if (!IsVariableValid(myCustomResource.variables[i]))
                return;

            Variable.VariableObj variable = myCustomResource.variables[i];
            variable.value = CorrectVariables(myCustomResource.variables[i]);
            myCustomResource.variables[i] = variable;
        }

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

        resourcesControl.Add(myCustomResource);
        ReloadLists();

        myCustomResource = new Resource.ResourceObj();
    }

    public void EditAttribute()
    {
        foreach (EditableAttribute editableAttribute in editAttribute)
        {
            for (int i = attributesControl.Count - 1; i >= 0; i--)
            {
                if (attributesControl[i].name == editableAttribute.name && editableAttribute.editThis)
                {
                    attributesControl[i] = editableAttribute.attribute;
                }
            }

            foreach (Resource.ResourceObj resource in resourcesControl)
            {
                for (int i = resource.attributes.Count - 1; i >= 0; i--)
                {
                    if (resource.attributes[i].name == editableAttribute.name && editableAttribute.editThis)
                    {
                        resource.attributes[i] = editableAttribute.attribute;
                    }
                }
            }
        }

        ReloadLists();
    }

    public void EditResource()
    {
        foreach (EditableResource editableResource in editResource)
        {
            for (int i = resourcesControl.Count - 1; i >= 0; i--)
            {
                if (resourcesControl[i].name == editableResource.name && editableResource.editThis)
                {
                    resourcesControl[i] = editableResource.resource;
                }
            }
        }

        ReloadLists();
    }

    public void RemoveAttribute()
    {
        foreach (Removable removableAttribute in removeAttribute)
        {
            for (int i = attributesControl.Count - 1; i >= 0; i--)
            {
                if (attributesControl[i].name == removableAttribute.name && removableAttribute.removeThis)
                {
                    attributesControl.Remove(attributesControl[i]);
                    continue;
                }
            }

            foreach (Resource.ResourceObj resource in resourcesControl)
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

        ReloadLists();
    }

    public void RemoveResource()
    {
        foreach (Removable removableResource in removeResource)
        {
            for (int i = resourcesControl.Count - 1; i >= 0; i--)
            {
                if (resourcesControl[i].name == removableResource.name && removableResource.removeThis)
                {
                    resourcesControl.Remove(resourcesControl[i]);
                    continue;
                }
            }
        }

        ReloadLists();
    }

    public void ReloadLists()
    {
        SyncAttributes();
        SyncResources();
        UpdateResourcesAttributesList();
        removeAttribute = UpdateRemovableList("attribute");
        removeResource = UpdateRemovableList("resource");
        UpdateEditAttributeList();
        UpdateEditResourceList();
    }

    public void ClearAll()
    {
        attributesControl.Clear();
        resourcesControl.Clear();
        attributes.Clear();
        resources.Clear();
        if (myCustomResource.attributes != null)
            myCustomResource.attributes.Clear();
        removeAttribute.Clear();
        myCustomAttribute = new Attribute.AttributeObj();
        myCustomResource = new Resource.ResourceObj();
        editAttribute.Clear();
        editResource.Clear();
    }

    // -------------------------------------------------- FUNCTIONALITY --------------------------------------------------
    void UpdateResourcesAttributesList() // Updates the list of attributes in the resources list
    {
        List<Attribute.AttributeObj> tempAttList = new List<Attribute.AttributeObj>();
       
        foreach (Attribute.AttributeObj attribute in attributesControl)
        {
            // fills the attributes list in the custom resource
            Attribute.AttributeObj tempAtt = new Attribute.AttributeObj();
            tempAtt.name = attribute.name;
            tempAtt.variable = attribute.variable;
            tempAttList.Add(tempAtt);
        }

        myCustomResource.attributes = tempAttList;
    }

    /// <summary>
    /// <paramref name="listToEdit"/> requires "attribute" or "resource"
    /// </summary>
    List<Removable> UpdateRemovableList(string listToEdit)
    {
        List<Removable> tempRemovableList = new List<Removable>();

        if (listToEdit == "attribute")
        {
            foreach (Attribute.AttributeObj attribute in attributesControl)
            {
                Removable tempRemove = new Removable();
                tempRemove.name = attribute.name;
                tempRemove.removeThis = false;
                tempRemovableList.Add(tempRemove);
            }
        }
        else if (listToEdit == "resource")
        {
            foreach (Resource.ResourceObj resource in resourcesControl)
            {
                Removable tempRemove = new Removable();
                tempRemove.name = resource.name;
                tempRemove.removeThis = false;
                tempRemovableList.Add(tempRemove);
            }
        }

        return tempRemovableList;
    }

    bool IsVariableValid(Variable.VariableObj variable)
    {
        #region Name
        // is variable name is empty
        if (variable.name == null || variable.name == "")
        {
            Debug.LogAssertion("Variable name can't be empty.");
            return false;
        }

        // does variable start with a number
        for (int i = 0; i < 10; i++)
        {
            if (variable.name[0] == usableChars[i])
            {
                Debug.LogAssertion("Variable name can't start with a number");
                return false;
            }
        }

        // checks if variable name only contains '@'
        if (variable.name.Length == 1 && variable.name == "@")
        {
            Debug.LogAssertion("When using '@' in the variable name, there needs to be more then one character.");
            return false;
        }

        for (int i = 0; i < variable.name.Length;)
        {
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
        #endregion

        #region Type
        // has a variable type been selected
        if (variable.type == Variable.VarialbeTypes.NoCustomVariable)
        {
            Debug.LogAssertion("Variable must hava a type.");
            return false;
        }
        #endregion

        #region Value
        if (variable.type == Variable.VarialbeTypes.TypeInt)
        {
            for (int i = 0; i < variable.value.Length;)
            {
                // only checks the numbers in usableChars
                for (int j = 0; j < 10; j++)
                {
                    if (variable.value[i] == usableChars[j])
                    {
                        i++;
                        j = 9;
                    }
                    else if (j == 9)
                    {
                        Debug.LogAssertion("Integers can only contain whole numbers");
                        return false;
                    }
                }
            }
        }
        else if (variable.type == Variable.VarialbeTypes.TypeFloat)
        {
            for (int i = 0; i < variable.value.Length;)
            {
                // only checks the numbers in usableChars
                for (int j = 0; j < 10; j++)
                {
                    if (variable.value[i] == usableChars[j] || variable.value[i] == '.')
                    {
                        i++;
                        j = 9;
                    }
                    else if (j == 9)
                    {
                        Debug.LogAssertion("Floats can only contain whole numbers and decimal numbers.");
                        return false;
                    }
                }
            }
        }
        else if (variable.type == Variable.VarialbeTypes.TypeBool)
        {
            if (variable.value.ToLower() != "true")
            {
                if (variable.value.ToLower() != "false")
                {
                    Debug.LogAssertion("Boolean values can only be true or false.");
                    return false;
                }
            }
        }

        if (variable.type != Variable.VarialbeTypes.TypeString)
        {
            if (variable.value == null || variable.value == "")
            {
                Debug.LogAssertion("Value can't be empty.");
                return false;
            }
        }
        #endregion

        return true;
    }

    string CorrectVariables(Variable.VariableObj variable)
    {
        if (variable.type == Variable.VarialbeTypes.TypeFloat)
        {
            if (variable.value[0] == '.')
                variable.value = "0" + variable.value;

            for (int i = 0; i < variable.value.Length; i++)
            {
                if (variable.value[i] == '.' && i == variable.value.Length - 1)
                    variable.value = variable.value + "0";
            }    
        }
        else if (variable.type == Variable.VarialbeTypes.TypeBool)
        {
            variable.value = variable.value.ToLower();
        }

        return variable.value;
    }

    void SyncAttributes()
    {
        List<Attribute.AttributeObj> temp = new List<Attribute.AttributeObj>();
        foreach (Attribute.AttributeObj attribute in attributesControl)
        {
            temp.Add(attribute);
        }
        attributes = temp;
    }

    void SyncResources()
    {
        List<Resource.ResourceObj> temp = new List<Resource.ResourceObj>();
        foreach (Resource.ResourceObj resource in resourcesControl)
        {
            temp.Add(resource);
        }    
        resources = temp;
    }

    void UpdateEditAttributeList()
    {
        editAttribute.Clear();
        foreach (Attribute.AttributeObj attributeObj in attributesControl)
        {
            EditableAttribute editableAttribute = new EditableAttribute();
            editableAttribute.name = attributeObj.name;
            editableAttribute.attribute = attributeObj;
            editableAttribute.editThis = false;
            editAttribute.Add(editableAttribute);
        }
    }

    void UpdateEditResourceList()
    {
        editResource.Clear();
        foreach (Resource.ResourceObj resourceObj in resourcesControl)
        {
            EditableResource editableResource = new EditableResource();
            editableResource.name = resourceObj.name;
            editableResource.resource = resourceObj;
            editableResource.editThis = false;
            editResource.Add(editableResource);
        }
    }
}