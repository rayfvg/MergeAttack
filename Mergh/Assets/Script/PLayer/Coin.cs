using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1; // Значение монетки
    private Wallet wallet;

    private void Start()
    {
        wallet = FindObjectOfType<Wallet>();
    }
    void OnMouseDown()
    {
        // Добавляем значение монетки к общему количеству монет
        wallet.AddCoins(coinValue);

        // Уничтожаем монетку
        Destroy(gameObject);
    }
}
