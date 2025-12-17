using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRBaseInteractable))]
public class CheckoutInteraction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CartUIController cartUIController;
    
    [Header("Inputs")]
    [SerializeField] private InputActionProperty checkoutInput;

    private XRBaseInteractable interactable;
    private bool isHovering = false;

    private void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
        
        // Listen for hover events to know when the ray is pointing at the button
        interactable.hoverEntered.AddListener(OnHoverEntered);
        interactable.hoverExited.AddListener(OnHoverExited);
    }

    private void Update()
    {
        // Check if we are hovering AND the trigger button is pressed
        if (isHovering && checkoutInput.action != null && checkoutInput.action.WasPressedThisFrame())
        {
            PerformCheckout();
        }
    }

    private void PerformCheckout()
    {
        if (cartUIController != null)
        {
            cartUIController.OnCheckoutButtonClicked();
        }
        else
        {
            Debug.LogWarning("CartUIController is not assigned in CheckoutInteraction!");
        }
    }

    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        isHovering = true;
    }

    private void OnHoverExited(HoverExitEventArgs args)
    {
        isHovering = false;
    }
}
