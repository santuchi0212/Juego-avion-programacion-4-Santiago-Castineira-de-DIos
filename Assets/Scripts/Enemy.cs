using UnityEngine;

public class Enemy : MonoBehaviour

{
    [Header("Estadísticas")]
    public float speed = 5f;
    public int health = 3;

    [Header("Recompensas")]
    public GameObject scrapPrefab; // La chatarra que va a soltar

    [Header("Combate")]
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public float fireRate = 1.5f;
    private float nextFire = 0f;

    void Start()
    {
        // Desincroniza el primer disparo para que no todos los enemigos disparen a la vez
        nextFire = Time.time + Random.Range(0f, fireRate);
    }

    void Update()
    {
        // Al usar Vector3.back y Space.World, el enemigo siempre viajará 
        // desde el fondo del escenario hacia la cámara, sin importar cómo esté rotado.
        transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);

        // Disparo automático basado en tiempo
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
            // Creamos la bala desde la posición del enemigo
            GameObject clonBala = Instantiate(bulletPrefab, transform.position, bulletPrefab.transform.rotation);

            // EL TRUCO: Le programamos a la bala que su objetivo es el Jugador
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
        // Si tiene asignada una chatarra, la crea en la posición exacta donde murió
        if (scrapPrefab != null)
        {
            Instantiate(scrapPrefab, transform.position, scrapPrefab.transform.rotation);
        }

        Destroy(gameObject); // El enemigo desaparece
    }
}