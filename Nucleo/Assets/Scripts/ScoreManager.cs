using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TextMeshProUGUI scoreText;

    private int currentScore = 0;
    private int killStreak = 0;
    private float streakTimer = 0f;
    private float streakTimeout = 3f;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateScoreUI();
    }

    void Update()
    {
        if (streakTimer > 0)
        {
            streakTimer -= Time.deltaTime;
            if (streakTimer <= 0)
            {
                killStreak = 0;
            }
        }
    }

    public void AddScore(int basePoints, bool isWeakness)
    {
        int points = basePoints;

        // Bonus for using correct radiation type
        if (isWeakness)
            points = Mathf.RoundToInt(points * 1.5f);

        // Streak multiplier
        killStreak++;
        streakTimer = streakTimeout;

        if (killStreak >= 10)
            points = Mathf.RoundToInt(points * 2f);
        else if (killStreak >= 5)
            points = Mathf.RoundToInt(points * 1.5f);

        currentScore += points;
        UpdateScoreUI();
    }

    public void AddWaveBonus(int waveNumber)
    {
        int bonus = waveNumber * 100;
        currentScore += bonus;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            string streakText = killStreak >= 5 ? $" | Streak x{killStreak}" : "";
            scoreText.text = "Score: " + currentScore + streakText;
        }
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void SaveScoreAndHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (currentScore > highScore)
            PlayerPrefs.SetInt("HighScore", currentScore);

        PlayerPrefs.SetInt("LastScore", currentScore);
        PlayerPrefs.Save();
    }
}
