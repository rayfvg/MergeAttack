using UnityEngine;
using UnityEngine.UI;

public class RandomObjectSpawner : MonoBehaviour
{
    public GameObject[] objectPrefabs; // ������ �������� ��������
    public Transform spawnPosition; // ������� ������ �������� (������ ������� ������)
   

    public void SpawnRandomObject()
    {
        int randomIndex = Random.Range(0, objectPrefabs.Length);
        Instantiate(objectPrefabs[randomIndex], spawnPosition.position, Quaternion.identity);
    }
}
