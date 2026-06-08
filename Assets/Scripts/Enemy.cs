using UnityEngine;

public class Enemy : MonoBehaviour

{
    [Header("Estadísticas")]
    public float speed = 5f;
    public int health = 3;

    [Header("Recompensas")]
    public GameObject scrapPrefab; 

    [Header("Combate")]
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public float fireRate = 1.5f;
    private float nextFire = 0f;

    void Start()
    {
    
        nextFire = Time.time + Random.Range(0f, fireRate);
    }

    void Update()
    {

        transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);

        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null)
        {
            GameObject clonBala = Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);

            Projectile scriptBala = clonBala.GetComponent<Projectile>();
            if (scriptBala != null)
            {
                scriptBala.targetTag = "Player";
            }
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (scrapPrefab != null)
        {
            Instantiate(scrapPrefab, transform.position, scrapPrefab.transform.rotation);
        }

        Destroy(gameObject); 
    }
}