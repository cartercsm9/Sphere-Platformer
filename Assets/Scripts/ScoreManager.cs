using UnityEngine;
using TMPro;  // Include the TextMesh Pro namespace

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI scoreText;  // Change this to use TextMeshProUGUI

    private int score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Makes the ScoreManager persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int value)
    {
        score += value;
        if (scoreText != null)
            scoreText.text = "Score: " + score;  // Update the TextMesh Pro text
    }
}
