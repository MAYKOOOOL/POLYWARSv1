using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public TextMeshProUGUI coinText; // UI element to show player's coins
    public int playerCoins = 100; 

    public ShopItem[] shopItems;
    public GameObject shopUI;
    public GameObject[] otherUIElements;

    private void Start()
    {
        shopUI.SetActive(false);
        UpdateCoinUI();
    }

    public void OpenShop()
    {
        UpdateCoinUI();
        shopUI.SetActive(true);
        ToggleOtherUI(false);
    }

    public void CloseShop()
    {
        shopUI.SetActive(false);
        ToggleOtherUI(true);
    }

    private void ToggleOtherUI(bool state)
    {
        foreach (GameObject uiElement in otherUIElements)
        {
            if (uiElement != null)
                uiElement.SetActive(state);
        }
    }

    public void UpdateCoinUI()
    {
        playerCoins = CoinManager.Instance.currentCoins; // Get updated coin count
        coinText.text = "Coins: " + playerCoins;
    }

    public void TryPurchase()
    {
        int totalCost = 0;

        foreach (ShopItem item in shopItems)
        {
            totalCost += item.GetTotalCost();
        }

        if (playerCoins >= totalCost)
        {
            playerCoins -= totalCost;

            foreach (ShopItem item in shopItems)
            {
                item.ConfirmPurchase();
            }

            Debug.Log("Purchase successful!");
        }
        else
        {
            Debug.Log("Not enough coins!");
        }

        UpdateCoinUI();
    }
}
