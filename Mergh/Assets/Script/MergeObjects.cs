using UnityEngine;

public class MergeObjects : MonoBehaviour
{
    public GameObject nextConnectionLable;
    public int Level;
    public float gridSize = 2.0f; // Размер клеток для размещения нового объекта
    public DragAndDrop DragAndDrop;

  

    private bool hasMerged = false; // Флаг, чтобы предотвратить повторное объединение

    private void Start()
    {
      
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
         // Проверяем, что уже произошло объединение
        if (DragAndDrop.isDragging == true)
        {
            

        // Проверяем, что объект с которым произошло столкновение, имеет компонент MergeObjects
        MergeObjects obj = collision.gameObject.GetComponent<MergeObjects>();
            if (obj != null && obj.Level == Level)
            {
                // Помечаем текущий объект как уже объединенный
                hasMerged = true;

                // Вычисляем среднюю точку между двумя объектами
                Vector3 mergePosition = (transform.position + collision.transform.position) / 2;

                // Определяем ближайшую ячейку для размещения нового объекта
                Vector3 snappedPosition = SnapToGrid(mergePosition);

                // Создаем объект второго уровня в ближайшей ячейке
                // Instantiate(level2Prefab, snappedPosition, Quaternion.identity);

                Instantiate(nextConnectionLable, Vector2.Lerp(transform.position, collision.transform.position, 5f), Quaternion.identity);
                
                // Уничтожаем оба объекта первого уровня, но только если один из них уже объединился
                if (!obj.hasMerged)
                {
                    Destroy(collision.gameObject);
                }
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    }

    Vector3 SnapToGrid(Vector3 position)
    {
        // Округляем позицию до ближайшей сетки
        float x = Mathf.Round(position.x / gridSize) * gridSize;
        float y = Mathf.Round(position.y / gridSize) * gridSize;
        return new Vector3(x, y, position.z);
    }
}




