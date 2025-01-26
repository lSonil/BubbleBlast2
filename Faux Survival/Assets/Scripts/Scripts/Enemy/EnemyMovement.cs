using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyStats enemy;
    Transform player;
    public float maxDistance = 0;
    public float projectileSpeed = 0;
    public float cooldown = 0;
    public GameObject projectile;
    public GameObject firePoint;
    bool started=false;
    void Start()
    {
        enemy = GetComponent<EnemyStats>();
        player = FindFirstObjectByType<PlayerMovement>().transform;
    }

    void Update()
    {
        if (maxDistance != 0 && Vector2.Distance(transform.position, player.transform.position) <= maxDistance)
        {
            if(!started)
            {
                started = true;
                StartCoroutine(Shoot());
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime);    //Constantly move the enemy towards the player
        }
    }

    IEnumerator Shoot()
    {
        while(Vector2.Distance(transform.position, player.transform.position) <= maxDistance)
        {
            GameObject proj = Instantiate(projectile, firePoint.transform.position, Quaternion.identity);
            proj.transform.SetParent(GameObject.Find("ProjectilesCluster").transform, false);

            // Calculate the direction from the projectile to the player
            Vector2 direction = (player.transform.position - proj.transform.position).normalized;

            // Set the projectile's velocity to move towards the player
            proj.GetComponent<Rigidbody2D>().linearVelocity = direction * projectileSpeed; yield return new WaitForSeconds(cooldown);
        }
        started = false;
    }
}
