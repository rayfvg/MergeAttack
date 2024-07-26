using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Префаб врага
    public Transform[] spawnPoints; // Точки появления врагов
    public float spawnInterval = 5f; // Интервал между появлениями врагов

    private float enemyHealth = 5f; // Начальное здоровье врагов

    public Animator animator;

    void Start()
    {
        StartCoroutine(SpawnEnemies()); // Запускаем корутину для спавна врагов
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval); // Ждем перед спавном следующего врага
            SpawnEnemy();
            enemyHealth += 2f; // Увеличиваем здоровье для следующего врага
        }
    }

    void SpawnEnemy()
    {

        animator.SetTrigger("Spawn");
        // Выбираем случайную точку спавна
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];

        // Создаем врага в выбранной точке спавна
        
        GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPoint.position, spawnPoint.rotation);
        enemy.GetComponent<Enemy>().SetHealth(enemyHealth);
        



        //// Устанавливаем здоровье врага
        //Enemy healthComponent = enemy.GetComponent<Enemy>();
        //if (healthComponent != null)
        //{
        //    healthComponent.SetHealth(enemyHealth);
        //}
    }
}
