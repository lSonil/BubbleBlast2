using UnityEngine;

public class SoundController : MonoBehaviour
{
    public GameObject bossSound;
    public GameObject loopSound;
    public GameObject winSound;
    public GameObject gameOverSound;

    public void StartBossSound()
    {
        loopSound.SetActive(false);
        bossSound.SetActive(true);
    }

    public void StartWinSound()
    {
        loopSound.SetActive(false);
        bossSound.SetActive(false);
        winSound.SetActive(true);
    }

    public void StartGameOverSound()
    {
        loopSound.SetActive(false);
        bossSound.SetActive(false);
        gameOverSound.SetActive(true);
    }
}
