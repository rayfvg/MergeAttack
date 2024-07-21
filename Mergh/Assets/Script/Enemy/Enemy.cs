using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float speed = 5.0f; // �������� �������� ����� ����
    public int maxHealth = 3;  // ������������ ���������� ������
    public Transform attackLine; // �����, �� ���������� ������� ���� �������� ���������

    public float attackInterval = 1.0f; // �������� ����� ������� (� ��������)
    public int ForseAttack;

    private int currentHealth;
    private bool isAttacking = false; // ����, �����������, ��� ���� ����� �����
    private bool isCoroutineRunning = false; // ���� ��� �������� ��������

    public PlayerFortress PlayerBase;

    void Start()
    {
        // ������������� ��������� ���������� ������
        currentHealth = maxHealth;
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
            Debug.Log("Enemy attacking!");

            PlayerBase.TakeDamage(ForseAttack);
            // �������� �������� ����� �� ��������� �����
            yield return new WaitForSeconds(attackInterval);

            // ����� ����� �������� �������������� ������ �����, ���� �����
        }

        isCoroutineRunning = false;
    }
}
