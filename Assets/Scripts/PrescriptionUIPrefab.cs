using TMPro;
using UnityEngine;

public class PrescriptionUIPrefab : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text quantityText;

    public void Setup(Prescription p)
    {
        nameText.text = p.m_PrescriptionName;
        quantityText.text = p.m_PrescriptionQuantity;
    }
}