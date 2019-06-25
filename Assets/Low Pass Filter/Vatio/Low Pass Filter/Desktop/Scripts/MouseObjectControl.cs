using UnityEngine;
using Vatio.Filters;

/*
 * This class moves the object it is assigned to as a component to the current position of the mouse filtered using a low pass filter to smooth the moves.
 * For demonstration purposes it can use both the auto-update and normal modes.
 */
public class MouseObjectControl : MonoBehaviour
{

    // Variable deciding if the script should use auto-update mode
    public bool shouldUseComponent = true;
    // Smoothing factor - the lower value, the more inertia, in this example it is assigned from GUI slider
    float a = 0.05f;

    MouseControl mouseControl;
    LowPassFilter<Vector3> lowPassFilter;

    /*
     * If in auto-update, the function just looks for the wrapper of filter component and assigns it to a variable.
     * Otherwise it instantiates a low pass filter.
     */
    void Start()
    {
        if (shouldUseComponent)
        {
            mouseControl = GetComponent<MouseControl>();
        }
        else
        {
            lowPassFilter = new LowPassFilter<Vector3>(a, Vector3.zero);
        }
    }

    /*
     * If in auto-update, the function just assigns current filtered value to local position.
     * Otherwise it inputs the current mouse position to the filter and then assigns filtered value to local position.
     */
    void Update()
    {
        if (shouldUseComponent)
        {
            if (a != Parameters.a)
            {
                a = Parameters.a;
                mouseControl.SetA(a);
            }

            transform.localPosition = mouseControl.Get();
        }
        else
        {
            if (a != Parameters.a)
            {
                a = Parameters.a;
                lowPassFilter.A = a;
            }

            lowPassFilter.Append(MousePositionWithOffset.GetOffsetMousePosition());
            transform.localPosition = lowPassFilter.Get();
            // or simply:
            // transform.localPosition = lowPassFilter.Append(MousePositionWithOffset.GetOffsetMousePosition());
        }
    }
}
