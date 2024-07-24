using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // ������ �����
    public Transform[] spawnPoints; // ����� ��������� ������
    public float spawnInterval = 5f; // �������� ����� ����������� ������

    private float enemyHealth = 5f; // ��������� �������� ������

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
            enemyHealth += 1f; // ����������� �������� ��� ���������� �����
        }
    }

    void SpawnEnemy()
    {
        // �������� ��������� ����� ������
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];

        // ������� ����� � ��������� ����� ������
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        enemy.GetComponent<Enemy>().SetHealth(enemyHealth);
        



        //// ������������� �������� �����
        //Enemy healthComponent = enemy.GetComponent<Enemy>();
        //if (healthComponent != null)
        //{
        //    healthComponent.SetHealth(enemyHealth);
        //}
    }
}
