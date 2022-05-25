using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ResourceManager))]
public class ResourceManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ResourceManager manager = (ResourceManager)target;

        if (GUILayout.Button("Add Attribute"))
        {
            manager.AddAttributes();
        }

        if (GUILayout.Button("Add Resource"))
        {
            manager.AddResource();
        }

        if (GUILayout.Button("Remove Attribute"))
        {
            manager.RemoveAttribute();
        }

        if (GUILayout.Button("Remove Resource"))
        {
            manager.RemoveResource();
        }

        if (GUILayout.Button("Clear All"))
        {
            manager.ClearAll();
        }
    }
}
