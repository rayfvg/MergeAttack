using UnityEngine;
using UnityEngine.SceneManagement; // ��� ������������ �����

public class PlayerFortress : MonoBehaviour
{
    public int maxHealth = 100; // ������������ ���������� �������� ��������
    public int currentHealth;  // ������� ���������� ��������

    void Start()
    {
        // ������������� ��������� ���������� ��������
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        // ��������� ���������� �������� �� ����
        currentHealth -= damage;

        // ���������, �� ����������� �� �����
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // ������� ��������� � ���������
        Debug.Log("Player fortress is destroyed. Game Over!");

        // ����� ����� �������� �������������� ������, ��������, �������� ����� ���������

        // ������������� ������� ����� (��� ����� ��������� ������ �����, ��������, ����� ���������)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
