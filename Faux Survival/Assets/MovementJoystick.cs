using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementJoystick : MonoBehaviour
{
    [SerializeField] private GameObject joyStick;
    [SerializeField] private GameObject joyStickBackground;
    [SerializeField] private Vector2 joystickVect;
    private Vector2 joystickTouchPos;
    private Vector2 joystickOriginalPos;
    private float joystickRadius;

    // Start is called before the first frame update
    void Start()
    {
        joystickOriginalPos = joyStickBackground.transform.position;
        joystickRadius = joyStickBackground.GetComponent<RectTransform>().sizeDelta.y / 4;
    }

    public Vector2 GetJoystickVect()
    {
        return joystickVect;
    }

    public void PointerDown()
    {
        joyStick.transform.position = Input.mousePosition;
        joyStickBackground.transform.position = Input.mousePosition;
        joystickTouchPos = Input.mousePosition;
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickVect = (dragPos - joystickTouchPos).normalized;

        float joystickDist = Vector2.Distance(dragPos, joystickTouchPos);

        if(joystickDist < joystickRadius)
        {
            joyStick.transform.position = joystickTouchPos + joystickVect * joystickDist;
        }
        else
        {
            joyStick.transform.position = joystickTouchPos + joystickVect * joystickRadius;
        }
    }

    public void PointerUp()
    {
        joystickVect = Vector2.zero;
        joyStick.transform.position = joystickOriginalPos;
        joyStickBackground.transform.position = joystickOriginalPos;
    }
}
