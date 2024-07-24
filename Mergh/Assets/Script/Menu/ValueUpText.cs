using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ValueUpText : MonoBehaviour
{
    public TMP_Text CountDamage;

    public TMP_Text CountMoney;

    public TMP_Text CountMoneyForBuy;

    public TMP_Text PriceDamage;
    public TMP_Text PricaMoney;

    private void Awake()
    {
        PriceDamage.text = PlayerPrefs.GetInt("PriceForUpgradeDamage").ToString();
        PricaMoney.text = PlayerPrefs.GetInt("PriceForUpgradeMoney").ToString();

        CountDamage.text = PlayerPrefs.GetInt("bulletDamage").ToString();

        CountMoney.text = PlayerPrefs.GetInt("ValueAddCoinForDeadEnemy").ToString();
        CountMoneyForBuy.text = PlayerPrefs.GetInt("Money").ToString();
    }

    public void UpdateTextAll()
    {
        PriceDamage.text = PlayerPrefs.GetInt("PriceForUpgradeDamage").ToString();
        PricaMoney.text = PlayerPrefs.GetInt("PriceForUpgradeMoney").ToString();
        CountDamage.text = PlayerPrefs.GetInt("bulletDamage").ToString();

        CountMoney.text = PlayerPrefs.GetInt("ValueAddCoinForDeadEnemy").ToString();
        CountMoneyForBuy.text = PlayerPrefs.GetInt("Money").ToString();
    }

}
