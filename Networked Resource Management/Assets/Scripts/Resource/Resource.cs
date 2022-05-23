using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Resource : ScriptableObject
{
    public string name;
    public int countInInventory;
    public Sprite spirte;
    public List<Variable> variables = new List<Variable>();
    public List<Attribute> attributes = new List<Attribute>();
}
