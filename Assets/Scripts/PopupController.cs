using UnityEngine;
using TMPro;

public class PopupController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        
        // Fallback if Camera.main is not found (sometimes happens in VR setups depending on initialization)
        if (mainCamera == null)
        {
            mainCamera = FindObjectOfType<Camera>();
        }
    }

    private void LateUpdate()
    {
        // Billboard effect: Make the popup face the user
        if (mainCamera != null)
        {
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                             mainCamera.transform.rotation * Vector3.up);
        }
    }

    public void SetInfo(string name, float price, string description)
    {
        if (nameText != null) nameText.text = name;
        if (priceText != null) priceText.text = $"${price:F2}"; // Format price as currency
        if (descriptionText != null) descriptionText.text = description;
    }
}

