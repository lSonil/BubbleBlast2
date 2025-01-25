using UnityEngine;

[CreateAssetMenu(fileName = "LevelUpItem", menuName = "ScriptableObjects/LevelUpItem")]
public class LevelUpItem : ScriptableObject
{
    [Header("Item Attributes")]
    public Sprite image;
    public string description;
    public int itemLevel;
    public string weaponName;
    public WeaponStats wapon;

    // Method to reset itemLevel to 1
    public void ResetItemLevel()
    {
        itemLevel = 0;
    }
}

