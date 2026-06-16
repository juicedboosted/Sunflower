using UnityEngine;
using UnityEngine.EventSystems;

public class TimeSlot : MonoBehaviour, IDropHandler
{
   public void OnDrop(PointerEventData _eventData)
    {
        GameObject task = _eventData.pointerDrag;
        if (task != null)
        {
            task.transform.SetParent(transform);
            task.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }
}
