using UnityEngine;
using TMPro;
using System.Text;

public class CartUIController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI cartListText; // Обычный текст для списка (простой вариант)
    [SerializeField] private TextMeshProUGUI totalPriceText;

    private void Start()
    {
        // Подписываемся на обновления корзины
        if (CartManager.Instance != null)
        {
            CartManager.Instance.OnCartUpdated += UpdateUI;
            UpdateUI(); // Первичное обновление
        }
    }

    private void OnDestroy()
    {
        if (CartManager.Instance != null)
        {
            CartManager.Instance.OnCartUpdated -= UpdateUI;
        }
    }

    // Метод для кнопки "Checkout" (назначить через инспектор Unity)
    public void OnCheckoutButtonClicked()
    {
        if (CartManager.Instance != null)
        {
            CartManager.Instance.Checkout();
        }
    }

    private void UpdateUI()
    {
        if (CartManager.Instance == null) return;

        // 1. Обновляем список товаров
        StringBuilder sb = new StringBuilder();
        if (CartManager.Instance.cartItems.Count == 0)
        {
            sb.AppendLine("Cart is empty");
        }
        else
        {
            foreach (var item in CartManager.Instance.cartItems)
            {
                sb.AppendLine($"- {item.productName} \t ${item.price:F2}");
            }
        }

        if (cartListText != null) cartListText.text = sb.ToString();

        // 2. Обновляем итоговую сумму
        if (totalPriceText != null)
        {
            float total = CartManager.Instance.GetTotalPrice();
            totalPriceText.text = $"Total: ${total:F2}";
        }
    }
}