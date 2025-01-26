using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGem : Pickup
{
    public int experienceGranted;
    public override void Collect()
    {
        PlayerStats player = FindFirstObjectByType<PlayerStats>();
        player.IncreaseExperience(experienceGranted);
        player.sound.Play();
    }
}
