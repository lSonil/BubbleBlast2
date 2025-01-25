using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    [SerializeField] public WeaponScriptableObject Properties;
    private int pierce;

    private void Awake()
    {
        pierce = Properties.Pierce;
    }

    public void FixedUpdate()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (Vector2.Distance(transform.position, player.transform.position) >= 16)
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Enemy"))
        {
            if (pierce==0)
                Destroy(gameObject);
            pierce--;
        }
        
    }
}
