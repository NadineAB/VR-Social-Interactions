using UnityEngine;

/*
 * This class moves the object it is assigned to as a component to the current position of the mouse with no filter.
 * It's here to compare it's results to those obtained using filter.
 */
public class NoFilterMouseControl : MonoBehaviour
{
    /*
     * Move the object
     */
    void Update()
    {
        transform.localPosition = MousePositionWithOffset.GetOffsetMousePosition();
    }
}
