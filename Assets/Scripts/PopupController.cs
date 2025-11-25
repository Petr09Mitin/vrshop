using UnityEngine;
using TMPro;

public class PopupController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (mainCamera != null)
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }

    public void SetInfo(string n, float p, string d)
    {
        if (nameText) nameText.text = n;
        if (priceText) priceText.text = $"${p:F2}";
        if (descriptionText) descriptionText.text = d;
    }
}