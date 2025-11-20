using UnityEngine;

public class ProductData : MonoBehaviour
{
    [Header("Product Information")]
    public string productName = "New Product";
    public float price = 1.99f;
    [TextArea]
    public string description = "A brief description of the product.";
}

