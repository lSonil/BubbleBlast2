using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    //[SerializeField] private WeaponStats defaultWeapon;
    public List<WeaponShoot> equipedWeapons = new List<WeaponShoot>(0);
    public TextMeshProUGUI[] weaponLevels = new TextMeshProUGUI[6];
    private Dictionary<WeaponStats, int> weaponLevelsDict = new Dictionary<WeaponStats, int>();
    public List<Image> weaponUISlots = new List<Image>(6);
    public int[] passiveItemLevels = new int[6];
    public List<Image> passiveItemUISlots = new List<Image>(6);
    private int slotIndex = 0;

    private void Awake()
    {
        //AddWeapon(defaultWeapon);
    }

    public void AddWeapon(WeaponShoot weapon)
    {
        WeaponShoot weaponSlot = CheckIfAlreadyInInventory(weapon);
        // Already in inventory. Level-up!
        if (weaponSlot!=null)
        {
            print(2);
            LevelUpWeapon(weaponSlot);
        }
        // Not in inventory already. Add it!
        else
        {
            print(3);
            GameObject body = Instantiate(weapon.gameObject);
            body.transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform, false);
            body.name = weapon.name;
            slotIndex++;
            equipedWeapons.Add(body.GetComponent<WeaponShoot>());
            weaponUISlots[slotIndex].enabled = true;   // Enable the image component
            weaponUISlots[slotIndex].sprite = weapon.weapon.Properties.Sprite;

            // Initialize the weapon level in both the dictionary and the array.
            weaponLevelsDict.Add(weapon.weapon, 1); // Start with level 1
            weaponLevels[slotIndex].text = "1";
        }
    }

    public void LevelUpWeapon(WeaponShoot weapon)
    {
        weapon.lvl++;
        print(weapon.lvl);

        // Update the weapon's level in both the dictionary and the array.
        weaponLevelsDict[weapon.weapon] = weapon.lvl;
        weaponLevels[slotIndex].text = weapon.lvl.ToString();
    }

    public WeaponStats FindWeaponInList(string name)
    {
        if (equipedWeapons != null)
        {
            for (int i = 0; i < equipedWeapons.Count; i++)
            {
                print(equipedWeapons[i].name);
                print(name);

                if (equipedWeapons[i].name == name)
                {
                    return equipedWeapons[i].weapon;
                }
            }
        }
        return null;
    }

    private WeaponShoot CheckIfAlreadyInInventory(WeaponShoot weapon)
    {
        foreach (WeaponShoot wp in equipedWeapons)
        {
            print(weapon.name== wp.name);
            if(weapon.name == wp.name)
            {
                print(234);
                return wp;
            }
        }
        return null;
    }
}