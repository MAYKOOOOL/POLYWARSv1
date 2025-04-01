using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    public int currentCoins = 0;
    public TextMeshProUGUI inGameCoinCounterText; // In-game UI coin counter
    public TextMeshProUGUI shopCoinCounterText; // Shop UI coin counter

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keeps this object persistent
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoins(int amount)
    {
        currentCoins += amount;
        UpdateCoinUI();
    }

    public void SpendCoins(int amount)
    {
        currentCoins -= amount;
        if (currentCoins < 0)
        {
            currentCoins = 0; // Prevents negative coin values
        }
        UpdateCoinUI();
    }

    public void UpdateCoinUI()
    {
        if (inGameCoinCounterText != null)
        {
            inGameCoinCounterText.text = ": " + currentCoins;
        }

        if (shopCoinCounterText != null)
        {
            shopCoinCounterText.text = ": " + currentCoins;
        }
    }
}
