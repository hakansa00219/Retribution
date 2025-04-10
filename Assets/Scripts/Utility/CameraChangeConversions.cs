using UnityEngine;

public class CameraChangeConversions
{
    public static Vector3 CameraChangeConversion_Position(CamType fromType, CamType toType, Vector3 position)
    {
        Vector3 convertedPosition = Vector3.zero;

        //Ortho (x, 2, z)
        //Side  (0, y, z)
        switch ((fromType, toType))
        {
            case (CamType.Orthographic, CamType.Side):
                convertedPosition = new Vector3(0, position.y, position.z);
                break;
            case (CamType.Side, CamType.Orthographic):
                
                break;
            default:
                break;
        }
        return convertedPosition;
    }
    public static Vector3 CameraChangeConversion_Rotation(CamType fromType, CamType toType, Vector3 rotation)
    {
        Vector3 convertedRotation = Vector3.zero;

        //Ortho (x, 2, z)
        //Side  (0, y, z)
        switch ((fromType, toType))
        {
            case (CamType.Orthographic, CamType.Side):
                convertedRotation = new Vector3(rotation.y, 0, 90);
                break;
            case (CamType.Side, CamType.Orthographic):
                
                break;
            default:
                break;
        }
        return convertedRotation;
    }
}