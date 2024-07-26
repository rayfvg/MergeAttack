using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // ��� ������������ �����

public class PlayerFortress : MonoBehaviour
{
    public int maxHealth = 100; // ������������ ���������� �������� ��������
    public int currentHealth;  // ������� ���������� ��������

    public GameObject GameOverMenu;
    public OpenWindow OpenWindows;

    public Slider healthSlider;

    private Renderer objectRenderer;



    void Start()
    {
        // ������������� ��������� ���������� ��������
        currentHealth = maxHealth;
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        objectRenderer = GetComponent<Renderer>();
        UpdateColor();
    }

    public void TakeDamage(int damage)
    {
        // ��������� ���������� �������� �� ����
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // ���������, ��� �������� �� ������ 0 � �� ������ maxHealth
        healthSlider.value = currentHealth;
        UpdateColor();

        // ���������, �� ����������� �� �����
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
        // ������� ��������� � ���������
        
        OpenWindows.OpenWindowInGame(GameOverMenu);
        // ����� ����� �������� �������������� ������, ��������, �������� ����� ���������

        // ������������� ������� ����� (��� ����� ��������� ������ �����, ��������, ����� ���������)
       
    }
}
