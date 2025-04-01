using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public int price = 1;
    private int quantity = 0;
    public TextMeshProUGUI quantityText;

    public HealthManager playerHealth; // Reference to player's health

    public void IncreaseQuantity()
    {
        quantity++;
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClickSound);
        UpdateUI();
    }

    public void DecreaseQuantity()
    {
        if (quantity > 0)
        {
            quantity--;
            AudioManager.instance.PlaySFX(AudioManager.instance.buttonClickSound);
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

    public int GetQuantity()
    {
        return quantity;
    }

    public void ConfirmPurchase()
    {
        if (quantity > 0)
        {
            int healAmount = quantity * 20; // Heal 10 HP per potion

            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount);
                Debug.Log("Healed for " + healAmount + " HP. Current health: " + playerHealth.GetCurrentHealth());
            }

            Debug.Log("Bought " + quantity + " potions for " + GetTotalCost() + " coins.");

            quantity = 0;
            AudioManager.instance.PlaySFX(AudioManager.instance.buttonClickSound);
            UpdateUI();
        }
    }


}
