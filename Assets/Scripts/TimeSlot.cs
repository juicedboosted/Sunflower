using UnityEngine;
using UnityEngine.EventSystems;

public class TimeSlot : MonoBehaviour, IDropHandler
{
   public TrackerManager m_trackerManager;
   public void OnDrop(PointerEventData _eventData)
    {
        GameObject taskObject = _eventData.pointerDrag;
        if (taskObject == null)
        {
            return;
        }
        DraggableTask dragTask = taskObject.GetComponent<DraggableTask>();
        if (dragTask == null)
        {
            return;
        }
        if (m_trackerManager == null)
        {
            Debug.LogError("No TrackerManager on " + gameObject.name);
            return;
        }
        Debug.Log("Dropped " + dragTask.m_taskName + " -" + dragTask.m_energyCost);

        if (m_trackerManager.SpendEnergy(dragTask.m_energyCost))
        {
            m_trackerManager.AddScheduledTask();
            taskObject.transform.SetParent(transform, false);
            taskObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }
}
