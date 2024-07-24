using UnityEngine;
using UnityEngine.SceneManagement; // Для перезагрузки сцены

public class PlayerFortress : MonoBehaviour
{
    public int maxHealth = 100; // Максимальное количество здоровья крепости
    public int currentHealth;  // Текущее количество здоровья

    public GameObject GameOverMenu;
    public OpenWindow OpenWindows;

    

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
        
        OpenWindows.OpenWindowInGame(GameOverMenu);
        // Здесь можно добавить дополнительную логику, например, показать экран поражения

        // Перезагружаем текущую сцену (или можно загрузить другую сцену, например, экран проигрыша)
       
    }
}
