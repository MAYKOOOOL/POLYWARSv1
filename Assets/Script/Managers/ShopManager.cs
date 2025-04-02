using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public TextMeshProUGUI shopCoinText; // UI for coins in the shop
    public TextMeshProUGUI inGameCoinText; // UI for coins in the main game

    public ShopItem[] shopItems;
    public GameObject shopUI;
    public GameObject[] otherUIElements;

    private void Start()
    {
        shopUI.SetActive(false);
        UpdateCoinUI();
    }

    private void Update()
    {
        // Always sync coin UI with CoinManager
        UpdateCoinUI();
    }

    public void OpenShop()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClickSound);
        shopUI.SetActive(true);
        ToggleOtherUI(false);
    }

    public void CloseShop()
    {
        shopUI.SetActive(false);
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonClickSound);
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
        int currentCoins = CoinManager.Instance.currentCoins; 
        if (shopCoinText != null)
        {
            shopCoinText.text = "Coins: " + currentCoins;
        }

        if (inGameCoinText != null)
        {
            inGameCoinText.text = "Coins: " + currentCoins;
        }
    }

    public void TryPurchase()
    {
        int totalCost = 0;

        foreach (ShopItem item in shopItems)
        {
            totalCost += item.GetTotalCost();
        }

        if (CoinManager.Instance.currentCoins >= totalCost)
        {
            CoinManager.Instance.SpendCoins(totalCost); // Deduct coins correctly

            foreach (ShopItem item in shopItems)
            {
                item.ConfirmPurchase();
            }

            Debug.Log("Purchase successful! Remaining coins: " + CoinManager.Instance.currentCoins);
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }
}
