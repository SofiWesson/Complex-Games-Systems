using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Health : NetworkBehaviour
{
    [SyncVar(hook = "onHealthChanged")]
    public float health = 100;
    public float maxHealth = 100;

    void Start()
    {
        // add a healthbar to the to the canvas
        // healthBar = HealthBarManager.instance.AddHealthBar(this);
    }

    public void ApplyDamage(float damage)
    {
        // // play the blood FX
        // if (damage > 0 && hitFX)
        //     hitFX.Play();

        // subtract the damage
        health -= damage;

        // // update our health bar
        // if (healthBar)
        //     healthBar.UpdateMeter();
    }

    public void onHealthChanged(System.Single oldValue, System.Single newValue)
    {
        if (oldValue != newValue)
            health = newValue;
    }    
}
