using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VRInputCanvas : BaseInputModule
{
    public Camera m_Camera;
    public SteamVR_Input_Sources m_TargetSource;
    public SteamVR_Action_Boolean m_clickAction;

    private GameObject m_CurrentObject = null;
    private PointerEventData m_Data = null;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        m_Data = new PointerEventData(eventSystem);
    }

    // Update is called once per frame
    public override void Process()
    {

        // Reset data, set camera
        m_Data.Reset();
        m_Data.position = new Vector2(m_Camera.pixelWidth/2 , m_Camera.pixelHeight / 2);
        // Raycast
        eventSystem.RaycastAll(m_Data, m_RaycastResultCache);
        m_Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        m_CurrentObject = m_Data.pointerCurrentRaycast.gameObject;
        // Clear 
        m_RaycastResultCache.Clear();
        // Hover
        HandlePointerExitAndEnter(m_Data, m_CurrentObject);

        // Press 
        if (m_clickAction.GetStateDown(m_TargetSource))
        {
            ProcessPress(m_Data);
        }

        // Release 
        if (m_clickAction.GetStateDown(m_TargetSource))
        {
            ProcessRelease(m_Data);
        }

    }
    public PointerEventData GetData()
    {
        return m_Data;
    }
    public void ProcessPress (PointerEventData data)
    {
        // Set raycast
        data.pointerPressRaycast = data.pointerCurrentRaycast;
        // Check for object hit, get the down handler , call
        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(m_CurrentObject, data, ExecuteEvents.pointerClickHandler);
        // if no down handler, try and get click handler
        if (newPointerPress = null)
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject);
        // Set data
        data.pressPosition = data.position;
        data.rawPointerPress = m_CurrentObject;


    }
    public void ProcessRelease(PointerEventData data)
    {
        // excute pointer up
        ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);
        // check for click handler
        GameObject  pointerUpHandler= ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject);

        // check if actual 
        if(data.pointerPress== pointerUpHandler)
        {

            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);
        }

        // clear selected gameobject
        eventSystem.SetSelectedGameObject(null);
        // Rset data
        data.pressPosition = Vector2.zero;
        data.pointerPress = null;
        data.rawPointerPress = null;
    }
}
