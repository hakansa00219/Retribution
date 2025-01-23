using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/MovementBehaviours", fileName = "MovementBehaviours")]
public class MovementBehaviours : SerializedScriptableObject
{
    public Dictionary<string, Func<float, float>> Behaviours = new Dictionary<string, Func<float, float>>()
    {
        {"LeftRightMovement", (x) => x * 10 },
    };
}