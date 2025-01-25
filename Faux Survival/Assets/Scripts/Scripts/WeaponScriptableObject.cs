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

    [SerializeField] private WeaponShootType type;
    public WeaponShootType Type { get => type; private set => type = value; }

    [SerializeField] private bool instant;
    public bool Instant { get => instant; private set => instant = value; }

    [SerializeField] private int numberOfProjectiles;
    public int NumberOfProjectiles { get => numberOfProjectiles; private set => numberOfProjectiles = value; }

    //[SerializeField] private int level;
    //public int Level { get => level; private set => level = value; }

    [SerializeField] private float delayBetweenShots;
    public float DelayBetweenShots { get => delayBetweenShots; private set => delayBetweenShots = value; }
    [SerializeField] private float cooldown;
    public float Cooldown { get => cooldown; private set => cooldown = value; }

    [SerializeField] private GameObject projectilePrefab;
    public GameObject ProjectilePrefab { get => projectilePrefab; private set => projectilePrefab = value; }

    [SerializeField] private WeaponBonus[] levelUpBonus;
    public WeaponBonus[] LevelUpBonus { get => levelUpBonus; private set => levelUpBonus = value; }
}

public enum WeaponShootType
{
    Direction,
    Homing,
    Random,
    Special
}

[System.Serializable]
public struct WeaponBonus
{
    public float moveSpeed;          // Ensure fields are serializable
    public int damage;
    public int knockbackForce;
    public int numberOfProjectiles;
    public float cooldown;
}