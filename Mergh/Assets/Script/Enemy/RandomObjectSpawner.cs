using UnityEngine;
using UnityEngine.UI;

public class RandomObjectSpawner : MonoBehaviour
{
    public GameObject[] objectPrefabs; // Массив префабов объектов
    public Transform spawnPosition; // Позиция спавна объектов (нижняя угловая ячейка)
   

    public void SpawnRandomObject()
    {
        int randomIndex = Random.Range(0, objectPrefabs.Length);
        Instantiate(objectPrefabs[randomIndex], spawnPosition.position, Quaternion.identity);
    }
}
