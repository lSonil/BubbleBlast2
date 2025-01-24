using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject firePoint;
    [SerializeField] private EnemySpawner enemySpawner;
    private List<WeaponStats> WeaponsOnPlayer = new List<WeaponStats>(6); // Weapons in the Players inventory
    [SerializeField] private InventoryManager inventoryManager;
    public float WeaponSpeed = 10f; // Speed at which Weapons move.
    public float WeaponLifetime = 2f; // Lifetime of each Weapon before it despawns.

    private float[] nextFireTime;
    private Vector2 shootDirection = Vector2.zero;
    private bool movedOnce = false;
    private bool busyShootingProjectile = false;
    private bool busyShootingSpecial = false;
    private bool busyShootingHoming = false;

    private IEnumerator ShootProjectile(GameObject projectilePrefab, int weaponLevel, float delayBetweenShots)
    {
        busyShootingProjectile = true;

        for (int i = 0; i < weaponLevel; i++)
        {
            // Create a new Weapon instance at the firePoint position.
            GameObject Weapon = Instantiate(projectilePrefab, firePoint.transform.position, Quaternion.identity);

            // Set the Weapon's velocity to move in the last direction the player was moving.
            Weapon.GetComponent<Rigidbody2D>().linearVelocity = shootDirection * WeaponSpeed;

            // Destroy the Weapon after the specified lifetime.
            Destroy(Weapon, WeaponLifetime);

            // Wait for the specified delay between shots.
            yield return new WaitForSeconds(delayBetweenShots);
        }

        busyShootingProjectile = false;
    }

    private IEnumerator ShootHoming(GameObject projectilePrefab, int weaponLevel, float delayBetweenShots)
    {
        busyShootingHoming = true;

        // Find the closest enemy (you need to implement this logic)
        Transform closestEnemy = FindClosestEnemy();

        if (closestEnemy != null)
        {
            for (int i = 0; i < weaponLevel; i++)
            {
                // Calculate the direction towards the closest enemy.
                Vector2 homingDirection = (closestEnemy.position - firePoint.transform.position).normalized;

                // Create a new homing Weapon instance at the firePoint position.
                GameObject homingWeapon = Instantiate(projectilePrefab, firePoint.transform.position, Quaternion.identity);

                // Set the Weapon's velocity to move towards the closest enemy.
                homingWeapon.GetComponent<Rigidbody2D>().linearVelocity = homingDirection * WeaponSpeed;

                // Destroy the Weapon after the specified lifetime.
                Destroy(homingWeapon, WeaponLifetime);

                // Wait for the specified delay between shots.
                yield return new WaitForSeconds(delayBetweenShots);
            }
        }
        busyShootingHoming = false;
    }

    private IEnumerator ShootSpecial(GameObject projectilePrefab, int weaponLevel, float delayBetweenShots)
    {
        busyShootingSpecial = true;

        for (int i = 0; i < weaponLevel; i++)
        {
            // Create a new Weapon instance at the firePoint position.
            GameObject Weapon = Instantiate(projectilePrefab, firePoint.transform.position, Quaternion.identity);

            // Set the Weapon's velocity to move in the last direction the player was moving.
            Weapon.GetComponent<Rigidbody2D>().linearVelocity = shootDirection * WeaponSpeed;

            // Destroy the Weapon after the specified lifetime.
            Destroy(Weapon, WeaponLifetime);

            // Wait for the specified delay between shots.
            yield return new WaitForSeconds(delayBetweenShots);
        }

        busyShootingSpecial = false;
    }

    private void Start()
    {
        nextFireTime = new float[WeaponsOnPlayer.Count];
    }

    private void Update()
    {
        // Check if the player has started moving.
        if (GetComponent<Rigidbody2D>().linearVelocity != Vector2.zero)
        {
            shootDirection = GetComponent<Rigidbody2D>().linearVelocity.normalized;
            movedOnce = true;
        }

        if (movedOnce)
        {
            for (int i = 0; i < WeaponsOnPlayer.Count; i++)
            {
                CheckIfReadyToShoot(WeaponsOnPlayer[i], i);
            }
        }
    }

    void CheckIfReadyToShoot(WeaponStats weapon, int weaponIndex)
    {
        if (Time.time >= nextFireTime[weaponIndex])
        {
            if (weapon.WeaponProperties.Homing/* && !busyShootingHoming*/)
            {
                nextFireTime[weaponIndex] = Time.time + weapon.WeaponProperties.DelayBetweenShots;
                StartCoroutine(ShootHoming(weapon.WeaponProperties.ProjectilePrefab, inventoryManager.GetWeaponLevel(weapon), weapon.WeaponProperties.DelayBetweenShots));
            }
            else if(weapon.WeaponProperties.Special /*&& !busyShootingSpecial*/)
            {
                nextFireTime[weaponIndex] = Time.time + weapon.WeaponProperties.DelayBetweenShots;
                StartCoroutine(ShootSpecial(weapon.WeaponProperties.ProjectilePrefab, inventoryManager.GetWeaponLevel(weapon), weapon.WeaponProperties.DelayBetweenShots));
            }
            else /*if(!busyShootingProjectile)*/
            {
                nextFireTime[weaponIndex] = Time.time + weapon.WeaponProperties.DelayBetweenShots;
                StartCoroutine(ShootProjectile(weapon.WeaponProperties.ProjectilePrefab, inventoryManager.GetWeaponLevel(weapon), weapon.WeaponProperties.DelayBetweenShots));
            }
        }
    }

    public void AddWeapon(WeaponStats weapon)
    {
        WeaponsOnPlayer.Add(weapon);
        System.Array.Resize(ref nextFireTime, WeaponsOnPlayer.Count);
        nextFireTime[WeaponsOnPlayer.Count - 1] = 0f;
    }

    // Implement a method to find the closest enemy here.
    private Transform FindClosestEnemy()
    {
        return enemySpawner.FindClosestEnemy(gameObject.transform.position);
    }
}
