using UnityEngine;

public class MergeObjects : MonoBehaviour
{
    public GameObject nextConnectionLable;
    public int Level;
    public float gridSize = 2.0f; // ������ ������ ��� ���������� ������ �������
    public DragAndDrop DragAndDrop;

  

    private bool hasMerged = false; // ����, ����� ������������� ��������� �����������

    private void Start()
    {
      
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
         // ���������, ��� ��� ��������� �����������
        if (DragAndDrop.isDragging == true)
        {
            

        // ���������, ��� ������ � ������� ��������� ������������, ����� ��������� MergeObjects
        MergeObjects obj = collision.gameObject.GetComponent<MergeObjects>();
            if (obj != null && obj.Level == Level)
            {
                // �������� ������� ������ ��� ��� ������������
                hasMerged = true;

                // ��������� ������� ����� ����� ����� ���������
                Vector3 mergePosition = (transform.position + collision.transform.position) / 2;

                // ���������� ��������� ������ ��� ���������� ������ �������
                Vector3 snappedPosition = SnapToGrid(mergePosition);

                // ������� ������ ������� ������ � ��������� ������
                // Instantiate(level2Prefab, snappedPosition, Quaternion.identity);

                Instantiate(nextConnectionLable, Vector2.Lerp(transform.position, collision.transform.position, 5f), Quaternion.identity);
                
                // ���������� ��� ������� ������� ������, �� ������ ���� ���� �� ��� ��� �����������
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
        // ��������� ������� �� ��������� �����
        float x = Mathf.Round(position.x / gridSize) * gridSize;
        float y = Mathf.Round(position.y / gridSize) * gridSize;
        return new Vector3(x, y, position.z);
    }
}




