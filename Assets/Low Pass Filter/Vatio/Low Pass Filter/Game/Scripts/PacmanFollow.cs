using UnityEngine;
using Vatio.Filters;

/*
 * This class makes the Pacman follow the player
 */
public class PacmanFollow : MonoBehaviour
{

    public Transform target;
    public float alpha = 0.03F;
    LowPassFilter<Vector3> targetPositionFilter;

    /*
     * Initialize filter
     */
    void Start()
    {
        targetPositionFilter = new LowPassFilter<Vector3>(alpha, transform.position);
    }

    /*
     * Move toward the player using filter
     */
    void Update()
    {
        transform.position = targetPositionFilter.Append(target.position);
    }
}
