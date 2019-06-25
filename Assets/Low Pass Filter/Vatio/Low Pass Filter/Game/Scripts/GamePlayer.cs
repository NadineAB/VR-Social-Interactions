using UnityEngine;

/*
 * This class is responsible for player movement
 */
public class GamePlayer : MonoBehaviour
{
    static Vector3 screenCenter;

    /*
     * Remember the screen center
     */
    void Start()
    {
        screenCenter = new Vector3(Screen.width / 2.0F, Screen.height / 2.0F, 0.0F);
    }

    /*
     * Move player
     */
    void Update()
    {
        transform.localPosition = GetOffsetMousePosition();
    }

    /*
     * Return mouse position in relation to the screen center and scaled
     */
    public static Vector3 GetOffsetMousePosition()
    {
        return (Input.mousePosition - screenCenter) / 40.0f;
    }
}
