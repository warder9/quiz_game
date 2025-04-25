using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    // UI Elements
    public TextMeshProUGUI finalScoreText;

    void Start()
    {
        // Load the final score from PlayerPrefs and display it on the GameOver screen
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        finalScoreText.text = "Your Final Score: " + finalScore.ToString();
    }
}
