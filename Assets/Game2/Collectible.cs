using UnityEngine;

public class Collectible : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.AddPoint();  // Add a point if the player collects the collectible
            Destroy(gameObject);  // Destroy the collectible after it's collected
        }
    }
}
