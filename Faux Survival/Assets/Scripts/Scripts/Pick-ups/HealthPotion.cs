using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Pickup
{
    // The amount of health to restore when this item is collected
    public int healthToRestore;

    public override void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.RestoreHealth(healthToRestore);
    }

    //private void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.CompareTag("Player"))
    //    {
    //        Collect();
    //    }
    //}
}
