using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;

    public bool stopDamage;

    public float currentHealth { get; private set; }
    
    private void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float _damage)
    {
        //makes sure that health doesnt go under 0 or max value
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
    }
  
    public void OnCollisionEnter2D(Collision2D collision)
    {
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
            yield return new WaitForSeconds(1f);
            stopDamage = false;
        }

        if (currentHealth == 0)
        {
            
        }
    }
}
