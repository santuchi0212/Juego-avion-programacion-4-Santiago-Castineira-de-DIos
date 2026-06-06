using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Configuración del Generador")]
    public GameObject enemyPrefab;  // El enemigo a generar
    public float spawnRate = 2f;    // Cada cuántos segundos sale un enemigo
    private float nextSpawn = 0f;

    [Header("Posiciones de Aparición")]
    public float minX = -10f; // Límite izquierdo
    public float maxX = 10f;  // Límite derecho
    public float minY = -5f;  // Límite inferior (para que coincida con tu movimiento S)
    public float maxY = 5f;   // Límite superior (para que coincida con tu movimiento W)
    public float distanceZ = 30f; // Qué tan al fondo de la pantalla aparecen

    void Update()
    {
        // Generador automático basado en tiempo
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(randomX, randomY, distanceZ);

        // CAMBIO AQUÍ: Ahora usa "enemyPrefab.transform.rotation" en vez de los números fijos
        Instantiate(enemyPrefab, spawnPosition, enemyPrefab.transform.rotation);
    }
}