using UnityEngine;
using Vatio.Filters;

/*
 * This class controls the camera
 */
public class GameCamera : MonoBehaviour
{

    static Vector3 screenCenter;
    public float alpha = 0.15F;
    LowPassFilter<Vector3> cameraPositionFilter;

    /*
     * Initialize values
     */
    void Start()
    {
        screenCenter = new Vector3(Screen.width / 2.0F, Screen.height / 2.0F, 0.0F);
        cameraPositionFilter = new LowPassFilter<Vector3>(alpha, GetOffsetMousePosition());
    }

    /*
     * Move the camera
     */
    void Update()
    {
        transform.localPosition = cameraPositionFilter.Append(GetOffsetMousePosition());
    }

    /*
     * Return mouse position in relation to the screen center and scaled
     */
    public static Vector3 GetOffsetMousePosition()
    {
        return (Input.mousePosition - screenCenter) / 100.0f;
    }
}
