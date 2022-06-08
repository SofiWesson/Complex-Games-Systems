using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheckStorageNode : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            RaycastHit hit;
            Vector3 mousePos = Mouse.current.position.ReadValue();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePos), out hit))
            {
                ResourceIdentifier storage;
                hit.transform.TryGetComponent(out storage);
                Resource.ResourceObj resource = storage.GetResource();

                Debug.Log(resource.name);
            }
        }
    }
}