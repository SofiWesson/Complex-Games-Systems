using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EasyResourceManager
{
    public class Attribute : MonoBehaviour
    {
        [Serializable]
        public struct AttributeObj
        {
            public string name;
            public Variable.VariableObj variable;
        }
    }
}