using UnityEngine;

public class SpawnEnemigos : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array de prefabs de enemigos
    public float initialSpawnInterval = 3f; // Intervalo inicial de generación de enemigos
    public float spawnIntervalDecreaseRate = 0.1f; // Tasa de disminución del intervalo de generación
    public float minSpawnInterval = 0.5f; // Intervalo mínimo de generación de enemigos
    public float spawnRadius = 10f; // Radio de generación alrededor del jugador

    private float timeSinceLastSpawn = 0f;
    private float currentSpawnInterval;
    private Transform playerTransform;

    private void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= currentSpawnInterval)
        {
            SpawnEnemy();
            timeSinceLastSpawn = 0f;

            currentSpawnInterval -= spawnIntervalDecreaseRate;
            currentSpawnInterval = Mathf.Max(currentSpawnInterval, minSpawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        // Generar un enemigo aleatorio
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyPrefab = enemyPrefabs[randomIndex];

        // Calcular una posición dentro del rango alrededor del jugador
        Vector2 randomCirclePoint = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPosition = playerTransform.position + new Vector3(randomCirclePoint.x, 0f, randomCirclePoint.y);

        // Calcular la rotación para que el enemigo mire hacia el jugador
        Quaternion spawnRotation = Quaternion.LookRotation(playerTransform.position - spawnPosition, Vector3.up);

        // Instanciar el enemigo en la posición y rotación calculadas
        Instantiate(enemyPrefab, spawnPosition, spawnRotation);
    }
}


