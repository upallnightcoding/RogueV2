using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemCntrl : MonoBehaviour
{
    private InputSystem inputSystem;

    private InputAction attackAction;
    private InputAction moveAction;
    private InputAction mouseAction;

    public Vector2 GetMoveDirection()
    {
        return (moveAction.ReadValue<Vector2>());
    }

    public Vector2 GetMouseDirection()
    {
        return (mouseAction.ReadValue<Vector2>());
    }

    public bool GetLeftMouseButton() => Mouse.current.leftButton.isPressed;

    public bool GetRightMouseButton() => Mouse.current.rightButton.isPressed;

    // Start is called before the first frame update
    void Awake()
    {
        inputSystem = new InputSystem();

        attackAction = inputSystem.Player.Attack;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
}
