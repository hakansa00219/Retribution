using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/MovementBehaviours", fileName = "MovementBehaviours")]
public class MovementBehaviours : SerializedScriptableObject
{
    public Dictionary<string, Func<Enemy, UniTaskVoid>> Behaviours = new Dictionary<string, Func<Enemy, UniTaskVoid>>()
    {
        {"LeftRightMovement", LeftRightMovement },
    };

    //need to be canceled when camera changed
    private static async UniTaskVoid LeftRightMovement(Enemy e)
    {
        int rng = UnityEngine.Random.Range(0, 2);
        switch (rng)
        {
            //go +x
            case 0:
                while (e != null && e.gameObject != null && e.gameObject.activeInHierarchy)
                {
                    await MovementHelper.MoveTransformAsync(e.transform,
                        new Vector3(4f, e.transform.position.y, e.transform.position.z), 1f);
                    await MovementHelper.MoveTransformAsync(e.transform,
                        new Vector3(-4f, e.transform.position.y, e.transform.position.z), 2f);
                } 
                break;
            //go -x
            case 1:
                while (e != null && e.gameObject != null && e.gameObject.activeInHierarchy)
                {
                    await MovementHelper.MoveTransformAsync(e.transform,
                        new Vector3(-4f, e.transform.position.y, e.transform.position.z), 1f);
                    await MovementHelper.MoveTransformAsync(e.transform,
                        new Vector3(4f, e.transform.position.y, e.transform.position.z), 2f);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}