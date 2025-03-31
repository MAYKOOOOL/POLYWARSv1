using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    public int currentCoins = 0;
    public TextMeshProUGUI coinCounterText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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

    public void UpdateCoinUI()
    {
        if (coinCounterText != null)
        {
            coinCounterText.text = ": " + currentCoins;
        }
    }
}
