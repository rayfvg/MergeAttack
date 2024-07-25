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

    

    void Start()
    {
        // ������������� ��������� ���������� ��������
        currentHealth = maxHealth;
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        // ��������� ���������� �������� �� ����
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // ���������, ��� �������� �� ������ 0 � �� ������ maxHealth
        healthSlider.value = currentHealth;

        // ���������, �� ����������� �� �����
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // ������� ��������� � ���������
        
        OpenWindows.OpenWindowInGame(GameOverMenu);
        // ����� ����� �������� �������������� ������, ��������, �������� ����� ���������

        // ������������� ������� ����� (��� ����� ��������� ������ �����, ��������, ����� ���������)
       
    }
}
