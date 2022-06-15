using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EasyResourceManager
{
    public class CollectionMethod : UnityEngine.Object
    {
        [Serializable]
        public struct CollectionMethodObj
        {
            public string name;
            public float countInInventory;
            public Sprite spirte;
            [NonReorderable]
            public List<Attribute.AttributeObj> attributes;
            public List<Variable.VariableObj> variables;
        }
    }
}