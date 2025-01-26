using UnityEngine;

public class BossStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    public GameObject winPanel;
    public GameObject gameOverPanel;
    public GameObject soundController;

    //Current stats
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public int currentHealth;
    [HideInInspector]
    public int currentDamage;

    public float despawnDistance = 20f;
    Transform player;

    private SoundController soundControllerScript;

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
        soundControllerScript = soundController.GetComponent<SoundController>();
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
        if (!gameOverPanel.activeInHierarchy)
        {
            winPanel.SetActive(true);
            soundControllerScript.StartWinSound();
        }
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
}
