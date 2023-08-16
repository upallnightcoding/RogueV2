using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArgoCntrl : MonoBehaviour
{
    [SerializeField] private InputSystemCntrl inputSystemCntrl;
    [SerializeField] private CharacterController charCntrl;
    [SerializeField] private Transform cameraObject;
    [SerializeField] private Animator animator;
    [SerializeField] private CameraSystemCntrl cameraCntrl;
    [SerializeField] private GameData gameData;

    private ArgoState crntState = ArgoState.IDLE;

    private ArgoCombatCntrl animationCntrl;

    private float speed = 1.0f;

    private void Start()
    {
        animationCntrl = GetComponent<ArgoCombatCntrl>();
        animator = GetComponent<Animator>();

        animator.SetFloat("MovementSpeed", 0.0f);
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 moveDirection = inputSystemCntrl.GetMoveDirection();

        switch(crntState)
        {
            case ArgoState.INIT:
                break;
            case ArgoState.IDLE:
                crntState = StateIdle(moveDirection);
                break;
            case ArgoState.ROAMING:
                crntState = StateRoaming(moveDirection, Time.deltaTime);
                break;
            case ArgoState.COMBAT:
                crntState = StateCombat();
                break;
        }
    }

    private void LateUpdate()
    {
        cameraCntrl.HandleAllCameraMovement();
    }

    private ArgoState StateIdle(Vector2 direction)
    {
        ArgoState state = ArgoState.IDLE;

        if (IsPlayerMoving(direction))
        {
            state = ArgoState.ROAMING;
            animator.SetFloat("MovementSpeed", 0.5f);
        }

        return (state);
    }

    private ArgoState StateCombat()
    {
        return (ArgoState.COMBAT);
    }

    private ArgoState StateRoaming(Vector2 direction, float dt)
    {
        ArgoState state = ArgoState.ROAMING;

        if (IsPlayerMoving(direction))
        {
            MovePlayerDirection(direction, dt);
        } 
        else
        {
            state = ArgoState.IDLE;
        }

        return (state);
    }

    private void MovePlayerDirection(Vector2 moveDirection, float dt)
    {
        float gravitySpeed = 0.0f;

        Vector3 direction = cameraObject.forward * moveDirection.y;
        direction = direction + cameraObject.right * moveDirection.x;
        direction.y = 0.0f;
        direction.Normalize();

        Vector3 hortMovement = gameData.runSpeed * direction;
        Vector3 vertMovement = Vector3.up * gravitySpeed;

        charCntrl.Move(dt * (vertMovement + hortMovement));

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, gameData.turnSpeed * dt);

        transform.rotation = playerRotation;
    }

    private void OnAttack()
    {
        animationCntrl.OnAttack();
    }

    private bool IsPlayerMoving(Vector2 playerDirection)
    {
        return((int)playerDirection.magnitude != 0);
    }

    private void OnEnable()
    {
        InputSystemCntrl.OnAttackEvent += OnAttack;
    }

    private void OnDisable()
    {
        InputSystemCntrl.OnAttackEvent -= OnAttack;
    }

    private enum ArgoState
    {
        INIT,
        IDLE,
        ROAMING,
        COMBAT
    }
}
