using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float speed = 5.0f; // Скорость движения врага вниз
    public int maxHealth = 3;  // Максимальное количество жизней
    public Transform attackLine; // Линия, по достижении которой враг начинает атаковать

    public float attackInterval = 1.0f; // Интервал между атаками (в секундах)
    public int ForseAttack;

    private int currentHealth;
    private bool isAttacking = false; // Флаг, указывающий, что враг начал атаку
    private bool isCoroutineRunning = false; // Флаг для контроля корутины

    public PlayerFortress PlayerBase;

    void Start()
    {
        // Устанавливаем начальное количество жизней
        currentHealth = maxHealth;
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
        currentHealth -= damage;

        // Проверяем, не закончились ли жизни
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Уничтожаем объект врага
        Destroy(gameObject);
    }

    void StartAttacking()
    {
        if (!isCoroutineRunning)
        {
            isAttacking = true;
            isCoroutineRunning = true;
            // Запускаем корутину атаки
            StartCoroutine(AttackCoroutine());
        }
    }

    IEnumerator AttackCoroutine()
    {
        while (isAttacking)
        {
            // Логика атаки (например, запуск анимации атаки или вызов метода атаки)
            Debug.Log("Enemy attacking!");

            PlayerBase.TakeDamage(ForseAttack);
            // Подождем заданное время до следующей атаки
            yield return new WaitForSeconds(attackInterval);

            // Здесь можно добавить дополнительную логику атаки, если нужно
        }

        isCoroutineRunning = false;
    }
}
