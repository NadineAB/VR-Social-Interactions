/* Introduction Instruction
// Laser pointer to interact with UI of the beginning of the experiment
// Nadine Abu Rumman, UCL 2019 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VRClickLaserPointer : MonoBehaviour
{
    public float m_DefaultLength = 50.0f;
    public GameObject m_endofPointer;
    public VRInputCanvas m_Input;
    private LineRenderer m_lineRenderer = null;

    private void Awake()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        // Use default or distance
        PointerEventData data = m_Input.GetData();
        float target = data.pointerPressRaycast.distance == 0 ? m_DefaultLength : data.pointerPressRaycast.distance; 
        // Raycast
        RaycastHit hit = CreateRaycast(target);
        // Default
        Vector3 endPosition = transform.position + (transform.forward * target);
        // or based on hit
        if (hit.collider != null)
            endPosition = hit.point;
        // Set position of the dot
        m_endofPointer.transform.position = endPosition;
        // Set linerender
        m_lineRenderer.SetPosition(0, transform.position);
        m_lineRenderer.SetPosition(1, endPosition);
    }
    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, m_DefaultLength);
         return hit;


    }

}
