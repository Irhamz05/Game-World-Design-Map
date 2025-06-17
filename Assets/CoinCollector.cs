using UnityEngine;
using UnityEngine.UI;

public class CoinCollector : MonoBehaviour
{
    public int coinCount = 0;
    public Text coinText; // Assign in Inspector

    public void AddCoin(int amount)
    {
        coinCount += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + coinCount;
        }
    }
}

