using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystemCntrl : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private Transform target;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private InputSystemCntrl inputControls;

    private Vector3 cameraVelocity = Vector3.zero;

    private Vector2 mouseDirection;

    private float lookAngle;
    private float pivotAngle;

    private void Awake()
    {

    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
    }

    private void FollowTarget()
    {
        Vector3 position = Vector3.SmoothDamp
            (transform.position, target.position, ref cameraVelocity, gameData.cameraFollowSpeed);

        transform.position = position;
    }

    private void RotateCamera()
    {
        mouseDirection = inputControls.GetMousePosition();

        lookAngle = lookAngle + (mouseDirection.x * gameData.cameraLookSpeed);
        pivotAngle = pivotAngle - (mouseDirection.y * gameData.cameraPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, -gameData.minmaxPivotAngle, gameData.minmaxPivotAngle);

        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);

        cameraPivot.localRotation = targetRotation;
    }
}
