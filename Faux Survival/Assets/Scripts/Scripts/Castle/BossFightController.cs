using UnityEngine;

public class BossFightController : MonoBehaviour
{
    public int castlesCount = 3;
    public GameObject boss;

    public void LoseCastle()
    {
        castlesCount--;
        if (castlesCount <= 0)
        {
            boss.SetActive(true);
        }
    }
}
