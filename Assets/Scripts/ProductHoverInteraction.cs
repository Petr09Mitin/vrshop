using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRSimpleInteractable))]
[RequireComponent(typeof(ProductData))]
public class ProductHoverInteraction : MonoBehaviour
{
    [Header("Popup Configuration")]
    [SerializeField] private GameObject popupPrefab;
    [SerializeField] private Vector3 popupOffset = new Vector3(0, 0.3f, 0);

    private XRSimpleInteractable simpleInteractable;
    private ProductData productData;
    private GameObject currentPopup;

    private void Awake()
    {
        simpleInteractable = GetComponent<XRSimpleInteractable>();
        productData = GetComponent<ProductData>();
    }

    private void OnEnable()
    {
        simpleInteractable.hoverEntered.AddListener(OnHoverEntered);
        simpleInteractable.hoverExited.AddListener(OnHoverExited);
    }

    private void OnDisable()
    {
        simpleInteractable.hoverEntered.RemoveListener(OnHoverEntered);
        simpleInteractable.hoverExited.RemoveListener(OnHoverExited);
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
        if (currentPopup != null) return; // Already showing

        if (popupPrefab != null)
        {
            currentPopup = Instantiate(popupPrefab, transform.position + popupOffset, Quaternion.identity);
            
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

