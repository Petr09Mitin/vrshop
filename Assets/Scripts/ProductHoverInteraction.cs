using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRBaseInteractable))]
[RequireComponent(typeof(ProductData))]
public class ProductHoverInteraction : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] private GameObject popupPrefab;
    [SerializeField] private Vector3 offset = new Vector3(0, 0.3f, 0);

    [Header("Inputs")]
    // Только одна кнопка - для покупки. Открытие теперь автоматическое.
    [SerializeField] private InputActionProperty addToCartInput;

    private ProductData data;
    private XRBaseInteractable interactable;
    private GameObject currentPopup; // Ссылка на текущее окно

    private void Awake()
    {
        data = GetComponent<ProductData>();
        interactable = GetComponent<XRBaseInteractable>();

        interactable.hoverEntered.AddListener(OnHoverEntered);
        interactable.hoverExited.AddListener(OnHoverExited);
    }

    private void Update()
    {
        // Если окно открыто (значит мы смотрим на товар) И нажата кнопка покупки
        if (currentPopup != null && addToCartInput.action != null && addToCartInput.action.WasPressedThisFrame())
        {
            AddToCart();
        }
    }

    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        ShowPopup();
    }

    private void OnHoverExited(HoverExitEventArgs args)
    {
        HidePopup();
    }

    private void ShowPopup()
    {
        if (currentPopup != null) return;
        if (!popupPrefab) return;

        Vector3 spawnPos = transform.position + offset + (Camera.main.transform.position - transform.position).normalized * 0.1f;
        currentPopup = Instantiate(popupPrefab, spawnPos, Quaternion.identity);

        var ctrl = currentPopup.GetComponent<PopupController>();
        if (ctrl) ctrl.SetInfo(data.productName, data.price, data.description);
    }

    private void HidePopup()
    {
        if (currentPopup != null)
        {
            Destroy(currentPopup);
            currentPopup = null;
        }
    }

    private void AddToCart()
    {
        if (CartManager.Instance != null)
        {
            CartManager.Instance.AddToCart(data.productName, data.price);
        }
    }
}