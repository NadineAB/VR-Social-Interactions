using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonEventHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler
{
    private Image m_Image = null;

    public Color32 m_NormalColor;
    public Color32 m_HoverColor;
    public Color32 m_DownColor;

    private void Awake()
    {
        m_Image = GetComponent<Image>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        m_Image.color = m_HoverColor;

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        m_Image.color = m_NormalColor;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        m_Image.color = m_DownColor;
     
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        print("up");
    }
    // called on click
    public void OnPointerClick (PointerEventData eventData)
    {
        m_Image.color = m_DownColor;

    }
}
