using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage; // Урон, наносимый пулей

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<DestroerBullet>())
        {
            Destroy(gameObject);
        }
    }
}
