using UnityEngine;

public class TriangleSpawner : MonoBehaviour
{
    public GameObject trianglePrefab;
    public Transform player; 
    public float initialSpawnInterval = 2f; 
    public float spawnIntervalDecreaseRate = 0.1f; 
    public float minimumSpawnInterval = 0.5f;
    public float spawnRadius = 10f;
    public float triangleSpeed = 1f;

    private Color[] colors = { Color.red, Color.green, Color.blue, Color.magenta };
    private float spawnTimer = 0f;
    private float currentSpawnInterval;

    void Start()
    {
     
        currentSpawnInterval = initialSpawnInterval;
    }

    void Update()
    {
      
        spawnTimer += Time.deltaTime;

      
        if (spawnTimer >= currentSpawnInterval)
        {
            SpawnTriangle();
            spawnTimer = 0f;

           
            if (currentSpawnInterval > minimumSpawnInterval)
            {
                currentSpawnInterval -= spawnIntervalDecreaseRate * Time.deltaTime;
            }
        }
    }

    void SpawnTriangle()
    {
        Vector2 spawnPosition = (Vector2)player.position + Random.insideUnitCircle.normalized * spawnRadius;

        GameObject triangle = Instantiate(trianglePrefab, spawnPosition, Quaternion.identity);

    
        SpriteRenderer triangleRenderer = triangle.GetComponent<SpriteRenderer>();
        if (triangleRenderer != null)
        {
            triangleRenderer.color = colors[Random.Range(0, colors.Length)];
        }

        triangle.AddComponent<MoveTowardsPlayer>().Initialize(player, triangleSpeed);
    }
}
