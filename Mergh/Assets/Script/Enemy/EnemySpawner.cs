using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // ������ �����
    public Transform[] spawnPoints; // ����� ��������� ������
    public float spawnInterval = 5f; // �������� ����� ����������� ������

    private float enemyHealth = 5f; // ��������� �������� ������

    public Animator animator;

    void Start()
    {
        StartCoroutine(SpawnEnemies()); // ��������� �������� ��� ������ ������
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval); // ���� ����� ������� ���������� �����
            SpawnEnemy();
            enemyHealth += 2f; // ����������� �������� ��� ���������� �����
        }
    }

    void SpawnEnemy()
    {

        animator.SetTrigger("Spawn");
        // �������� ��������� ����� ������
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];

        // ������� ����� � ��������� ����� ������
        
        GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPoint.position, spawnPoint.rotation);
        enemy.GetComponent<Enemy>().SetHealth(enemyHealth);
        



        //// ������������� �������� �����
        //Enemy healthComponent = enemy.GetComponent<Enemy>();
        //if (healthComponent != null)
        //{
        //    healthComponent.SetHealth(enemyHealth);
        //}
    }
}
