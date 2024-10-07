using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameManager gameManager;  // Reference to the GameManager script

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player hits an obstacle
        if (other.CompareTag("Obstacle"))
        {
            // Call the GameOver method from the GameManager
            gameManager.GameOver();
        }

        // Check if the player hits a collectible
        if (other.CompareTag("Collectible"))
        {
            // Call the AddPoint method from the GameManager
            gameManager.AddPoint();
            Destroy(other.gameObject);  // Destroy the collectible after collecting it
        }
    }
}
