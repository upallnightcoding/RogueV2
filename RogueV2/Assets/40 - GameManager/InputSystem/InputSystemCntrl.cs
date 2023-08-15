using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemCntrl : MonoBehaviour
{
    public static event Action OnAttackEvent = delegate { };

    private InputSystem inputSystem;

    private InputAction moveAction;
    private InputAction mouseAction;
    private InputAction attackAction;

    public Vector2 GetMoveDirection() => moveAction.ReadValue<Vector2>();

    public Vector2 GetMousePosition() => mouseAction.ReadValue<Vector2>();

    public bool GetLeftMouseButton() => Mouse.current.leftButton.isPressed;

    public bool GetRightMouseButton() => Mouse.current.rightButton.isPressed;

    // Start is called before the first frame update
    void Awake()
    {
        inputSystem = new InputSystem();

        moveAction = inputSystem.Player.Movement;
        mouseAction = inputSystem.Player.Mouse;
        attackAction = inputSystem.Player.Attack;
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        OnAttackEvent.Invoke();
    }

    private void OnEnable()
    {
        moveAction.Enable();
        mouseAction.Enable();
        attackAction.Enable();

        attackAction.performed += OnAttack;
    }

    private void OnDisable()
    {
        moveAction.Disable();
        mouseAction.Disable();
        attackAction.Disable();

        attackAction.performed -= OnAttack;
    }

    private enum INPUT_DIRECTIVE
    {
        DO_NOTHING,
        ATTACK
    }
}
