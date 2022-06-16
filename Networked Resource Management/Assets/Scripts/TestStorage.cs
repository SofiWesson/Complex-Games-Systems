using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using EasyResourceManager;

public class TestStorage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 mousePos = Mouse.current.position.ReadValue();
        if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePos), out hit))
        {
            if (hit.transform.GetComponent<StorageNode>() && Keyboard.current.eKey.wasPressedThisFrame)
            {
                StorageNode storage = hit.transform.GetComponent<StorageNode>();

                Debug.Log(storage.GetItemAmount("Water"));
            }
        }
    }
}
