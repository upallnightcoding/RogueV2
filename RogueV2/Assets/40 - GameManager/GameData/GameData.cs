using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "RogueV2/Game Data")]
public class GameData : ScriptableObject
{
    [Header("Player Attributes")]
    public float runSpeed;
    public float turnSpeed;

    [Header("Camera Attributes")]
    public float cameraFollowSpeed;
    public float cameraLookSpeed;
    public float cameraPivotSpeed;
    public float minmaxPivotAngle;

    [Header("Combat Attributes")]
    public string[] comboList;
}
