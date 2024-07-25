using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 5.0f; // �������� �������� ����� ����
    public float maxHealth = 3;  // ������������ ���������� ������
    public Transform attackLine; // �����, �� ���������� ������� ���� �������� ���������

    public float attackInterval = 1.0f; // �������� ����� ������� (� ��������)
    public int ForseAttack;

    public float currentHealth;
    private bool isAttacking = false; // ����, �����������, ��� ���� ����� �����
    private bool isCoroutineRunning = false; // ���� ��� �������� ��������

    public GameObject coinPrefab; // ������ �������
    public float dropChance = 0.25f; // ����������� ��������� �������

    private PlayerFortress PlayerBase;
    private Wallet PlayerWallet;

    public int ValueAddCoinForDeadEnemy = 1;

    public Slider SliderHp;

    

   

    private void Awake()
    {
        if (PlayerPrefs.GetInt("ValueAddCoinForDeadEnemy") != 0)
        ValueAddCoinForDeadEnemy = PlayerPrefs.GetInt("ValueAddCoinForDeadEnemy");
    }
    void Start()
    {
        PlayerWallet = FindObjectOfType<Wallet>();
        PlayerBase = FindObjectOfType<PlayerFortress>();
        // ������������� ��������� ���������� ������
        
        currentHealth = maxHealth;
        SliderHp.maxValue = maxHealth;
        SliderHp.value = currentHealth;

    }

    void Update()
    {
        if (!isAttacking)
        {
            // ������� ����� ���� ������ ���� �� �� �������
            MoveDown();

            // ���������, ������ �� ���� ����� �����
            if (transform.position.y <= attackLine.position.y)
            {
                StartAttacking();
            }
        }
    }

    void MoveDown()
    {
        // ������� ����� ���� �� ��� Y
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        // ��������� ���������� ������ �� ����
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        SliderHp.value = currentHealth;

        // ���������, �� ����������� �� �����
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // ���������� ������ �����
        Destroy(gameObject);
        PlayerWallet.AddCoins(ValueAddCoinForDeadEnemy);
        PlayerWallet.Score += 100;
        PlayerPrefs.SetInt("Score", PlayerWallet.Score);
        PlayerWallet.Money += 1;
        PlayerPrefs.SetInt("Money", PlayerWallet.Money);

        if (coinPrefab != null && Random.value < dropChance)
        {
            Vector2 spawnPosition = (Vector2)transform.position;
            GameObject coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
            Destroy(coin, 3f);
        }

        //Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
        //GameObject coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        //Destroy(coin, 3f);
    }

    void StartAttacking()
    {
        if (!isCoroutineRunning)
        {
            isAttacking = true;
            isCoroutineRunning = true;
            // ��������� �������� �����
            StartCoroutine(AttackCoroutine());
        }
    }

    IEnumerator AttackCoroutine()
    {
        while (isAttacking)
        {
            // ������ ����� (��������, ������ �������� ����� ��� ����� ������ �����)
            // Debug.Log("Enemy attacking!");

            PlayerBase.TakeDamage(ForseAttack);
            // �������� �������� ����� �� ��������� �����
            yield return new WaitForSeconds(attackInterval);

            // ����� ����� �������� �������������� ������ �����, ���� �����
        }

        isCoroutineRunning = false;
    }

    public void SetHealth(float health)
    {
        maxHealth = health;
        
    }

    public void UpgradeAddMoneyForDeadEnemy(int value)
    {
        int PriceForUpgradeMoney = PlayerPrefs.GetInt("PriceForUpgradeMoney");
        int Money = PlayerPrefs.GetInt("Money");
        if (Money >= PriceForUpgradeMoney)
        {
            Money -= PriceForUpgradeMoney;
            PriceForUpgradeMoney += 10;
            PlayerPrefs.SetInt("PriceForUpgradeMoney", PriceForUpgradeMoney);
            PlayerPrefs.SetInt("Money", Money);
            ValueAddCoinForDeadEnemy += value;
            PlayerPrefs.SetInt("ValueAddCoinForDeadEnemy", ValueAddCoinForDeadEnemy);
        }
    }
}
