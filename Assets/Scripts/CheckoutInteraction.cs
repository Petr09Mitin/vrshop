using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRSimpleInteractable))]
public class CheckoutInteraction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CartUIController cartUIController;
    
    private XRSimpleInteractable interactable;

    private void Awake()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        
        // Listen for the Select event (triggered by Ray Interactor's Select Action, e.g., Trigger)
        interactable.selectEntered.AddListener(OnSelectEntered);
    }

    private void OnDestroy()
    {
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnSelectEntered);
        }
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        // This method runs when the user clicks the trigger while pointing at this object
        PerformCheckout();
    }

    private void PerformCheckout()
    {
        if (cartUIController != null)
        {
            Debug.Log("Checkout Button Pressed!");
            cartUIController.OnCheckoutButtonClicked();
        }
        else
        {
            Debug.LogWarning("CartUIController is not assigned in CheckoutInteraction!");
        }
    }
}
