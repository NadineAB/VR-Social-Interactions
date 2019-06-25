using UnityEngine;
using Vatio.Filters;

/*
 * This class moves the object it is assigned to as a component to the position calculated from accelerometer data and filtered using a low pass filter to smooth the moves.
 * For demonstration purposes it can use both the auto-update and normal modes.
 */
public class AccelerometerObjectControl : MonoBehaviour
{

    // Variable deciding if the script should use auto-update mode
    public bool shouldUseComponent = true;
    // Smoothing factor - the lower value, the more inertia
    public float a = 0.05f;

    AccelerometerControl accelerometerControl;
    LowPassFilter<Vector3> lowPassFilter;

    /*
     * If in auto-update, the function just looks for the wrapper of filter component and assigns it to a variable.
     * Otherwise it instantiates a low pass filter.
     */
    void Start()
    {
        if (shouldUseComponent)
        {
            accelerometerControl = GetComponent<AccelerometerControl>();
        }
        else
        {
            lowPassFilter = new LowPassFilter<Vector3>(a, Vector3.zero);
        }
    }

    /*
     * If in auto-update, the function just assigns current filtered value to local position.
     * Otherwise it inputs the current value to the filter and then assigns filtered value to local position.
     */
    void Update()
    {
        if (shouldUseComponent)
        {
            transform.localPosition = accelerometerControl.Get();
        }
        else
        {
            lowPassFilter.Append(AccelerometerWithOffset.GetOffsetAccelerometerData());
            transform.localPosition = lowPassFilter.Get();
            // or simply:
            // transform.localPosition = lowPassFilter.Append(AccelerometerWithOffset.GetOffsetAccelerometerData());
        }
    }
}
