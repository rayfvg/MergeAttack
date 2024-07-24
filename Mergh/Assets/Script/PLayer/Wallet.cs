using TMPro;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    
    public int CoinCount;
    public int Score;
    public int Money;
    public TMP_Text textCoin;

    public int PriceForUpgradeDamage = 10;
    public int PriceForUpgradeMoney = 10;

    private void Start()
    {
        PriceForUpgradeDamage = PlayerPrefs.GetInt("PriceForUpgradeDamage");
        PriceForUpgradeMoney = PlayerPrefs.GetInt("PriceForUpgradeMoney");
        Money = PlayerPrefs.GetInt("Money");
        textCoin.text = CoinCount.ToString();
    }
    public void AddCoins(int amount)
    {
        CoinCount += amount;
        textCoin.text = CoinCount.ToString();
        
        // ����� ����� �������� UI ��� ��������� ������ �������� ��� ���������� �����
    } 
    public void RemoveCoins(int amount)
    {
        CoinCount -= amount;
        textCoin.text = CoinCount.ToString();
        
        // ����� ����� �������� UI ��� ��������� ������ �������� ��� ���������� �����
    }


}
