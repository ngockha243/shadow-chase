
using _0.Game.Scripts.Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
    Vector2 startPosition;
    protected override void Start()
    {
        base.Start();
        //background.gameObject.SetActive(false);
        startPosition = background.anchoredPosition;
        background.gameObject.SetActive(true);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        //background.gameObject.SetActive(false);
        background.anchoredPosition = startPosition;
        base.OnPointerUp(eventData);
        GameController.instance.player.movement.SetJoystickDirection(Vector2.zero);
    }

    public void JoyUp()
    {
        background.anchoredPosition = startPosition;
        Up();
    }
}