using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject bulletPrefab; // ������ ����
    public float bulletSpeed = 1f; // �������� ����
    public float fireRate = 0.5f; // ������� �������� (������� ����� ����������)
    public int bulletDamage = 1; // ���� ����
    public float detectionRange = 100f; // ��������� ����������� �����

    private float nextFireTime = 0f; // ����� �� ���������� ��������

    

    private void Awake()
    {
       

        if (PlayerPrefs.GetInt("bulletDamage") != 0)
            bulletDamage = PlayerPrefs.GetInt("bulletDamage");
    }
    private void Start()
    {

    }
    void Update()
    {
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Fire();
        }
    }

    void Fire()
    {
        // ������� ���� � ������� ����������� ������� � � ��� ��������
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation, transform.parent);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed; // �������� �����

        Bullet bulletComponent = bullet.GetComponent<Bullet>();
        if (bulletComponent != null)
        {
            bulletComponent.damage = bulletDamage;
        }
    }

    public void UpgradeBulletDamage(int value)
    {
        
        int Money = PlayerPrefs.GetInt("Money");
        int PriceForUpgradeDamage = PlayerPrefs.GetInt("PriceForUpgradeDamage");
        if (Money >= PriceForUpgradeDamage)
        {
            Money -= PriceForUpgradeDamage;
            PriceForUpgradeDamage += 30;
            PlayerPrefs.SetInt("PriceForUpgradeDamage", PriceForUpgradeDamage);
            PlayerPrefs.SetInt("Money", Money);
            bulletDamage += value;
            PlayerPrefs.SetInt("bulletDamage", bulletDamage);
        }
    }
}
