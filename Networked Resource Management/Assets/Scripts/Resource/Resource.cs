using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Resource : MonoBehaviour
{
    protected string resourceName;
    protected int countInInventory;
    protected Sprite spirte;
    protected List<Variables.VarialbeTypes> variables;
    protected Attributes attributes;

    public struct CustomVariable
    {
        Variables.VarialbeTypes variable;
        string value;
    }
}
