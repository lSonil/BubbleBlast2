using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    [SerializeField] public WeaponScriptableObject Properties;
    public int lvl = 0; // Weapons in the Players inventory
    private int pierce;
    private float lifetime;

    private void Awake()
    {
        pierce = Properties.Pierce + Properties.LevelUpBonus[lvl].pierce;
        lifetime = Properties.Lifetime + Properties.LevelUpBonus[lvl].lifetime;
        if (lifetime != 0)
            StartCoroutine(DestryBullet());
    }

    IEnumerator DestryBullet()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    public void FixedUpdate()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (Vector2.Distance(transform.position, player.transform.position) >= 16)
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (lifetime != 0)
            return;

        if(collision.transform.CompareTag("Enemy"))
        {
            if (pierce==0)
                Destroy(gameObject);
            pierce--;
        }
        
    }
}
