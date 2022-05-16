using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;

public class CharacterMovement : NetworkBehaviour
{
    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (Keyboard.current.wKey.isPressed)
            transform.position += Vector3.forward * speed * Time.deltaTime;
        if (Keyboard.current.sKey.isPressed)
            transform.position -= Vector3.forward * speed * Time.deltaTime;
        if (Keyboard.current.dKey.isPressed)
            transform.position += Vector3.right * speed * Time.deltaTime;
        if (Keyboard.current.aKey.isPressed)
            transform.position -= Vector3.right * speed * Time.deltaTime;
    }
}
