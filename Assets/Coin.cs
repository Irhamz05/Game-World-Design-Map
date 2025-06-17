using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CoinCollector collector = other.GetComponent<CoinCollector>();
            if (collector != null)
            {
                collector.AddCoin(coinValue);
                Destroy(gameObject); // Remove the coin
            }
        }
    }
}
