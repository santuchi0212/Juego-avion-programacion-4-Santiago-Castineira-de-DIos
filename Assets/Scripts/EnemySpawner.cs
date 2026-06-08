using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Configuración del Generador")]
    public GameObject enemyPrefab;  
    public float spawnRate = 2f;    
    private float nextSpawn = 0f;

    [Header("Posiciones de Aparición")]
    public float minX = -10f; 
    public float maxX = 10f;  
    public float minY = -5f;  
    public float maxY = 5f;   
    public float distanceZ = 30f; 

    void Update()
    {
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

        Instantiate(enemyPrefab, spawnPosition, enemyPrefab.transform.rotation);
    }
}