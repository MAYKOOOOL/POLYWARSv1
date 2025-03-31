using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public int price = 5; 
    private int quantity = 0; 
    public TextMeshProUGUI quantityText;

    public void IncreaseQuantity()
    {
        quantity++;
        UpdateUI();
    }

    public void DecreaseQuantity()
    {
        if (quantity > 0)
        {
            quantity--;
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        quantityText.text = quantity.ToString();
    }

    public int GetTotalCost()
    {
        return quantity * price;
    }

    public void ConfirmPurchase()
    {
        Debug.Log("Bought " + quantity + " items for " + GetTotalCost() + " coins.");
        quantity = 0;
        UpdateUI();
    }
}
