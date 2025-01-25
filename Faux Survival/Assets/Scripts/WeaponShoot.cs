using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    [SerializeField] private GameObject firePoint;
    public WeaponStats weapon; // Weapons in the Players inventory
    public int lvl=0; // Weapons in the Players inventory

    private Vector2 shootDirection = Vector2.up;
    private Transform cluster;

    private IEnumerator ShootProjectile()
    {
        Rigidbody2D movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        // Check if the player has started moving.
        if (movement.linearVelocity != Vector2.zero)
        {
            shootDirection = movement.linearVelocity.normalized;
        }
        Transform targetBody=null;
        Vector2 target=Vector2.zero;

        for (int i = 0; i < weapon.Properties.NumberOfProjectiles + weapon.Properties.LevelUpBonus[lvl].numberOfProjectiles; i++)
        {

            if (targetBody == null || !weapon.Properties.Focus)
            {
                if (weapon.Properties.Type == WeaponShootType.Homing)
                {
                    if (FindClosestEnemy() == null)
                        yield break;

                    targetBody = FindClosestEnemy().transform;
                    target = targetBody.position;
                }

                if (weapon.Properties.Type == WeaponShootType.Random)
                {
                    if (FindRandomEnemy() == null)
                        yield break;

                    targetBody = FindRandomEnemy().transform;
                    target = targetBody.position;
                }
            }

            Vector2 homingDirection = shootDirection;
            if (targetBody!=null)
                homingDirection = (target - (Vector2)firePoint.transform.position).normalized;

            // Create a new Weapon instance at the firePoint position.
            GameObject projectile = Instantiate(weapon.Properties.ProjectilePrefab, firePoint.transform.position, Quaternion.identity);
            projectile.transform.SetParent(cluster, false);

            // Set the Weapon's velocity to move in the last direction the player was moving.
            if(weapon.Properties.Instant && targetBody != null)
                projectile.transform.position = target;
            else
                projectile.GetComponent<Rigidbody2D>().linearVelocity = homingDirection * (weapon.Properties.MoveSpeed + weapon.Properties.LevelUpBonus[lvl].moveSpeed);
            
            // Wait for the specified delay between shots.
            yield return new WaitForSeconds(weapon.Properties.DelayBetweenShots - weapon.Properties.LevelUpBonus[lvl].delayBetweenShots);
        }

    }
    private void Start()
    {
        cluster = GameObject.Find("ProjectilesCluster").transform;
        firePoint = GameObject.Find("Muzzle");

        StartCoroutine(StartShooting());
    }

    private IEnumerator StartShooting()
    {
        while (true)
        {
            if (weapon == null)
                yield return null;
            else
            {
                StartCoroutine(ShootProjectile());
                yield return new WaitForSeconds(weapon.Properties.Cooldown - weapon.Properties.LevelUpBonus[lvl].cooldown);
                weapon.lvl = lvl;
            }
        }
    }

    // Implement a method to find the closest enemy here.
    private Transform FindClosestEnemy()
    {
        return GameObject.FindFirstObjectByType<EnemySpawner>().FindClosestEnemy(gameObject.transform.position);
    }

    private Transform FindRandomEnemy()
    {
        return GameObject.FindFirstObjectByType<EnemySpawner>().FindRandomEnemy(gameObject.transform.position);
    }
}
