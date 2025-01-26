using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class LevelUpController : MonoBehaviour
{
    [SerializeField] private LevelUpItem defaultItem;
    public GameObject levelUpScreen;
    public GameObject gameOverPanel;
    public GameObject gameWinPanel;
    public Transform buttonContainer;
    public List<LevelUpItem> levelUpItems;
    private List<LevelUpItem> selectedItems;
    private List<WeaponStats> weapons;
    [SerializeField] private InventoryManager inventoryManager;

    void Start()
    {
        // Initialize the selectedItems list as empty
        selectedItems = new List<LevelUpItem>();

        // Reset item levels to 1 when the game starts
        foreach (LevelUpItem item in levelUpItems)
        {
            item.ResetItemLevel();
        }

        defaultItem.itemLevel = 1;
    }

    // Function to open the Level Up screen
    public void LevelUpScreen()
    {
        if (!gameOverPanel.activeInHierarchy && !gameWinPanel.activeInHierarchy)
        {// Pause the game time
            Time.timeScale = 0f;

            // Activate the Level Up screen UI
            levelUpScreen.SetActive(true);

            // Randomly select 3 items from levelUpItems
            selectedItems = levelUpItems
                .OrderBy(x => Random.value)
                .Take(3)
                .ToList();

            // Populate the buttons with selected items
            for (int i = 1; i < 4; i++)
            {
                buttonContainer.GetChild(i).gameObject.SetActive(true);

                Transform button = buttonContainer.GetChild(i);
                TextMeshProUGUI itemText = button.Find("ItemText").GetComponent<TextMeshProUGUI>();

                // Assign the image and text from selected items
                LevelUpItem selectedItem = selectedItems[i - 1];
                string itemDescription = selectedItem.description;

                if (selectedItem.itemLevel > 1)
                {
                    // Display the item description with a suffix if clicked before
                    itemText.text = $"{itemDescription} (Level {selectedItem.itemLevel})";
                }
                else
                {
                    itemText.text = itemDescription;
                }


                // Remove previous onClick listeners to prevent multiple calls
                button.GetComponent<Button>().onClick.RemoveAllListeners();

                // Add an onClick event to each button
                int itemIndex = i - 1; // To capture the correct itemIndex in the lambda
                button.GetComponent<Button>().onClick.AddListener(() => UpgradeItem(itemIndex));
            }
        }
    }


    public void WeaponPickUpScreen(LevelUpItem item)
    {
        if (!gameOverPanel.activeInHierarchy && !gameWinPanel.activeInHierarchy)
        {// Pause the game time
            Time.timeScale = 0f;

            // Activate the Level Up screen UI
            levelUpScreen.SetActive(true);

            // Randomly select 3 items from levelUpItems
            selectedItems = levelUpItems
                .OrderBy(x => Random.value)
                .Take(3)
                .ToList();
            selectedItems[1] = item;
            // Populate the buttons with selected items
            buttonContainer.GetChild(1).gameObject.SetActive(false);
            buttonContainer.GetChild(3).gameObject.SetActive(false);
            Transform button = buttonContainer.GetChild(2);
            TextMeshProUGUI itemText = button.Find("ItemText").GetComponent<TextMeshProUGUI>();

            // Assign the image and text from selected items
            LevelUpItem selectedItem = selectedItems[1];
            string itemDescription = selectedItem.description;

            if (selectedItem.itemLevel > 1)
            {
                // Display the item description with a suffix if clicked before
                itemText.text = $"{itemDescription} (Level {selectedItem.itemLevel})";
            }
            else
            {
                itemText.text = itemDescription;
            }


            // Remove previous onClick listeners to prevent multiple calls
            button.GetComponent<Button>().onClick.RemoveAllListeners();

            // Add an onClick event to each button
            button.GetComponent<Button>().onClick.AddListener(() => UpgradeItem(1));
        }
    }

    // Function to handle item upgrades
    public void UpgradeItem(int itemIndex)
    {
        // Get the selected item
        LevelUpItem selectedItem = selectedItems[itemIndex];

        //Debug.Log("UPGRADE: " + selectedItems[itemIndex].description + "LEVEL: " + selectedItems[itemIndex].itemLevel);

        WeaponShoot weapon = selectedItem.weapon;
        print(weapon);
        if (weapon != null)
        {
            inventoryManager.AddWeapon(weapon);
        }

        // Remove the Level Up screen
        levelUpScreen.SetActive(false);

        // Resume the game time
        Time.timeScale = 1f;
    }
}
