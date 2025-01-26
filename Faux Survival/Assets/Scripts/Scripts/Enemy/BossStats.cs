using UnityEngine;

public class BossStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    public GameObject winPanel;

    //Current stats
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public int currentHealth;
    [HideInInspector]
    public int currentDamage;

    public float despawnDistance = 20f;
    Transform player;

    void Awake()
    {
        //Assign the vaiables
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    void Start()
    {
        player = FindFirstObjectByType<PlayerStats>().transform;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            Kill();
        }
    }


    public void Kill()
    {
        Destroy(gameObject);
        winPanel.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerWeapon"))
        {
            TakeDamage(collision.gameObject.GetComponent<WeaponStats>().Properties.Damage);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        //Reference the script from the collided collider and deal damage using TakeDamage()
        if (col.gameObject.CompareTag("Player"))
        {
            PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }

    private void OnDestroy()
    {
        GameObject es = GameObject.Find("Enemy Spawner");

        if (es != null)
        {
            es.GetComponent<EnemySpawner>().OnEnemyKilled(gameObject);
        }
    }

    void ReturnEnemy()
    {
        EnemySpawner es = GameObject.Find("Enemy Spawner").GetComponent<EnemySpawner>();
        transform.position = player.position + es.relativeSpawnPoints[Random.Range(0, es.relativeSpawnPoints.Count)].position;
    }
}
