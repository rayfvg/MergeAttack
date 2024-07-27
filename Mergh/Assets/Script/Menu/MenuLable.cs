using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuLable : MonoBehaviour
{
   public Wallet wallet;
    public TMP_Text RecordText;
    public TMP_Text MoneyText;

    private void OnEnable()
    {
        RecordText.text = wallet.currentScore.ToString();
        MoneyText.text = wallet.Money.ToString();
    }
}
