using UnityEngine;
using DG.Tweening;
using System.Collections;

public class DropEffect : MonoBehaviour
{
    public Transform projectil;
    public GameObject bubbles;
    public Collider2D c2D;

    void Start()
    {

        // Start the boop effect
        Boop();
    }

    void Boop()
    {

        // Sequence for the boop effect
        Sequence mySequence = DOTween.Sequence();

        // Jump effect
        mySequence.Append(projectil.DOLocalMoveY(-32, 0.2f).SetRelative().SetEase(Ease.InOutQuad))
                  .Append(projectil.DOScale(new Vector3(5, 5, 5), 0.2f).SetEase(Ease.InOutQuad)) 
                  .OnComplete(() => PrepareBubbles());
        // Play the sequence
        mySequence.Play();
    }
    void PrepareBubbles()
    {
        bubbles.SetActive(true);
        c2D.enabled = true;
    }
}