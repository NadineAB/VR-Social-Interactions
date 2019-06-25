using UnityEngine;

/*
 * This is a utility class
 * it is here to display a slider that users can use to change the smoothing value of the filter
 * it is not part of LowPassFilter usage
 */

public class Parameters : MonoBehaviour
{
    public static float a = 0.05F;

    Rect instructionRect;
    Rect aSliderRect;

    GUIStyle instructionStyle = null;

    /*
     * Initialize the positions
     */
    void Start()
    {
        instructionRect = new Rect(10, 10, 350, 20);
        aSliderRect = new Rect(10, 40, 350, 20);
    }

    /*
     * Initialize the style
     * Display the instruction and the slider
     */
    void OnGUI()
    {
        if (instructionStyle == null)
        {
            instructionStyle = new GUIStyle(GUI.skin.label);
            instructionStyle.normal.textColor = Color.black;
        }

        GUI.Label(instructionRect, "Smoothing factor (lesser values mean smoother moves):", instructionStyle);
        a = GUI.HorizontalSlider(aSliderRect, a, 0.01F, 1.0F);
    }
}
