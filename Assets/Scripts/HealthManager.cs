using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] GameObject m_mainPage;
    [SerializeField] GameObject m_bookingPage;
    [SerializeField] GameObject m_prescriptionPage;
    [SerializeField] GameObject m_virtualApptPage;

    [SerializeField] AppManager appManager;
    [SerializeField] CalendarManager calendarManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        OpenMainPage();
    }

    public void OpenMainPage()
    {
        m_mainPage.SetActive(true);
        m_bookingPage.SetActive(false);
        m_prescriptionPage.SetActive(false);
    }

    public void OpenBookingPage()
    {
        m_mainPage.SetActive(false);
        m_bookingPage.SetActive(true);
        m_prescriptionPage.SetActive(false);
        m_virtualApptPage.SetActive(false);
    }

    public void OpenPrescPage()
    {
        m_mainPage.SetActive(false);
        m_bookingPage.SetActive(false);
        m_prescriptionPage.SetActive(true);
        m_virtualApptPage.SetActive(false);
    }
    public void StartAppt()
    {
        if (calendarManager != null && calendarManager.HasAppointmentScheduled())
        {
            m_mainPage.SetActive(false);
            m_bookingPage.SetActive(false);
            m_prescriptionPage.SetActive(false);
            m_virtualApptPage.SetActive(true);
        }
        else
        {
            Debug.Log("No appointment scheduled today.");
        }
    }

    // If the health app is open & not on the main page, the back button will go to the main health page rather than closing the app.
    public void CloseApp()
    {
        if (m_bookingPage.activeSelf || m_prescriptionPage.activeSelf)
        {
            OpenMainPage();
        }
        else if (m_virtualApptPage.activeSelf)
        {
            OpenBookingPage();
        }
        else
        {
            appManager.CloseApp();
        }
    }

    public void BookDrAppointment()
    {
        calendarManager.AddTomorrowTask("Appointment", 40, TaskType.Appointment);
    }
}