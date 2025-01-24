using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int startingHealth = 100;
    public int startingMight = 0;
    public int startingSpeed = 0;
    public Image healthFillAmount;
    public Image levelFillAmount;

    private int experienceTotal = 0;
    private int currentHealth;
    private int currentMight;
    private int currentSpeed;
    private int currentLevel = 1;

    private int previousLevelExperienceRequirement = 0;
    private int nextLevelExperienceRequirement = 50; // Initial threshold for level 2.

    // You can adjust this percentage to control how the level-up threshold increases.
    public float levelUpThresholdIncreasePercentage = 25f;
    [SerializeField] LevelUpController levelUpController;

    private void Start()
    {
        currentHealth = startingHealth;
        currentMight = startingMight;
        currentSpeed = startingSpeed;

        UpdateHealthText();
    }

    public int GetSpeedBonusPercentage()
    {
        return currentSpeed;
    }

    public int GetMightBonusPercentage()
    {
        return currentMight;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        UpdateHealthText();

        if (currentHealth <= 0)
        {
            RestartLevel();
        }
    }

    public void RestoreHealth(int restoreAmount)
    {
        currentHealth = Mathf.Clamp(currentHealth + restoreAmount, 0, startingHealth);
        UpdateHealthText();
    }

    private void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(10);
        }
    }

    public void IncreaseExperience(int experience)
    {
        experienceTotal += experience;
        Debug.Log("Percentage of 100% is: " + (float)(experienceTotal - previousLevelExperienceRequirement) / ((float)nextLevelExperienceRequirement - previousLevelExperienceRequirement));
        levelFillAmount.fillAmount = (float)(experienceTotal - previousLevelExperienceRequirement) / ((float)nextLevelExperienceRequirement - previousLevelExperienceRequirement);
        CheckForLevelUp();
    }


    private void CheckForLevelUp()
    {
        // Check if the accumulated experience is greater than or equal to the required experience for the next level.
        if (experienceTotal >= nextLevelExperienceRequirement)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        // Increase the current level.
        currentLevel++;

        // Reset the UI fill amount
        levelFillAmount.fillAmount = 0;

        // Store the previous level's experience requirement.
        previousLevelExperienceRequirement = nextLevelExperienceRequirement;

        // Calculate the new level-up threshold based on the percentage increase.
        nextLevelExperienceRequirement += Mathf.RoundToInt(previousLevelExperienceRequirement * (1 + levelUpThresholdIncreasePercentage / 100f));

        // You can also grant additional rewards or update player stats here.
        // For example, you might increase the player's maximum health or grant new abilities.

        Debug.Log("Level Up! Current Level: " + currentLevel);
        Debug.Log("Curent Experience: " + experienceTotal);
        Debug.Log("Next Level Requirement: " + nextLevelExperienceRequirement);
        levelUpController.LevelUpScreen();
    }

    private void UpdateHealthText()
    {
        healthFillAmount.fillAmount = (float)currentHealth / 100f;
    }
}
