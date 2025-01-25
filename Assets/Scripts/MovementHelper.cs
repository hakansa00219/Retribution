using Cysharp.Threading.Tasks;
using UnityEngine;

public static class MovementHelper
{
    public static async UniTask MoveTransformAsync(Transform transform, Vector3 targetPosition, float time)
    {
        Vector3 initialPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            // Calculate interpolation factor (0 to 1)
            float t = elapsedTime / time;
            
            // Update position with smooth interpolation
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            
            // Accumulate time and wait for next frame
            elapsedTime += Time.deltaTime;
            await UniTask.Yield();
        }

        // Ensure final position is exactly the target
        transform.position = targetPosition;
    }
}