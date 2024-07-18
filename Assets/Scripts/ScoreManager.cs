using UnityEngine;
using TMPro;  // Include the TextMesh Pro namespace

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public TextMeshProUGUI scoreText;  // Reference to the TextMeshProUGUI component for score display

    private int score = 0;

    private void Awake()
    {
        UpdateScoreDisplay();
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
        UpdateScoreDisplay();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreDisplay();
    }

    public void EndGame()
    {
        if (scoreText != null)
        {
            scoreText.text = "You Win! Final Score: " + score;
            scoreText.fontSize = 100;  // Increase size or adjust as needed
            scoreText.color = Color.red;  // Highlight color for game over

            // Center text on screen
            RectTransform rectTransform = scoreText.GetComponent<RectTransform>();
            rectTransform.anchorMin = rectTransform.anchorMax = rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.anchoredPosition = Vector2.zero;

            scoreText.alignment = TMPro.TextAlignmentOptions.Center;
            scoreText.enableWordWrapping = false;

            // Optional: Log to the console
            Debug.Log("Game has ended!");
        }
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
            // Position for gameplay score updates
            RectTransform rectTransform = scoreText.GetComponent<RectTransform>();
            rectTransform.anchorMin = rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.pivot = new Vector2(1, 1);
            rectTransform.anchoredPosition = new Vector2(-20, -20);

            scoreText.alignment = TMPro.TextAlignmentOptions.TopRight;
        }
    }
}
