using System.Collections.Generic;
using UnityEngine;
using System;

// Простая структура для хранения данных о товаре в корзине
[Serializable]
public class CartItem
{
    public string productName;
    public float price;
}

public class CartManager : MonoBehaviour
{
    public static CartManager Instance { get; private set; }

    // Список товаров в корзине
    public List<CartItem> cartItems = new List<CartItem>();

    // Событие, на которое подпишется UI, чтобы обновиться при изменениях
    public event Action OnCartUpdated;

    private void Awake()
    {
        // Реализация паттерна Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddToCart(string name, float price)
    {
        CartItem newItem = new CartItem { productName = name, price = price };
        cartItems.Add(newItem);

        Debug.Log($"Added to cart: {name} for ${price}");

        // Уведомляем всех подписчиков (UI), что корзина изменилась
        OnCartUpdated?.Invoke();
    }

    public float GetTotalPrice()
    {
        float total = 0;
        foreach (var item in cartItems)
        {
            total += item.price;
        }
        return total;
    }

    public void Checkout()
    {
        if (cartItems.Count == 0) return;

        Debug.Log("Checkout success! Total: $" + GetTotalPrice());

        // Очистка корзины
        cartItems.Clear();
        OnCartUpdated?.Invoke();
    }
}