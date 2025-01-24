using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapons")]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField] private float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }

    [SerializeField] private int damage;
    public int Damage { get => damage; private set => damage = value; }

    [SerializeField] private int knockbackForce;
    public int KnockbackForce { get => knockbackForce; private set => knockbackForce = value; }

    [SerializeField] private Sprite sprite;
    public Sprite Sprite { get => sprite; private set => sprite = value; }

    [SerializeField] private bool homing;
    public bool Homing { get => homing; private set => homing = value; }

    [SerializeField] private bool special;
    public bool Special { get => special; private set => special = value; }

    //[SerializeField] private int level;
    //public int Level { get => level; private set => level = value; }

    [SerializeField] private float delayBetweenShots;
    public float DelayBetweenShots { get => delayBetweenShots; private set => delayBetweenShots = value; }

    [SerializeField] private GameObject projectilePrefab;
    public GameObject ProjectilePrefab { get => projectilePrefab; private set => projectilePrefab = value; }
}
