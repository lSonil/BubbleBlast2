using System.Collections;
using UnityEngine;

public class BossFightController : MonoBehaviour
{
    public int castlesCount = 3;
    public float pause = 2f;
    public GameObject boss;
    public GameObject soundController;

    private SoundController soundControllerScript;

    void Start()
    {
        soundControllerScript = soundController.GetComponent<SoundController>();
    }

    public void LoseCastle()
    {
        castlesCount--;
        if (castlesCount <= 0)
        {
            StartCoroutine(WaitPause());
        }
    }

    private IEnumerator WaitPause()
    {
        yield return new WaitForSeconds(pause);
        soundControllerScript.StartBossSound();
        boss.SetActive(true);
    }
}
