using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private Camera cam;
    public bool isDragging = false;
    private bool isLocked = false; // Флаг для блокировки перетаскивания
    public float gridSize = 2.0f; // Размер клеток
    public float screenLimit = 0.5f; // Ограничение на перетаскивание в нижней части экрана (в процентах от высоты экрана)
    public LayerMask gridLayerMask; // Маска слоя для проверки занятости ячейки

    private Color originalColor;
    private Renderer objectRenderer;
    private Vector3 originalPosition; // Исходная позиция объекта


    
    void Start()
    {
        cam = Camera.main;
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
        originalPosition = transform.position;
    }

    void OnMouseDown()
    {
        if (!isLocked && IsInDraggableArea(GetMouseWorldPos()))
        {
            offset = gameObject.transform.position - GetMouseWorldPos();
            isDragging = true;
            SetTransparency(0.5f);
        }
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = GetMouseWorldPos() + offset;
            if (IsInDraggableArea(mousePosition))
            {
                // Ограничиваем перемещение объекта в пределах камеры
                transform.position = ConstrainToCameraBounds(mousePosition);
                if (IsInCenterArea(transform.position))
                {
                    isLocked = true;
                    isDragging = false;
                    SetTransparency(1.0f);
                }
            }
        }
    }

    void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;
            Vector3 snappedPosition = SnapPosition(transform.position);
            if (!IsCellOccupied(snappedPosition))
            {
                transform.position = snappedPosition;
            }
            else
            {
                // Возвращаем объект на исходную позицию, если ячейка занята
                transform.position = originalPosition;
            }
            SetTransparency(1.0f);
        }
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = cam.WorldToScreenPoint(gameObject.transform.position).z;
        return cam.ScreenToWorldPoint(mousePoint);
    }

    Vector3 SnapPosition(Vector3 position)
    {
        position.x = Mathf.Round(position.x / gridSize) * gridSize;
        position.y = Mathf.Round(position.y / gridSize) * gridSize;
        return position;
    }

    bool IsInDraggableArea(Vector3 position)
    {
        float screenHeight = cam.orthographicSize * 2.0f;
        float limitY = cam.transform.position.y - screenHeight / 2.0f + screenHeight * screenLimit;
        return position.y <= limitY;
    }

    bool IsInCenterArea(Vector3 position)
    {
        float screenHeight = cam.orthographicSize * 2.0f;
        float centerY = cam.transform.position.y;
        return position.y >= centerY;
    }

    bool IsCellOccupied(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, gridSize / 2.0f, gridLayerMask);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }

    void SetTransparency(float alpha)
    {
        Color color = originalColor;
        color.a = alpha;
        objectRenderer.material.color = color;
    }

    Vector3 ConstrainToCameraBounds(Vector3 position)
    {
        // Определяем границы камеры
        float cameraHeight = cam.orthographicSize;
        float cameraWidth = cameraHeight * cam.aspect;

        // Ограничиваем положение объекта в пределах границ камеры
        position.x = Mathf.Clamp(position.x, cam.transform.position.x - cameraWidth, cam.transform.position.x + cameraWidth);
        position.y = Mathf.Clamp(position.y, cam.transform.position.y - cameraHeight, cam.transform.position.y + cameraHeight);

        return position;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = cam.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;

            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    if (!isLocked && IsInDraggableArea(touchPosition))
                    {
                        isDragging = true;
                        offset = gameObject.transform.position - touchPosition;
                        SetTransparency(0.5f);
                    }
                }
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                if (IsInDraggableArea(touchPosition + offset))
                {
                    Vector3 newPosition = touchPosition + offset;
                    transform.position = ConstrainToCameraBounds(newPosition);
                    if (IsInCenterArea(transform.position))
                    {
                        isLocked = true;
                        isDragging = false;
                        SetTransparency(1.0f);
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
                Vector3 snappedPosition = SnapPosition(transform.position);
                if (!IsCellOccupied(snappedPosition))
                {
                    transform.position = snappedPosition;
                }
                else
                {
                    // Возвращаем объект на исходную позицию, если ячейка занята
                    transform.position = originalPosition;
                }
                SetTransparency(1.0f);
            }
        }
    }
}
