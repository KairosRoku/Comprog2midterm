using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    public GameObject starPrefab; 
    public int maxStars = 50;
    public float spawnRadius = 12f; 
    public Vector2 spawnAreaSize = new Vector2(20, 12); 

    private int currentStarCount = 0; 

    void Start()
    {
        for (int i = 0; i < maxStars; i++)
        {
            SpawnStar();
        }
    }

    void SpawnStar()
    {
        Vector2 spawnPosition = new Vector2(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
        );

        GameObject star = Instantiate(starPrefab, spawnPosition, Quaternion.identity);

        star.AddComponent<StarTwinkle>();
    }
}
