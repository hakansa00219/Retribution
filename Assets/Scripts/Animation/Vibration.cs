using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Vibration
{
    private Rigidbody _owner;
    private Vector3 _originalPosition;
    private readonly float _vibrationDuration;
    private readonly float _vibrationIntensity;
    private float _elapsedTime;
    
    public Vibration(Rigidbody owner, float vibrationDuration, float vibrationIntensity)
    {
        _owner = owner;
        _vibrationDuration = vibrationDuration;
        _vibrationIntensity = vibrationIntensity;
    }
    
    public IEnumerator Vibrate()
    {
        _originalPosition = _owner.position;
        
        while (_elapsedTime < _vibrationDuration)
        {
            _elapsedTime += Time.deltaTime;

            // Generate a random offset within the intensity range
            Vector3 randomOffset = new Vector3(
                Random.Range(-_vibrationIntensity, _vibrationIntensity),
                Random.Range(-_vibrationIntensity, _vibrationIntensity),
                Random.Range(-_vibrationIntensity, _vibrationIntensity)
            );

            // Apply the offset to the object's position
            _owner.MovePosition(_originalPosition + randomOffset);

            yield return null;
        }

        // Reset the object's position to its original state
        _owner.MovePosition(_originalPosition);
    }

}