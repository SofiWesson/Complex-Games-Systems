using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.InputSystem;

public class LaserBeam : NetworkBehaviour
{
    public LineRenderer lineRenderer;
    public float coolDown;
    public ParticleSystem fireFX;
    int index = 1;

    // Start is called before the first frame update
    void Start()
    {
        // // turn off the linerenderer
        // ShowLaser(false);
        // CharacterMovement cm = GetComponent<CharacterMovement>();
        // if (cm)
        //     index = cm.index;
    }

    // Update is called once per frame
    void Update()
    {
        // count down, and hide the laser after half a second
        if (coolDown > 0)
        {
            coolDown -= Time.deltaTime;
            //if (coolDown < 0.5f)
                //ShowLaser(false);
        }

        // only check controls if we're the local player
        if (!isLocalPlayer)
            return;

        if (Mouse.current.rightButton.wasPressedThisFrame && coolDown <= 0)
            CmdFire();
    }

    [Command]
    void CmdFire()
    {
        // tell all clients to do it too
        RpcFire();
    }

    [ClientRpc]
    void RpcFire()
    {
        DoLaser();
    }

    void DoLaser()
    {
        // trigger the visuals - this should happen on all machines individually
        // ShowLaser(true);
        coolDown = 1.0f;

        // do a raycast to subtract health. We only want to do this on the server rather than each client doing their own raycast
        Vector3 mousePos = Mouse.current.position.ReadValue();

        RaycastHit hit;
        bool didRayHit = Physics.Raycast(new Ray(transform.position, mousePos), out hit, 10.0f);

        if (!isServer)
        {
            if (didRayHit)
            {
                Health health = hit.transform.GetComponent<Health>();
                if (health)
                    health.health -= 10;
            }
        }
        else
        {
            // more visual fx, a burst around the firing nozzel
            if (fireFX)
            {
                float length = (Vector3.Distance(hit.point, transform.position)) / 5;

                fireFX.Play();
                fireFX.startLifetime = length;
                fireFX.time = length;
            }
        }
    }
}
