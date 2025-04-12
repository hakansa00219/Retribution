using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

[CreateAssetMenu(menuName = "Scriptable Objects/MovementBehaviours", fileName = "MovementBehaviours")]
public class MovementBehaviours : SerializedScriptableObject
{
    public List<MoveAction> Behaviours;
    
    private void OnEnable()
    {
        Behaviours = new List<MoveAction>
        {
            new("LeftRightMovement", LeftRightMovement),
            new("UpDownMovement", UpDownMovement),
            new("CircleMovement", CircleMovement),
            new("SquareMovement", SquareMovement),
        };
    }

    [Serializable]
    public struct MoveAction
    {
        public string ActionName;
        public Func<Enemy, UniTask> Action;

        public MoveAction(string actionName, Func<Enemy, UniTask> action)
        {
            ActionName = actionName;
            Action = action;
        }
    }

    //need to be canceled when camera changed
    private static async UniTask LeftRightMovement(Enemy e)
    {
        System.Random rnd = new System.Random();
        int rng = rnd.Next(0, 2);
        Vector3 startPos = e.transform.position;
        float offset = 4f;
        
        switch (rng)
        {
            //go +x
            case 0:
                while (IsEnemyValid(e))
                {
                    await MovementHelper.MoveTransformAsync(e.transform,
                        startPos + 
                        (e.CurrentCamType == CamType.Orthographic ? new Vector3(offset, 0f, 0f) : new Vector3(0f, offset, 0f)),
                        2f);
                    await MovementHelper.MoveTransformAsync(e.transform,
                        startPos + 
                        (e.CurrentCamType == CamType.Orthographic ? new Vector3(-offset, 0f, 0f) : new Vector3(0f, -offset, 0f)),
                        2f);
                } 
                break;
            //go -x
            case 1:
                while (IsEnemyValid(e))
                {
                    await MovementHelper.MoveTransformAsync(e.transform,
                        startPos + 
                        (e.CurrentCamType == CamType.Orthographic ? new Vector3(-offset, 0f, 0f) : new Vector3(0f, -offset, 0f)),
                        2f);
                    await MovementHelper.MoveTransformAsync(e.transform,
                        startPos + 
                        (e.CurrentCamType == CamType.Orthographic ? new Vector3(offset, 0f, 0f) : new Vector3(0f, offset, 0f)),
                        2f);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private static async UniTask UpDownMovement(Enemy e)
    {
        System.Random rnd = new System.Random();
        int rng = rnd.Next(0, 2);
        Vector3 startPos = e.transform.position;
        float offset = 3f;
        
        switch (rng)
        {
            //go +x
            case 0:
                while (IsEnemyValid(e))
                {
                    await MovementHelper.MoveTransformAsync(e.transform,
                        startPos + 
                        (e.CurrentCamType == CamType.Orthographic ? new Vector3(0f, 0f, offset) : new Vector3(0f, offset, 0f)),
                        2f);
                    await MovementHelper.MoveTransformAsync(e.transform,
                        startPos + 
                        (e.CurrentCamType == CamType.Orthographic ? new Vector3(0f, 0f, -offset) : new Vector3(0f, -offset, 0f)),
                        2f);
                } 
                break;
            //go -x
            case 1:
                while (IsEnemyValid(e))
                {
                    await MovementHelper.MoveTransformAsync(e.transform,
                        startPos + 
                        (e.CurrentCamType == CamType.Orthographic ? new Vector3(0f, 0f, -offset) : new Vector3(0f, -offset, 0f)),
                        2f);
                    await MovementHelper.MoveTransformAsync(e.transform,
                        startPos + 
                        (e.CurrentCamType == CamType.Orthographic ? new Vector3(0f, 0f, offset) : new Vector3(0f, offset, 0f)),
                        2f);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private static async UniTask SquareMovement(Enemy e)
    {
        System.Random rnd = new System.Random();
        int rng = rnd.Next(0, 2);
        Vector3 startPos = e.transform.position;
        float offset = 4f;
        
        switch (rng)
        {
            //go +x
            case 0:
                while (IsEnemyValid(e))
                {
                    await MovementHelper.MoveTransformAsync(e.transform,
                        startPos + 
                        (e.CurrentCamType == CamType.Orthographic ? new Vector3(0f, 0f, offset) : new Vector3(0f, offset, 0f)),
                        4f);
                    await MovementHelper.MoveTransformAsync(e.transform,
                        startPos + 
                        (e.CurrentCamType == CamType.Orthographic ? new Vector3(offset, 0f, offset) : new Vector3(offset, offset, 0f)),
                        4f);
                    await MovementHelper.MoveTransformAsync(e.transform,
                        startPos + new Vector3(offset, 0f, 0f), 4f);
                    await MovementHelper.MoveTransformAsync(e.transform,
                        startPos, 4f);
                } 
                break;
            //go -x
            case 1:
                while (IsEnemyValid(e))
                {
                    await MovementHelper.MoveTransformAsync(e.transform,
                        startPos + 
                        (e.CurrentCamType == CamType.Orthographic ? new Vector3(0f, 0f, -offset) : new Vector3(0f, -offset, 0f)),
                        4f);
                    await MovementHelper.MoveTransformAsync(e.transform,
                        startPos + 
                        (e.CurrentCamType == CamType.Orthographic ? new Vector3(-offset, 0f, -offset) : new Vector3(-offset, -offset, 0f)),
                        4f);
                    await MovementHelper.MoveTransformAsync(e.transform,
                        startPos + new Vector3(-offset, 0f, 0f), 4f);
                    await MovementHelper.MoveTransformAsync(e.transform,
                        startPos, 4f);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private static async UniTask CircleMovement(Enemy e)
    {
        System.Random rnd = new System.Random();
        int rng = rnd.Next(0, 2);
        
        switch (rng)
        {
            //go +x
            case 0:
                while (IsEnemyValid(e))
                {
                    await MovementHelper.MoveCircleTransformAsync(e.transform, 1f);
                    await UniTask.Yield();
                } 
                break;
            //go -x
            case 1:
                while (IsEnemyValid(e))
                {
                    await MovementHelper.MoveCircleTransformAsync(e.transform, -1f);
                    await UniTask.Yield();
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private static bool IsEnemyValid(Enemy e) =>
        e != null && e.gameObject != null && e.gameObject.activeInHierarchy;
}