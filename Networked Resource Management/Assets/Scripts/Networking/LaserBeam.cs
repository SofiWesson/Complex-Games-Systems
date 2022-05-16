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
        // // count down, and hide the laser after half a second
        // if (coolDown > 0)
        // {
        //     coolDown -= Time.deltaTime;
        //     if (coolDown < 0.5f)
        //         ShowLaser(false);
        // }

        // only check controls if we're the local player
        if (!isLocalPlayer)
            return;

        if (Mouse.current.leftButton.wasPressedThisFrame && coolDown <= 0)
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
        // // trigger the visuals - this should happen on all machines individually
        // ShowLaser(true);
        // coolDown = 1.0f;
        // // more visual fx, a burst around the firing nozzel
        // if (fireFX)
        //     fireFX.Play();

        // do a raycast to subtract health. We only want to do this on the server rather than each client doing their own raycast
        if (!isServer)
            return;

        RaycastHit hit;
        if (Physics.Raycast(new Ray(transform.position, transform.forward), out hit, 10.0f))
        {
            Health health = hit.transform.GetComponent<Health>();
            if (health)
                health.health -= 10;
        }
    }
}
