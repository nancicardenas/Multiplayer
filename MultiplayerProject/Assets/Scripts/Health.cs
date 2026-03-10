using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;


public class Health : NetworkBehaviour
{
    [SerializeField] private int startingHealth;

    public NetworkVariable<int> currentHealth = new NetworkVariable<int>();

    public bool stopDamage;

    public override void OnNetworkSpawn()
    {
        if(IsServer)
        {
            currentHealth.Value = startingHealth;
        }
    }

    public void TakeDamage(int _damage)
    {
        if (!IsServer) return;

        //makes sure that health doesnt go under 0 or max value
        currentHealth.Value = Mathf.Clamp(currentHealth.Value - _damage, 0, startingHealth);
    }
  
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsServer) return;

        //take damage if hit by enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(TookDamage());
        }
    }

    private IEnumerator TookDamage()
    {
        if (!stopDamage)
        {
            stopDamage = true;
            TakeDamage(1);

            yield return new WaitForSeconds(2f);

            stopDamage = false;
        }

        //death
        if (currentHealth.Value <= 0)
        {
            
        }
    }
}
