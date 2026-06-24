using UnityEngine;
using UnityEngine.EventSystems;

public class TimeSlot : MonoBehaviour, IDropHandler
{
   public TrackerManager m_trackerManager;

    //call when task is dropped on calendar time slot
    public void OnDrop(PointerEventData _eventData)
    {
        GameObject taskObject = _eventData.pointerDrag;

        //make sure task actually exists and is valid
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

        //only spent energy the first time the task is scheduled
        if (dragTask.m_hasSpentEnergy == false)
        {
            //check if player has enough energy for the task
            if (m_trackerManager.SpendEnergy(dragTask.m_energyCost))
            {
                //record the task and remove energy
                m_trackerManager.AddScheduledTask();
                dragTask.m_hasSpentEnergy = true;
                MoveTask(taskObject);
            }
        }
        else
        {
            //allow the scheduled tasks to move freely between slots
            MoveTask(taskObject);
        }
    }

//puts task inside time slot
private void MoveTask(GameObject _taskObject)
    {
        _taskObject.transform.SetParent(transform, false);
        //centre the task
        _taskObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
}
