using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento y Disparo")]
    public float speed = 10f;
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;
    private float nextFire = 0f;

    [Header("Sistema de Vidas")]
    public int lives = 5;

    [Header("Progresión de Niveles")]
    public int currentScrap = 0;
    public int scrapToLevelUp = 100;
    public int currentLevel = 1;
    public int maxLevel = 5;

    void Start()
    {
        UIManager ui = FindObjectOfType<UIManager>();
        if (ui != null)
        {
            ui.ActualizarBarraNivel(currentScrap, scrapToLevelUp);
            ui.ActualizarVidas(lives);
            ui.ActualizarNivel(currentLevel);
        }
    }

    void Update()
    {
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveH, moveV, 0f);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);

        if (Input.GetMouseButton(0) && Time.time > nextFire)
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
                scriptBala.targetTag = "Enemy";
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        lives -= damageAmount;
        FindObjectOfType<UIManager>().ActualizarVidas(lives); 

        if (lives <= 0)
        {
            lives = 0;
            FindObjectOfType<UIManager>().PerderJuego();
        }
    }

    public void CollectScrap(int amount)
    {
        currentScrap += amount;
        FindObjectOfType<UIManager>().ActualizarBarraNivel(currentScrap, scrapToLevelUp);

        if (currentScrap >= scrapToLevelUp)
        {
            currentScrap = 0;
            FindObjectOfType<UIManager>().ActualizarBarraNivel(currentScrap, scrapToLevelUp);
            SubirDeNivel();
        }
    }

    void SubirDeNivel()
    {
        currentLevel++;
        FindObjectOfType<UIManager>().ActualizarNivel(currentLevel); // Actualiza la UI

        if (currentLevel >= maxLevel)
        {
            FindObjectOfType<UIManager>().GanarJuego();
        }
        else
        {
            FindObjectOfType<UIManager>().MostrarMenuMejoras();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(2); 

            Enemy enemigo = other.GetComponent<Enemy>();
            if (enemigo != null) enemigo.Die();
        }
    }
}