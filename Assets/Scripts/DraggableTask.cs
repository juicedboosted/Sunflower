using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableTask : MonoBehaviour
{
    private Canvas m_canvas;
    private RectTransform m_rectTransform;
    private CanvasGroup m_canvasGroup;
    private Transform m_originalParent;


    void Awake()
    {
        m_rectTransform = GetComponent<RectTransform>();
        m_canvasGroup = GetComponent<CanvasGroup>();
        m_canvas = GetComponent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData _eventData)
    {
        m_originalParent = transform.parent;
        transform.SetParent(m_canvas.transform);
        m_canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData _eventData)
    {
        m_rectTransform.anchoredPosition += _eventData.delta / m_canvas.scaleFactor;

    }

    public void OnEndDrag(PointerEventData _eventData)
    {
        m_canvasGroup.blocksRaycasts = true;
        if (transform.parent == m_canvas.transform)
        {
            transform.SetParent(m_originalParent);
            m_rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}
