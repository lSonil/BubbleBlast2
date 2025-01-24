using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableProps : MonoBehaviour
{
    public float health;

    public void TakeDamage(float dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerWeapon"))
        {
            TakeDamage(collision.gameObject.GetComponent<WeaponStats>().WeaponProperties.Damage);
        }
    }
}
