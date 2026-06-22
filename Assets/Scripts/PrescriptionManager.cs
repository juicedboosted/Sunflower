using System.Collections.Generic;
using UnityEngine;

public class PrescriptionManager : MonoBehaviour
{
    [SerializeField] private GameObject m_PrescriptionPrefab;
    [SerializeField] private Transform m_ContentParent;

    public void DisplayPrescriptions(List<Prescription> _prescriptions)
    {
        foreach (Prescription prescription in _prescriptions)
        {

        }
    }
}