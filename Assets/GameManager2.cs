using UnityEngine;
using TMPro;

public class GameManager2 : MonoBehaviour
{
    public GameObject endScreen;  // Reference to the end screen UI
    public TextMeshProUGUI enemiesDestroyedText;  // Reference to the UI text for showing enemy count

    private int enemiesDestroyed = 0;  // Counter for enemies destroyed

    private void Start()
    {
        if (endScreen != null)
        {
            endScreen.SetActive(false);  // Hide the end screen initially
        }
        UpdateEnemiesDestroyedText();  // Initialize the score text
    }

    public void ShowEndScreen()
    {
        Time.timeScale = 0f;  // Pause the game
        if (endScreen != null)
        {
            endScreen.SetActive(true);  // Show the game over screen
        }
    }

    public void IncrementEnemiesDestroyed()
    {
        enemiesDestroyed++;
        UpdateEnemiesDestroyedText();  // Update the text whenever an enemy is destroyed
    }

    private void UpdateEnemiesDestroyedText()
    {
        if (enemiesDestroyedText != null)
        {
            enemiesDestroyedText.text = "Enemies Destroyed: " + enemiesDestroyed;
        }
    }
}
