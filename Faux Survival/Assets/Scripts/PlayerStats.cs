using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening; // Make sure you import the DoTween namespace

public class PlayerStats : MonoBehaviour
{
    public int startingHealth = 100;
    public int startingMight = 0;
    public int startingSpeed = 0;
    public Image healthFillAmount;
    public Image levelFillAmount;
    public GameObject gameOverPanel;

    private int experienceTotal = 0;
    public int currentHealth;
    private int currentMight;
    private int currentSpeed;

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
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
            UpdateHealthText();

            print(2);
        }
        else
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
        if (!gameOverPanel.activeInHierarchy)
            gameOverPanel.SetActive(true);
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
        CheckForLevelUp();
        float fillAmountTarget = (float)(experienceTotal - previousLevelExperienceRequirement) / ((float)nextLevelExperienceRequirement - previousLevelExperienceRequirement);

        Debug.Log("Percentage of 100% is: " + fillAmountTarget);

        // Tween the fill amount to the target value over 1 second

        levelFillAmount.DOFillAmount(fillAmountTarget, 1.0f);
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
        // Reset the UI fill amount
        levelFillAmount.fillAmount = 0;

        // Store the previous level's experience requirement.
        previousLevelExperienceRequirement = nextLevelExperienceRequirement;

        // Calculate the new level-up threshold based on the percentage increase.
        nextLevelExperienceRequirement += Mathf.RoundToInt(previousLevelExperienceRequirement * (1 + levelUpThresholdIncreasePercentage / 100f));

        // You can also grant additional rewards or update player stats here.
        // For example, you might increase the player's maximum health or grant new abilities.

        //Debug.Log("Level Up! Current Level: " + currentLevel);
        //Debug.Log("Curent Experience: " + experienceTotal);
        //Debug.Log("Next Level Requirement: " + nextLevelExperienceRequirement);
        levelUpController.LevelUpScreen();
    }

    private void UpdateHealthText()
    {
        healthFillAmount.fillAmount = (float)currentHealth / 100f;
    }
}
