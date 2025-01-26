using UnityEngine;

public class WeaponPickUp : Pickup
{
    // The amount of health to restore when this item is collected
    public LevelUpController levelUp;
    public LevelUpItem weapon;

    public override void Collect()
    {
        levelUp.WeaponPickUpScreen(weapon);
        PlayerStats player = FindFirstObjectByType<PlayerStats>();
        player.sound.Play();
    }

    //private void OnTriggerEnter2D(Collider2D col)
    //{
    //    if (col.CompareTag("Player"))
    //    {
    //        Collect();
    //    }
    //}
}