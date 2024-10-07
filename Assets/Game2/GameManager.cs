using UnityEngine;
using TMPro;  // Import TextMeshPro namespace

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public TMP_Text scoreText;   // TextMeshPro text for displaying the score
    public GameObject gameOverPanel;  // UI Panel for Game Over

    // Method to update score
    public void AddPoint()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();  // Update the TMP text
    }

    // Method to trigger Game Over
    public void GameOver()
    {
        gameOverPanel.SetActive(true);  // Display Game Over panel
        Time.timeScale = 0;  // Pause the game
    }
}
