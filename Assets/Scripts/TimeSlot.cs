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

        if (dragTask.m_hasSpentEnergy == false)
        {
            if (m_trackerManager.SpendEnergy(dragTask.m_energyCost))
            {
                m_trackerManager.AddScheduledTask();
                dragTask.m_hasSpentEnergy = true;
                MoveTask(taskObject);
                dragTask.m_isScheduled = true;
            }
        }
        else
        {
            MoveTask(taskObject);
        }
    }

    private void MoveTask(GameObject _taskObject)
    {
        _taskObject.transform.SetParent(transform, false);
        _taskObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        DraggableTask task = _taskObject.GetComponent<DraggableTask>();
        if (task != null)
        {
            task.m_isScheduled = true;
        }
    }
}
