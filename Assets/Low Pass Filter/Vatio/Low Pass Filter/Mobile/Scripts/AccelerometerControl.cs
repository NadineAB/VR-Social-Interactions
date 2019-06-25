using UnityEngine;

/*
 * This class is a wrapper for the filter MonoBehaviour component.
 * It's not really needed, as You can do the same from just any script, but is here for the clarity of code.
 * For the same purpose You may want to use a similar class.
 */

public class AccelerometerControl : MonoBehaviour
{

    LowPassFilterMonoBehaviour lowPassFilterMonoBehaviour;

    /*
     * In the Start function we initialize the Filter MonoBehaviour component that will the auto-update variable being filtered.
     * Then we assign initial values to it: Input script, that supplies the filter with input values, alpha parameter and initial value for the filter.
     */
    void Start()
    {
        float a = GetComponent<AccelerometerObjectControl>().a; // a hack to easily set value from the AccelerometerObjectControl component - don't do this, You should specify it somewhere instead, perhaps in a config class
        lowPassFilterMonoBehaviour = gameObject.AddComponent<LowPassFilterMonoBehaviour>();
        lowPassFilterMonoBehaviour.Set<Vector3>(new AccelerometerInput(), a, Vector3.zero);
    }

    /*
     * This is a getter for the current filtered value of the filter, it returns the value invoking a similar method in the filter MonoBehaviour component.
     */
    public Vector3 Get()
    {
        return lowPassFilterMonoBehaviour.Get<Vector3>();
    }

    /*
     * This is the setter for the smoothing factor, it sends the new value to the filter MonoBehaviour component, so it can set the new value in the filter.
     */
    public void SetA(float a)
    {
        lowPassFilterMonoBehaviour.SetA(a);
    }
}
