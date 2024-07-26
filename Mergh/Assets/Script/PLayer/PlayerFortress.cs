using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Для перезагрузки сцены

public class PlayerFortress : MonoBehaviour
{
    public int maxHealth = 100; // Максимальное количество здоровья крепости
    public int currentHealth;  // Текущее количество здоровья

    public GameObject GameOverMenu;
    public OpenWindow OpenWindows;

    public Slider healthSlider;

    private Renderer objectRenderer;



    void Start()
    {
        // Устанавливаем начальное количество здоровья
        currentHealth = maxHealth;
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        objectRenderer = GetComponent<Renderer>();
        UpdateColor();
    }

    public void TakeDamage(int damage)
    {
        // Уменьшаем количество здоровья на урон
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Убедитесь, что здоровье не меньше 0 и не больше maxHealth
        healthSlider.value = currentHealth;
        UpdateColor();

        // Проверяем, не закончились ли жизни
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateColor()
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        Color currentColor = Color.Lerp(Color.red, Color.white, healthPercentage);
        objectRenderer.material.color = currentColor;
    }
    void Die()
    {
        // Выводим сообщение о проигрыше
        
        OpenWindows.OpenWindowInGame(GameOverMenu);
        // Здесь можно добавить дополнительную логику, например, показать экран поражения

        // Перезагружаем текущую сцену (или можно загрузить другую сцену, например, экран проигрыша)
       
    }
}
