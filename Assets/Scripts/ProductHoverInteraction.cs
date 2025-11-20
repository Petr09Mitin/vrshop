using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRBaseInteractable))]
[RequireComponent(typeof(ProductData))]
public class ProductHoverInteraction : MonoBehaviour
{
    [Header("Popup Configuration")]
    [SerializeField] private GameObject popupPrefab;
    [SerializeField] private Vector3 popupOffset = new Vector3(0, 0.3f, 0);

    private XRBaseInteractable interactable;
    private ProductData productData;
    private GameObject currentPopup;

    private void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
        productData = GetComponent<ProductData>();
    }

    private void OnEnable()
    {
        interactable.hoverEntered.AddListener(OnHoverEntered);
        interactable.hoverExited.AddListener(OnHoverExited);
    }

    private void OnDisable()
    {
        interactable.hoverEntered.RemoveListener(OnHoverEntered);
        interactable.hoverExited.RemoveListener(OnHoverExited);
    }

    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        // Check if it is the Right Controller (Interactor)
        // This assumes the interactor GameObject has "Right" in its name
        // Common naming: "Right Ray Interactor", "Right Controller", etc.

        if (args.interactorObject.transform.name.Contains("Right") || args.interactorObject.transform.parent.name.Contains("Right"))
        {
            ShowPopup();
        }
    }

    private void OnHoverExited(HoverExitEventArgs args)
    {
        // We can destroy the popup when the ray leaves
        // Or check if it was the right hand leaving (but generally safer to just hide if any exit to avoid stuck UI)
        if (args.interactorObject.transform.name.Contains("Right") || args.interactorObject.transform.parent.name.Contains("Right"))
        {
            HidePopup();
        }
    }

    private void ShowPopup()
    {
        if (currentPopup != null) return;

        if (popupPrefab != null)
        {
            // Calculate position: Start at product center
            Vector3 spawnPos = transform.position;

            // Add the offset (Upwards)
            spawnPos += popupOffset;

            // OPTIONAL: Move it slightly towards the player (Camera) so it doesn't clip into shelves
            Vector3 directionToCamera = (Camera.main.transform.position - transform.position).normalized;
            spawnPos += directionToCamera * 0.1f; // Move 10cm towards the player

            currentPopup = Instantiate(popupPrefab, spawnPos, Quaternion.identity);

            PopupController controller = currentPopup.GetComponent<PopupController>();
            if (controller != null)
            {
                controller.SetInfo(productData.productName, productData.price, productData.description);
            }
        }
        else
        {
            Debug.LogWarning("ProductHoverInteraction: Popup Prefab is missing!");
        }
    }

    private void HidePopup()
    {
        if (currentPopup != null)
        {
            Destroy(currentPopup);
            currentPopup = null;
        }
    }
}

