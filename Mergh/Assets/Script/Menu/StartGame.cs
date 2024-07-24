using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public TMP_Text Moneytext;
    public Wallet _wallet;
    private int _money;
    private void Awake()
    {
        Time.timeScale = 0;
        _money = PlayerPrefs.GetInt("Money");
    }
    private void OnEnable()
    {
        Moneytext.text = _money.ToString();
    }
    public void StartGameBut(GameObject gameObject)
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void ResetAllSave()
    {
        PlayerPrefs.DeleteAll();
    }
}
