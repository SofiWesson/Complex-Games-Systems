using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Health : NetworkBehaviour
{
    [SyncVar(hook = "onHealthChanged")]
    public float health = 100;
    public float maxHealth = 100;

    public void onHealthChanged(System.Single oldValue, System.Single newValue)
    {

    }
}
