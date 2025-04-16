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
    
    public static async UniTask MoveCircleTransformAsync(Transform transform, CamType camType, float speed)
    {
        if (transform == null)
            return;
        
        Vector3 initialPosition = transform.position;
        float radius = 1f;
        float angle = 0f;

        while (true)
        {
            if (transform == null)
                return;
            
            angle += speed * Time.deltaTime;
            if (angle >= Mathf.PI * 2f) angle -= Mathf.PI * 2f;

            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            
            transform.position = initialPosition + (camType is CamType.Orthographic ? new Vector3(x, 0f, y) : new Vector3(0f, x, y));
            
            await UniTask.Yield();
        }
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