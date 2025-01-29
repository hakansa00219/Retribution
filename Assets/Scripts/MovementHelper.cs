using Cysharp.Threading.Tasks;
using UnityEngine;

public static class MovementHelper
{
    public static async UniTask MoveTransformAsync(Transform transform, Vector3 targetPosition, float time)
    {
        if (transform == null)
            return;
        
        Vector3 initialPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            // Calculate interpolation factor (0 to 1)
            float t = elapsedTime / time;
            
            if (transform == null)
                return;
            // Update position with smooth interpolation
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            
            // Accumulate time and wait for next frame
            elapsedTime += Time.deltaTime;
            await UniTask.Yield();
        }

        // Ensure final position is exactly the target
        transform.position = targetPosition;
    }
    public static async UniTask MoveTransformAsyncUnscaled(Transform transform, Vector3 targetPosition, float time)
    {
        if (transform == null)
            return;
        
        Vector3 initialPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            // Calculate interpolation factor (0 to 1)
            float t = elapsedTime / time;

            if (transform == null)
                return;
            // Update position with smooth interpolation
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            
            // Accumulate time and wait for next frame
            elapsedTime += Time.unscaledDeltaTime;
            await UniTask.Yield();
        }

        // Ensure final position is exactly the target
        transform.position = targetPosition;
    }
}