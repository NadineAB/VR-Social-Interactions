using UnityEngine;

/*
 * This class moves the object it is assigned to as a component to the position calculated from accelerometer data with no filter.
 * It's here to compare it's results to those obtained using filter.
 */
public class NoFilterAccelerometerControl : MonoBehaviour
{

    /*
     * Move the object
     */
    void Update()
    {
        transform.localPosition = AccelerometerWithOffset.GetOffsetAccelerometerData();
    }
}
