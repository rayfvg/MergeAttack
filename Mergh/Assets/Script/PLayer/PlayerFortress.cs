using UnityEngine;
using UnityEngine.SceneManagement; // ��� ������������ �����

public class PlayerFortress : MonoBehaviour
{
    public int maxHealth = 100; // ������������ ���������� �������� ��������
    public int currentHealth;  // ������� ���������� ��������

    public GameObject GameOverMenu;
    public OpenWindow OpenWindows;

    

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
        
        OpenWindows.OpenWindowInGame(GameOverMenu);
        // ����� ����� �������� �������������� ������, ��������, �������� ����� ���������

        // ������������� ������� ����� (��� ����� ��������� ������ �����, ��������, ����� ���������)
       
    }
}
