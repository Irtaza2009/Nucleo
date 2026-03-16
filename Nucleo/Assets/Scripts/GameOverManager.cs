using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        int lastScore = PlayerPrefs.GetInt("LastScore", 0);
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (scoreText != null)
            scoreText.text = "Score: " + lastScore;

        if (highScoreText != null)
            highScoreText.text = "High Score: " + highScore;
    }
}
