using UnityEngine;

/*
 * This is a utility class
 * it is designed to transform mouse input to a usable and pretty-to-display value
 * it is not part of LowPassFilter usage
 */

public class MousePositionWithOffset : MonoBehaviour
{

    static Vector3 screenCenter;

    /*
     * Remember where screen center is
     */
    void Start()
    {
        screenCenter = new Vector3(Screen.width / 2.0F, Screen.height / 2.0F, 0.0F);
    }

    /*
     * Transform mouse position into a usable value
     * and return it as a Vector3
     * The Vector3 class is used here, because native Unity variable "Input.mousePosition" is of this type
     * z value is unused here though and set to 0
     */
    public static Vector3 GetOffsetMousePosition()
    {
        return (Input.mousePosition - screenCenter) / 50.0f;
    }
}
