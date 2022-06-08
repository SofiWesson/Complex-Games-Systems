using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ResourcesManager))]
public class ResourceManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ResourcesManager manager = (ResourcesManager)target;

        if (GUILayout.Button("Add Attribute"))
        {
            manager.AddAttributes();
        }

        if (GUILayout.Button("Add Resource"))
        {
            manager.AddResource();
        }

        if (GUILayout.Button("Edit Attribute"))
        {
            manager.EditAttribute();
        }

        if (GUILayout.Button("Edit Resource"))
        {
            manager.EditResource();
        }

        if (GUILayout.Button("Remove Attribute"))
        {
            manager.RemoveAttribute();
        }

        if (GUILayout.Button("Remove Resource"))
        {
            manager.RemoveResource();
        }

        if (GUILayout.Button("Reload Lists"))
        {
            manager.ReloadLists();
        }

        if (GUILayout.Button("Clear All"))
        {
            manager.ClearAll();
        }
    }
}
