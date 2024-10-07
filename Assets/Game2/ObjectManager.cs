using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject obstaclePrefab;        // Prefab for obstacles
    public GameObject collectiblePrefab;     // Prefab for collectibles
    public float spawnRate = 2f;             // How often new objects spawn
    public float speed = 5f;                 // Speed of moving objects
    private float nextSpawnTime = 0f;        // Time for next spawn

    public float minY = -4f;                 // Minimum Y position for spawn
    public float maxY = 4f;                  // Maximum Y position for spawn

    void Update()
    {
        // Handle spawning of objects
        if (Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + spawnRate;
            SpawnObject();
        }

        // Move all spawned objects left
        MoveObjects();
    }

    void SpawnObject()
    {
        // Randomize the Y position for spawning (irrelevant axis, but for variety)
        float randomY = Random.Range(minY, maxY);
        Vector2 spawnPosition = new Vector2(8f, randomY);  // Spawn at +8 on X-axis (right)

        // Randomly decide to spawn an obstacle or collectible
        GameObject objectToSpawn = Random.value > 0.5f ? obstaclePrefab : collectiblePrefab;
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }

    void MoveObjects()
    {
        // Find all objects tagged as either "Obstacle" or "Collectible"
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        GameObject[] collectibles = GameObject.FindGameObjectsWithTag("Collectible");

        // Move and destroy obstacles
        foreach (GameObject obstacle in obstacles)
        {
            MoveAndDestroyObject(obstacle);
        }

        // Move and destroy collectibles
        foreach (GameObject collectible in collectibles)
        {
            MoveAndDestroyObject(collectible);
        }
    }

    void MoveAndDestroyObject(GameObject obj)
    {
        // Move the object to the left, strictly on the X-axis
        obj.transform.Translate(Vector2.left * speed * Time.deltaTime);

        // Destroy the object once it moves past the left edge (-8 on the X-axis)
        if (obj.transform.position.x < -8f)
        {
            Destroy(obj);
        }
    }
}