using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Attribute : MonoBehaviour
{
    [Serializable]
    public struct AttributeObj
    {
        public string name;
        public Variable.VariableObj variable;
    }
}