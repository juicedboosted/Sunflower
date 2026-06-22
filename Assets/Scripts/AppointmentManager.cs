using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AppointmentManager : MonoBehaviour
{
    // pick 2 random dialogue options, set them to be button text
    // handle player selection
    // display response for selected dialogue
    // add prescriptions
    // close dialogue (return to booking appointments page)

    [SerializeField] private List<AppointmentDialogueOption> m_DialogueOptions;

    [SerializeField] private TMP_Text m_Option1Text;
    [SerializeField] private TMP_Text m_Option2Text;
    [SerializeField] private TMP_Text m_DrResponseText;

    [SerializeField] private Button m_Option1Button;
    [SerializeField] private Button m_Option2Button;
    [SerializeField] private Button m_OkEndButton;

    [SerializeField] private List<Prescription> m_Prescriptions = new List<Prescription>();

    [SerializeField] private HealthManager m_HealthManager;

    private int m_Option1Index;
    private int m_Option2Index;

    private void OnEnable()
    {
        StartDialogue();
    }

    public void StartDialogue()
    {
        m_Option1Button.gameObject.SetActive(true);
        m_Option2Button.gameObject.SetActive(true);
        m_OkEndButton.gameObject.SetActive(false);

        m_DrResponseText.text = "Hi, what symptoms have you been experiencing recently?";

        SelectTwoOptions();
        SetButtonText();
    }

    private void SelectTwoOptions()
    {
        List<int> m_OptionIndices = new List<int>();

        // Add all the dialogue button options
        for (int i = 0; i < m_DialogueOptions.Count; i++)
        {
            m_OptionIndices.Add(i);
        }

        // Remove dialogue option from the list once it's been selected
        m_Option1Index = m_OptionIndices[Random.Range(0, m_OptionIndices.Count)];
        m_OptionIndices.Remove(m_Option1Index);

        // Select another dialogue option for button 2
        m_Option2Index = m_OptionIndices[Random.Range(0, m_OptionIndices.Count)];
    }

    private void SetButtonText()
    {
        m_Option1Text.text = m_DialogueOptions[m_Option1Index].m_optionText;
        m_Option2Text.text = m_DialogueOptions[m_Option2Index].m_optionText;
    }

    public void SelectOption1()
    {
        HandleDialogueSelection(m_Option1Index);
    }

    public void SelectOption2()
    {
        HandleDialogueSelection(m_Option2Index);
    }

    private void HandleDialogueSelection(int _index)
    {
        AppointmentDialogueOption selectedDialogue = m_DialogueOptions[_index];

        m_DrResponseText.text = selectedDialogue.m_drResponseText;

        TryAddPrescription(selectedDialogue);

        m_Option1Button.gameObject.SetActive(false);
        m_Option2Button.gameObject.SetActive(false);

        m_OkEndButton.gameObject.SetActive(true);
        m_OkEndButton.gameObject.GetComponentInChildren<TMP_Text>().text = "Ok.";
    }

    private void TryAddPrescription(AppointmentDialogueOption _option)
    {
        if (_option.m_prescriptionName == null || _option.m_prescriptionName == "")
        {
            return; // don't do anything if no prescription is assigned to that dialogue option (symptom)
        }

        // Check if the item already exists in the list
        foreach (Prescription p in m_Prescriptions)
        {
            if (p.m_PrescriptionName == _option.m_prescriptionName)
            {
                return; // stop here if item is already in list
            }
        }

        Prescription newPrescription = new Prescription();
        newPrescription.m_PrescriptionName = _option.m_prescriptionName;
        newPrescription.m_PrescriptionQuantity = _option.m_prescriptionQuantity;

        m_Prescriptions.Add(newPrescription);
    }

    public void CloseAppt()
    {
        m_HealthManager.OpenBookingPage();
        Debug.Log("ok was pressed");
    }

    public List<Prescription> GetPrescriptions()
    {
        return m_Prescriptions;
    }
}