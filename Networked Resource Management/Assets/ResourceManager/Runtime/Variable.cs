using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EasyResourceManager
{
    [SerializeField]
    public class Variable : MonoBehaviour
    {
        public enum VarialbeTypes
        {
            NoCustomVariable = 0,
            TypeInt,
            TypeFloat,
            TypeBool,
            TypeString
        };

        [Serializable]
        public struct VariableObj
        {
            public string name;
            public VarialbeTypes type;
            public string value;
        }
    }
}
