using UnityEngine;

/*
 * This is a utility class
 * it is designed to transform accelerometer data to a usable and pretty-to-display value
 * it is not part of LowPassFilter usage
 */
public class AccelerometerWithOffset : MonoBehaviour
{
    /*
     * Transform accelerometer data into a usable value
     * and return it as a Vector3
     */
    public static Vector3 GetOffsetAccelerometerData()
    {
        return Input.acceleration * 2.0F;
    }
}
