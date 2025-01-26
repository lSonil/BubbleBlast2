using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private void Update()
    {
        if(Vector2.Distance(transform.position, GameObject.FindFirstObjectByType<PlayerMovement>().transform.position)>=20)
            Destroy(gameObject);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStats>().TakeDamage(200);
            Destroy(gameObject);
        }
    }
}
