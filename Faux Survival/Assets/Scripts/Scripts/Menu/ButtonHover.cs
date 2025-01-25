using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler
{
    private Animator textAnimator;

    private void Start()
    {
        textAnimator = GetComponentInChildren<Animator>();
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        Console.WriteLine("hover");
        if (textAnimator != null)
        {
            Console.WriteLine("in if");
            textAnimator.SetTrigger("hover");
        }
    }
}
