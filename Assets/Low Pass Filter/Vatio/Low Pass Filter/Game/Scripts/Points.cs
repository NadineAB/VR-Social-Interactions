using UnityEngine;
using Vatio.Filters;

/*
 * This class is responsible for counting and displaying points.
 * It uses low pass filter to make the effect of gradually increasing the points.
 */

public class Points : MonoBehaviour
{

    public TextMesh pointsText;
    public Transform player;
    public Transform pacman;

    public float alpha = 0.05f;

    float points = 0.0f;
    LowPassFilter<float> pointsFilter;

    /*
     * Initialize values
     */
    void Start()
    {
        Reset();
    }

    /*
     * Add points number equal to the square power of distance
     */
    void Update()
    {
        float distance = (player.position - pacman.position).magnitude;
        points += distance * distance / 500.0f;
        pointsText.text = ((int)(pointsFilter.Append(points))).ToString() + " POINTS";
    }

    /*
     * Reset to initial values
     */
    internal void Reset()
    {
        points = 0.0f;
        pointsFilter = new LowPassFilter<float>(alpha, points);
        pointsText.text = ((int)(pointsFilter.Append(points))).ToString() + " POINTS";
    }
}
