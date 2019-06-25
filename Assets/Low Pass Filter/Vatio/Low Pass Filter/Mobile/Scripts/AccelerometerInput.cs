using UnityEngine;
using Vatio.Filters;

/*
 * Example low pass filter input for MonoBehaviour behaviour
 * it needs to derive from ILowPassFilterInput interface
 * and it needs to know what type it should return
 */
public class AccelerometerInput : IFilterInput<Vector3>
{
    public Vector3 Get()
    {
        return AccelerometerWithOffset.GetOffsetAccelerometerData();
    }
}
