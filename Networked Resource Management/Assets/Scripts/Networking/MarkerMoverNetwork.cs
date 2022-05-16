using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;

public class MarkerMoverNetwork : NetworkBehaviour
{
    public Transform marker;
    Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        // the scene has a couple of marker points - red for the client, blue for the server.
        // mouse clicks place the marker point for your marker. We need each character to point to
        // the correct marker on each machine, and dye each player to match their marker
        // isServer - true if you're the host instance of the game
        // islocalPlayer - true if you're in control of this CharacterMovement

        if (isServer != isLocalPlayer)
        {
            // you're the client and this is you, or you're the host and this is not you
            // therefore its the client character!
            // TODO use better logic to accomodate 3+ players, like id == GetComponent<NetworkIdentity>().playerControllerId;

            marker = GameObject.Find("ClientMarker").transform;
            SetColor(Color.red);
            gameObject.name = "Red Player";
        }
        else
        {
            marker = GameObject.Find("ServerMarker").transform;
            SetColor(Color.blue);
            gameObject.name = "Blue Player";
        }

        if (!isLocalPlayer)
            return;
        
        camera = GetComponentInChildren<Camera>();
    }

    void SetColor(Color col)
    {
        GetComponent<Renderer>().material.color = col;
    }

    // Update is called once per frame
    void Update()
    {
        // other player's character still gets an update - don't try to move them, just you!
        if (!isLocalPlayer)
            return;

        Camera.SetupCurrent(camera);

        // click to move a marker
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Vector3 mousePos = Mouse.current.position.ReadValue();

                RaycastHit hit;
                if (Physics.Raycast(camera.ScreenPointToRay(mousePos), out hit))
                {
                    CmdSetMarker(hit.point);
                }
            }
        }
    }

    [Command]
    void CmdSetMarker(Vector3 pos)
    {
        marker.position = pos;
    }
}
