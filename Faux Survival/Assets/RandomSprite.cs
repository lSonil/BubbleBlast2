using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    public Sprite[] spriteList;
    private void Awake()
    {
        SpriteRenderer sp = GetComponent<SpriteRenderer>();
        sp.sprite = spriteList[Random.Range(0, spriteList.Length-1)];
    }
}
