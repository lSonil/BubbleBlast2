using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    //Base stats for the enemy
    [SerializeField]
    float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }

    [SerializeField]
    int maxHealth;
    public int MaxHealth { get => maxHealth; private set => maxHealth = value; }

    [SerializeField]
    int damage;
    public int Damage { get => damage; private set => damage = value; }
}
