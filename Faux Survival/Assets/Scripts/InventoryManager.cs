using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private WeaponStats defaultWeapon;
    public List<WeaponStats> unlockedWeapons = new List<WeaponStats>(6);
    public List<WeaponShoot> equipedWeapons = new List<WeaponShoot>(0);
    public TextMeshProUGUI[] weaponLevels = new TextMeshProUGUI[6];
    private Dictionary<WeaponStats, int> weaponLevelsDict = new Dictionary<WeaponStats, int>();
    public List<Image> weaponUISlots = new List<Image>(6);
    public int[] passiveItemLevels = new int[6];
    public List<Image> passiveItemUISlots = new List<Image>(6);
    private int slotIndex = 0;

    private void Awake()
    {
        AddWeapon(defaultWeapon, 1);
    }

    public void AddWeapon(WeaponStats weapon, int itemLevel)
    {
        int weaponSlotIndex = CheckIfAlreadyInInventory(weapon);

        // Already in inventory. Level-up!
        if (weaponSlotIndex != 999)
        {
            LevelUpWeapon(weaponSlotIndex, itemLevel);
        }
        // Not in inventory already. Add it!
        else
        {
            GameObject body = Instantiate(new GameObject());
            body.transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform, false);
            body.name = weapon.name;
            body.AddComponent<WeaponShoot>().weapon= weapon;

            slotIndex++;
            equipedWeapons.Add(body.AddComponent<WeaponShoot>());
            weaponUISlots[slotIndex].enabled = true;   // Enable the image component
            weaponUISlots[slotIndex].sprite = weapon.Properties.Sprite;

            // Initialize the weapon level in both the dictionary and the array.
            weaponLevelsDict.Add(weapon, 1); // Start with level 1
            weaponLevels[slotIndex].text = "1";
        }
    }

    public void LevelUpWeapon(int slotIndex, int itemLevel)
    {
        if (slotIndex < unlockedWeapons.Count)
        {
            WeaponStats weapon = unlockedWeapons[slotIndex];

            if (itemLevel > 1)
            {
                int newLevel = itemLevel;

                // Update the weapon's level in both the dictionary and the array.
                weaponLevelsDict[weapon] = newLevel;
                weaponLevels[slotIndex].text = newLevel.ToString();

                equipedWeapons[slotIndex].bonus= weapon.Properties.LevelUpBonus[itemLevel];
            }
        }
    }

    //public int GetWeaponLevel(WeaponStats weapon)
    //{
    //    // Retrieve the weapon's level from the dictionary.
    //    if (weaponLevelsDict.TryGetValue(weapon, out int level))
    //    {
    //        return level;
    //    }
    //
    //    return 1; // Return 1 if the weapon is not found in the dictionary.
    //}

    public WeaponStats FindWeaponInList(string name)
    {
        if (unlockedWeapons != null)
        {
            for (int i = 0; i < unlockedWeapons.Count; i++)
            {
                if (unlockedWeapons[i].Properties.name == name)
                {
                    return unlockedWeapons[i];
                }
            }
        }

        return null;
    }

    private int CheckIfAlreadyInInventory(WeaponStats weapon)
    {
        foreach(WeaponShoot wp in equipedWeapons)
        {
            if(wp.weapon==weapon)
                return equipedWeapons.IndexOf(wp);
        }

        return 999;
    }
}

//public void AddPassiveItem(int slotIndex, PassiveItem passiveItem)  //Add a passive item to a specific slot
//{
//    passiveItemSlots[slotIndex] = passiveItem;
//    passiveItemLevels[slotIndex] = passiveItem.passiveItemData.Level;
//    passiveItemUISlots[slotIndex].enabled = true; //Enable the image component
//    passiveItemUISlots[slotIndex].sprite = passiveItem.passiveItemData.Icon;
//}


//public void LevelUpPassiveItem(int slotIndex)
//{
//    if (passiveItemSlots.Count > slotIndex)
//    {
//        PassiveItem passiveItem = passiveItemSlots[slotIndex];
//        if (!passiveItem.passiveItemData.NextLevelPrefab)  //Checks if there is a next level
//        {
//            Debug.LogError("NO NEXT LEVEL FOR " + passiveItem.name);
//            return;
//        }
//        GameObject upgradedPassiveItem = Instantiate(passiveItem.passiveItemData.NextLevelPrefab, transform.position, Quaternion.identity);
//        upgradedPassiveItem.transform.SetParent(transform);    //Set the passive item to be a child of the player
//        AddPassiveItem(slotIndex, upgradedPassiveItem.GetComponent<PassiveItem>());
//        Destroy(passiveItem.gameObject);
//        passiveItemLevels[slotIndex] = upgradedPassiveItem.GetComponent<PassiveItem>().passiveItemData.Level;  //To make sure we have the correct passive item level
//    }
//}
