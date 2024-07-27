using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 5.0f; // Скорость движения врага вниз
    public float maxHealth = 3;  // Максимальное количество жизней
    public Transform attackLine; // Линия, по достижении которой враг начинает атаковать

    public float attackInterval = 1.0f; // Интервал между атаками (в секундах)
    public int ForseAttack;

    public float currentHealth;
    private bool isAttacking = false; // Флаг, указывающий, что враг начал атаку
    private bool isCoroutineRunning = false; // Флаг для контроля корутины

    public GameObject coinPrefab; // Префаб монетки
    public float dropChance = 0.25f; // Вероятность выпадения монетки

    private PlayerFortress PlayerBase;
    private Wallet PlayerWallet;

    public int ValueAddCoinForDeadEnemy = 1;

    public Slider SliderHp;

    public Animator AnimatorEnemy;
    public GameObject hitEffectPrefab;



    private void Awake()
    {
        if (PlayerPrefs.GetInt("ValueAddCoinForDeadEnemy") != 0)
            ValueAddCoinForDeadEnemy = PlayerPrefs.GetInt("ValueAddCoinForDeadEnemy");
    }
    void Start()
    {

        PlayerWallet = FindObjectOfType<Wallet>();
        PlayerBase = FindObjectOfType<PlayerFortress>();
        // Устанавливаем начальное количество жизней

        PlayerWallet.Score = PlayerPrefs.GetInt("Score");
        PlayerWallet.Money = PlayerPrefs.GetInt("Money");

        currentHealth = maxHealth;
        SliderHp.maxValue = maxHealth;
        SliderHp.value = currentHealth;

    }

    void Update()
    {
        if (!isAttacking)
        {
            // Двигаем врага вниз только если он не атакует
            MoveDown();

            // Проверяем, достиг ли враг линии атаки
            if (transform.position.y <= attackLine.position.y)
            {
                StartAttacking();
            }
        }
    }

    void MoveDown()
    {
        // Двигаем врага вниз по оси Y
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        // Уменьшаем количество жизней на урон
        Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);

        hitEffectPrefab.GetComponent<ParticleSystem>().Play();

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        SliderHp.value = currentHealth;

        // Проверяем, не закончились ли жизни
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Уничтожаем объект врага
        Destroy(gameObject, 1f);
        AnimatorEnemy.SetTrigger("Die");
        PlayerWallet.AddCoins(ValueAddCoinForDeadEnemy);

       // PlayerWallet.Score += 90;
       // PlayerPrefs.SetInt("Score", PlayerWallet.Score);
        PlayerWallet.currentScore += 90;
        PlayerPrefs.SetInt("currentScore", PlayerWallet.currentScore);

        if (PlayerWallet.currentScore > PlayerWallet.Score)
        {
            PlayerWallet.Score = PlayerWallet.currentScore;
            // Сохраняем новый рекорд в PlayerPrefs
            PlayerPrefs.SetInt("Score", PlayerWallet.Score);
        }


        PlayerWallet.Money += 6;
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
            // Запускаем корутину атаки
            AnimatorEnemy.SetTrigger("Attack");
            StartCoroutine(AttackCoroutine());
        }
    }

    IEnumerator AttackCoroutine()
    {
        while (isAttacking)
        {
            // Логика атаки (например, запуск анимации атаки или вызов метода атаки)
            // Debug.Log("Enemy attacking!");

            PlayerBase.TakeDamage(ForseAttack);
            // Подождем заданное время до следующей атаки
            yield return new WaitForSeconds(attackInterval);

            // Здесь можно добавить дополнительную логику атаки, если нужно
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
            PriceForUpgradeMoney += 30;
            PlayerPrefs.SetInt("PriceForUpgradeMoney", PriceForUpgradeMoney);
            PlayerPrefs.SetInt("Money", Money);
            ValueAddCoinForDeadEnemy += value;
            PlayerPrefs.SetInt("ValueAddCoinForDeadEnemy", ValueAddCoinForDeadEnemy);
        }
    }
}
