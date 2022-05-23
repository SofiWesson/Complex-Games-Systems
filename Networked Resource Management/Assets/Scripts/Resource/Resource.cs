using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Resource : MonoBehaviour
{
    [Serializable]
    public struct ResourceObj
    {
        public string name;
        public int countInInventory;
        public Sprite spirte;
        [NonReorderable]
        public List<Variable.VariableObj> variables;
        [NonReorderable]
        public List<Attribute.AttributeObj> attributes;
    }
}
