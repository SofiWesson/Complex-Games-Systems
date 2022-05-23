using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public string name;
    public VarialbeTypes type;
    public string value;
}
