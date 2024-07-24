using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1; // �������� �������
    private Wallet wallet;

    private void Start()
    {
        wallet = FindObjectOfType<Wallet>();
    }
    void OnMouseDown()
    {
        // ��������� �������� ������� � ������ ���������� �����
        wallet.AddCoins(coinValue);

        // ���������� �������
        Destroy(gameObject);
    }
}
