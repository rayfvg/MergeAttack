using UnityEngine;
using UnityEngine.SceneManagement; // Для перезагрузки сцены

public class PlayerFortress : MonoBehaviour
{
    public int maxHealth = 100; // Максимальное количество здоровья крепости
    public int currentHealth;  // Текущее количество здоровья

    void Start()
    {
        // Устанавливаем начальное количество здоровья
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        // Уменьшаем количество здоровья на урон
        currentHealth -= damage;

        // Проверяем, не закончились ли жизни
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Выводим сообщение о проигрыше
        Debug.Log("Player fortress is destroyed. Game Over!");

        // Здесь можно добавить дополнительную логику, например, показать экран поражения

        // Перезагружаем текущую сцену (или можно загрузить другую сцену, например, экран проигрыша)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
