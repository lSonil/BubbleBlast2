using UnityEngine;

public class CastleStats : MonoBehaviour
{
    public int maxHealth = 100;
    public GameObject bossFightController;
    private int currentHealth;
    private BossFightController bossFightControllerScript;
    public AudioSource audio;

    void Start()
    {
        currentHealth = maxHealth;
        bossFightControllerScript = bossFightController.GetComponent<BossFightController>();
    }

    public void TakeDamage(int dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            bossFightControllerScript.LoseCastle();
            Kill();
        }
    }

    public void Kill()
    {
        audio.Play();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerWeapon"))
        {
            TakeDamage(collision.gameObject.GetComponent<WeaponStats>().Properties.Damage);
            Destroy(collision.gameObject);
        }
    }
}
