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
        if (!gameOverPanel.activeInHierarchy)
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
                Transform button = buttonContainer.GetChild(i);
                Image itemImage = button.Find("ItemImage").GetComponent<Image>();
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

                // Display the item image
                itemImage.sprite = selectedItem.image;

                // Remove previous onClick listeners to prevent multiple calls
                button.GetComponent<Button>().onClick.RemoveAllListeners();

                // Add an onClick event to each button
                int itemIndex = i - 1; // To capture the correct itemIndex in the lambda
                button.GetComponent<Button>().onClick.AddListener(() => UpgradeItem(itemIndex));
            }
        }
    }

    // Function to handle item upgrades
    void UpgradeItem(int itemIndex)
    {
        // Get the selected item
        LevelUpItem selectedItem = selectedItems[itemIndex];

        //Debug.Log("UPGRADE: " + selectedItems[itemIndex].description + "LEVEL: " + selectedItems[itemIndex].itemLevel);

        WeaponStats weapon = selectedItem.wapon;
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
