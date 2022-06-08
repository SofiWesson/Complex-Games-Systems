using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;

[CreateAssetMenu(fileName = "Collection Method Manager", menuName = "Resource Manager/Collection Method Manager", order = 2)]
public class CollectionMethodManager : ScriptableObject
{
    private List<char> usableChars = new List<char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '_', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

    protected List<Attribute.AttributeObj> attributesControl = new List<Attribute.AttributeObj>();
    protected List<CollectionMethod.CollectionMethodObj> collectionMethodControl = new List<CollectionMethod.CollectionMethodObj>();

    [SerializeField]
    [Tooltip("One of the attributes.")]
    private List<Attribute.AttributeObj> attributes = new List<Attribute.AttributeObj>();
    [SerializeField]
    [Tooltip("One of the collection methods.")]
    private List<CollectionMethod.CollectionMethodObj> collectionMethod = new List<CollectionMethod.CollectionMethodObj>();

    [Space(10)]
    [Tooltip("Create custom attributes.")]
    [SerializeField]
    private Attribute.AttributeObj myCustomAttribute;
    [Tooltip("Create custom collection method.")]
    [SerializeField]
    private CollectionMethod.CollectionMethodObj myCustomCollectionMethod;

    [Serializable]
    private struct EditableAttribute
    {
        public string name;
        public Attribute.AttributeObj attribute;
        public bool editThis;
    }

    [Space(10)]
    [NonReorderable]
    [SerializeField]
    private List<EditableAttribute> editAttribute;

    [Serializable]
    private struct EditableCollectionMethod
    {
        public string name;
        public bool editThis;
        public CollectionMethod.CollectionMethodObj collectionMethod;
    }

    [NonReorderable]
    [SerializeField]
    private List<EditableCollectionMethod> editCollectionMethod;

    [Serializable]
    private struct Removable
    {
        public string name;
        public bool removeThis;
    }

    [Space(10)]
    [NonReorderable]
    [Tooltip("Have 'Remove This' ticked then click the 'Remove Attribute' button to remove one or more attributes")]
    [SerializeField]
    private List<Removable> removeAttribute;
    [NonReorderable]
    [Tooltip("Have 'Remove This' ticked then click the 'Remove Collection Method' button to remove one or more collection methods")]
    [SerializeField]
    private List<Removable> removeCollectionMethod;

    [Command]
    public List<CollectionMethod.CollectionMethodObj> GetCollectionMethods()
    {
        return collectionMethodControl;
    }

    [Command]
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
        foreach (CollectionMethod.CollectionMethodObj collectionMethod in collectionMethodControl)
            collectionMethod.attributes.Add(myCustomAttribute);

        ReloadLists();

        myCustomAttribute = new Attribute.AttributeObj();
    }

    public void AddCollectionMethod()
    {
        // checks if resource name is empty
        if (myCustomCollectionMethod.name == null || myCustomCollectionMethod.name == "")
        {
            Debug.LogAssertion("Collection Method name can't be empty.");
            return;
        }

        for (int i = 0; i < myCustomCollectionMethod.variables.Count; i++)
        {
            if (!IsVariableValid(myCustomCollectionMethod.variables[i]))
                return;

            Variable.VariableObj variable = myCustomCollectionMethod.variables[i];
            variable.value = CorrectVariables(myCustomCollectionMethod.variables[i]);
            myCustomCollectionMethod.variables[i] = variable;
        }

        // check if two variables have same name
        for (int i = 0; i < myCustomCollectionMethod.variables.Count; i++)
        {
            for (int j = 0; j < myCustomCollectionMethod.variables.Count; j++)
            {
                if (j != i)
                {
                    if (myCustomCollectionMethod.variables[j].name == myCustomCollectionMethod.variables[i].name)
                    {
                        Debug.LogAssertion("Two variables on this resource have the same name, this is not allowed.");
                        return;
                    }
                }
            }
        }

        foreach (CollectionMethod.CollectionMethodObj collectionMethod in collectionMethod)
        {
            // check if a resource already as name
            if (myCustomCollectionMethod.name == collectionMethod.name)
            {
                Debug.LogAssertion("Collection Method already has this name.");
                return;
            }
        }

        collectionMethodControl.Add(myCustomCollectionMethod);
        ReloadLists();

        myCustomCollectionMethod = new CollectionMethod.CollectionMethodObj();
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

            foreach (CollectionMethod.CollectionMethodObj collectionMethod in collectionMethodControl)
            {
                for (int i = collectionMethod.attributes.Count - 1; i >= 0; i--)
                {
                    if (collectionMethod.attributes[i].name == editableAttribute.name && editableAttribute.editThis)
                    {
                        collectionMethod.attributes[i] = editableAttribute.attribute;
                    }
                }
            }
        }

        ReloadLists();
    }

    public void EditCollectionMethod()
    {
        foreach (EditableCollectionMethod editableCollectionMethod in editCollectionMethod)
        {
            for (int i = collectionMethodControl.Count - 1; i >= 0; i--)
            {
                CollectionMethod.CollectionMethodObj collectionMethodObj = new CollectionMethod.CollectionMethodObj();

                if (collectionMethodControl[i].name == editableCollectionMethod.name && editableCollectionMethod.editThis)
                {
                    collectionMethodObj = editableCollectionMethod.collectionMethod;
                    collectionMethodControl[i] = collectionMethodObj;
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

            foreach (CollectionMethod.CollectionMethodObj collectionMethod in collectionMethodControl)
            {
                for (int i = collectionMethod.attributes.Count - 1; i >= 0; i--)
                {
                    if (collectionMethod.attributes[i].name == removableAttribute.name && removableAttribute.removeThis)
                    {
                        collectionMethod.attributes.Remove(collectionMethod.attributes[i]);
                        continue;
                    }
                }
            }
        }

        ReloadLists();
    }

    public void RemoveCollectionMethod()
    {
        foreach (Removable removableCollectionMethod in removeCollectionMethod)
        {
            for (int i = collectionMethodControl.Count - 1; i >= 0; i--)
            {
                if (collectionMethodControl[i].name == removableCollectionMethod.name && removableCollectionMethod.removeThis)
                {
                    collectionMethodControl.Remove(collectionMethodControl[i]);
                    continue;
                }
            }
        }

        ReloadLists();
    }

    public void ReloadLists()
    {
        SyncAttributes();
        SyncCollectionMethods();
        UpdateCollectionMethodsAttributesList();
        removeAttribute = UpdateRemovableList("attribute");
        removeCollectionMethod = UpdateRemovableList("collection");
        UpdateEditAttributeList();
        UpdateEditCollectionMethodList();
    }

    public void ClearAll()
    {
        attributesControl.Clear();
        collectionMethodControl.Clear();
        attributes.Clear();
        collectionMethod.Clear();
        if (myCustomCollectionMethod.attributes != null)
            myCustomCollectionMethod.attributes.Clear();
        removeAttribute.Clear();
        myCustomAttribute = new Attribute.AttributeObj();
        myCustomCollectionMethod = new CollectionMethod.CollectionMethodObj();
        editAttribute.Clear();
        editCollectionMethod.Clear();
    }

    // -------------------------------------------------- FUNCTIONALITY --------------------------------------------------
    void UpdateCollectionMethodsAttributesList() // Updates the list of attributes in the resources list
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

        myCustomCollectionMethod.attributes = tempAttList;
    }

    /// <summary>
    /// <paramref name="listToEdit"/> requires "attribute" or "collection"
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
        else if (listToEdit == "collection")
        {
            foreach (CollectionMethod.CollectionMethodObj collectionMethod in collectionMethodControl)
            {
                Removable tempRemove = new Removable();
                tempRemove.name = collectionMethod.name;
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

    void SyncCollectionMethods()
    {
        List<CollectionMethod.CollectionMethodObj> temp = new List<CollectionMethod.CollectionMethodObj>();
        foreach (CollectionMethod.CollectionMethodObj collectionMethod in collectionMethodControl)
        {
            temp.Add(collectionMethod);
        }
        collectionMethod = temp;
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

    void UpdateEditCollectionMethodList()
    {
        editCollectionMethod.Clear();
        foreach (CollectionMethod.CollectionMethodObj collectionMethod in collectionMethodControl)
        {
            EditableCollectionMethod editableCollectionMethod = new EditableCollectionMethod();
            editableCollectionMethod.name = collectionMethod.name;
            editableCollectionMethod.collectionMethod = collectionMethod;
            editableCollectionMethod.editThis = false;
            editCollectionMethod.Add(editableCollectionMethod);
        }
    }
}