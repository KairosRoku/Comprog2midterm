using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color[] colors = { Color.red, Color.green, Color.blue, Color.magenta };
    private int currentColorIndex = 0;

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 2f;
    public float shootInterval = 0.5f;

    public float rotationSpeed = 2f;

    private float shootTimer = 0f;
    private HashSet<GameObject> enemiesInRange = new HashSet<GameObject>();
    private int enemiesDestroyed = 0;

    public TextMeshProUGUI enemiesDestroyedText;
    public GameObject endScreen;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = colors[currentColorIndex];
        UpdateEnemiesDestroyedText();

        if (endScreen != null)
        {
            endScreen.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangeColor();
        }

        SmoothFaceNearestEnemy();

        if (enemiesInRange.Count > 0)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                ShootBullet();
                shootTimer = 0f;
            }
        }
    }

    private void ChangeColor()
    {
        currentColorIndex = (currentColorIndex + 1) % colors.Length;
        spriteRenderer.color = colors[currentColorIndex];
    }

    public void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.GetComponent<SpriteRenderer>().color = spriteRenderer.color;
        bullet.GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
        Destroy(bullet, 5f);
    }

    private void SmoothFaceNearestEnemy()
    {
        GameObject nearestEnemy = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in allEnemies)
        {
            Vector3 directionToTarget = enemy.transform.position - currentPosition;
            float distanceSqrToTarget = directionToTarget.sqrMagnitude;
            if (distanceSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqrToTarget;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            Vector3 direction = nearestEnemy.transform.position - transform.position;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, targetAngle));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player hit by an enemy!");
            ShowEndScreen();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemiesInRange.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(collision.gameObject);
        }
    }

    public void IncrementEnemiesDestroyed()
    {
        enemiesDestroyed++;
        UpdateEnemiesDestroyedText();
    }

    private void UpdateEnemiesDestroyedText()
    {
        if (enemiesDestroyedText != null)
        {
            enemiesDestroyedText.text = "Enemies Destroyed: " + enemiesDestroyed;
        }
    }

    private void ShowEndScreen()
    {
        Debug.Log("Game Over! Showing end screen."); 
        Time.timeScale = 0f;
        if (endScreen != null)
        {
            endScreen.SetActive(true);
        }
        else
        {
            Debug.LogWarning("End screen object is not assigned in the Inspector.");
        }
    }
}
