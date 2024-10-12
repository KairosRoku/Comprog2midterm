using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            // Check if the bullet and enemy have matching colors
            Color bulletColor = collision.GetComponent<SpriteRenderer>().color;
            Color enemyColor = GetComponent<SpriteRenderer>().color;

            if (ColorsMatch(bulletColor, enemyColor))
            {
                Debug.Log("Bullet hit and destroyed the enemy!");
                DestroyEnemy();
            }
            else
            {
                Debug.Log("Bullet hit but colors did not match!");
            }

            Destroy(collision.gameObject);  // Destroy the bullet regardless
        }
        else if (collision.CompareTag("Player"))
        {
            Debug.Log("Enemy collided with the player!");
            GameManager.Instance.GameOver();
        }
    }

    private bool ColorsMatch(Color c1, Color c2)
    {
        float tolerance = 0.01f;  // Floating-point tolerance
        return Mathf.Abs(c1.r - c2.r) < tolerance &&
               Mathf.Abs(c1.g - c2.g) < tolerance &&
               Mathf.Abs(c1.b - c2.b) < tolerance &&
               Mathf.Abs(c1.a - c2.a) < tolerance;
    }

    public void DestroyEnemy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.IncrementEnemiesDestroyed();
        }
        else
        {
            Debug.LogError("GameManager instance not found!");
        }

        Destroy(gameObject);
    }
}
