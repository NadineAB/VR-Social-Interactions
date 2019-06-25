using UnityEngine;
using Vatio.Filters;

/*
 * This class smoothly rotates the Pacman so it looks in the direction of the player.
 */
public class PacmanLookAt : MonoBehaviour
{

    public Transform target;
    public float alpha = 0.05F;
    LowPassFilter<Quaternion> rotationFilter;
    Vector3 negativeZ = new Vector3(0.0f, 0.0f, -0.1f);

    /*
     * Initialize the filter
     */
    void Start()
    {
        rotationFilter = new LowPassFilter<Quaternion>(alpha, Quaternion.identity);
    }

    /*
     * Smoothly rotate toward the player
     */
    void Update()
    {
        transform.localRotation = rotationFilter.Append(Quaternion.LookRotation(negativeZ + target.position - transform.position)); // Z set to negative so the Pacman will rotate the right way (transition going through forward, not backward)
    }
}
