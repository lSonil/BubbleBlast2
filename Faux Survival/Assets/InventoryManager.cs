using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private PlayerShoot shootingHandler;
    [SerializeField] private WeaponStats defaultWeapon;
    public List<WeaponStats> unlockedWeapons = new List<WeaponStats>(6);
    public List<WeaponStats> equipedWeapons = new List<WeaponStats>(0);
    public TextMeshProUGUI[] weaponLevels = new TextMeshProUGUI[6];
    private Dictionary<WeaponStats, int> weaponLevelsDict = new Dictionary<WeaponStats, int>();
    public List<Image> weaponUISlots = new List<Image>(6);
    public int[] passiveItemLevels = new int[6];
    public List<Image> passiveItemUISlots = new List<Image>(6);
    private int slotIndex = 0;

    private void Start()
    {
        shootingHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShoot>();
        equipedWeapons.Add(defaultWeapon);
        weaponUISlots[slotIndex].enabled = true;   // Enable the image component
        weaponUISlots[slotIndex].sprite = defaultWeapon.WeaponProperties.Sprite;

        // Initialize the weapon levels dictionary based on the weaponLevels array.
        for (int i = 0; i < equipedWeapons.Count; i++)
        {
            int weaponLevel = 1; // Default level if parsing fails or text is empty
            if (!string.IsNullOrEmpty(weaponLevels[i].text))
            {
                int.TryParse(weaponLevels[i].text, out weaponLevel); // Try parsing the text
            }
            weaponLevelsDict.Add(equipedWeapons[i], weaponLevel);
            //weaponLevels[i].text = weaponLevel.ToString(); // Ensure the UI text is set correctly
        }

        shootingHandler.AddWeapon(defaultWeapon);
    }



    public void AddWeapon(WeaponStats weapon, LevelUpItem itemLevel)
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
            slotIndex++;
            shootingHandler.AddWeapon(weapon);
            equipedWeapons.Add(weapon);
            weaponUISlots[slotIndex].enabled = true;   // Enable the image component
            weaponUISlots[slotIndex].sprite = weapon.WeaponProperties.Sprite;

            // Initialize the weapon level in both the dictionary and the array.
            weaponLevelsDict.Add(weapon, 1); // Start with level 1
            weaponLevels[slotIndex].text = "1";
        }
    }

    public void LevelUpWeapon(int slotIndex, LevelUpItem itemLevel)
    {
        if (slotIndex < unlockedWeapons.Count)
        {
            WeaponStats weapon = unlockedWeapons[slotIndex];

            if (itemLevel.itemLevel > 1)
            {
                int newLevel = itemLevel.itemLevel;

                // Update the weapon's level in both the dictionary and the array.
                weaponLevelsDict[weapon] = newLevel;
                weaponLevels[slotIndex].text = newLevel.ToString();
            }

            weapon.UpgradeWeapon();
        }
    }

    public int GetWeaponLevel(WeaponStats weapon)
    {
        // Retrieve the weapon's level from the dictionary.
        if (weaponLevelsDict.TryGetValue(weapon, out int level))
        {
            return level;
        }

        return 1; // Return 1 if the weapon is not found in the dictionary.
    }

    public WeaponStats FindWeaponInList(string name)
    {
        if (unlockedWeapons != null)
        {
            for (int i = 0; i < unlockedWeapons.Count; i++)
            {
                if (unlockedWeapons[i].WeaponProperties.name == name)
                {
                    return unlockedWeapons[i];
                }
            }
        }

        return null;
    }

    private int CheckIfAlreadyInInventory(WeaponStats weapon)
    {
        for (int i = 0; i < equipedWeapons.Count; i++)
        {
            if (equipedWeapons[i].WeaponProperties.name == weapon.WeaponProperties.name)
            {
                return i;
            }
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
