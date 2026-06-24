using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableTask : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //task info
    public string m_taskName;
    public int m_energyCost;
    public TMP_Text m_taskText;

    //visual energy icons
    public Image[] m_energyBolts;

    public bool m_hasSpentEnergy = false;
    
    private Canvas m_canvas;
    private RectTransform m_rectTransform;
    private CanvasGroup m_canvasGroup;
    private Transform m_originalParent;

    // cache ui when created
    void Awake()
    {
        m_rectTransform = GetComponent<RectTransform>();
        m_canvasGroup = GetComponent<CanvasGroup>();
        m_canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData _eventData)
    {
        Debug.Log("Dragging: " + gameObject.name);
        m_originalParent = transform.parent;

        //move task to top canvas layer
        transform.SetParent(m_canvas.transform, true);
        transform.SetAsLastSibling();
        m_canvasGroup.blocksRaycasts = false;
    }

    //move task with cursor
    public void OnDrag(PointerEventData _eventData)
    {
        m_rectTransform.anchoredPosition += _eventData.delta / m_canvas.scaleFactor;

    }

    //move task back to original position if movement is not valid
    public void OnEndDrag(PointerEventData _eventData)
    {
        m_canvasGroup.blocksRaycasts = true;
        if (transform.parent == m_canvas.transform)
        {
            transform.SetParent(m_originalParent);
            m_rectTransform.anchoredPosition = Vector2.zero;
        }
    }

    //assign task details
    public void SetTask(string _name, int _cost)
    {
        m_taskName = _name;
        m_energyCost = _cost;
        m_taskText.text = m_taskName;
        UpdateEnergyIcons();
    }

    //display energy cost using icons
    public void UpdateEnergyIcons()
    {
        int bolts = m_energyCost / 10;
        bolts = Mathf.Clamp(bolts, 0, m_energyBolts.Length);
        for (int i = 0; i < m_energyBolts.Length; i++)
        {
            m_energyBolts[i].gameObject.SetActive(i < bolts);
        }
    }
}
