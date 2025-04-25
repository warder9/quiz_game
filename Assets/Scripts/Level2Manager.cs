using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Level2Manager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI questionText;
    public Image questionImage;
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI scoreText;
    public GameObject feedbackPanel;

    [Header("Audio")]
    public AudioSource audioSource;

    [Header("Buttons")]
    public Button trueButton;
    public Button falseButton;

    [Header("Questions")]
    public List<TFQuestionData> questions = new List<TFQuestionData>();

    private int currentQuestionIndex = 0;
    private int score = 0;

    private void Start()
    {
        feedbackPanel.SetActive(false);
        ShowQuestion();
        UpdateScoreText();
    }

    void ShowQuestion()
    {
        TFQuestionData q = questions[currentQuestionIndex];

        questionText.text = q.question;

        // Handle optional image
        if (questionImage != null)
        {
            questionImage.gameObject.SetActive(q.image != null);
            if (q.image != null)
                questionImage.sprite = q.image;
        }

        // Handle optional audio
        if (audioSource != null)
        {
            if (q.audioClip != null)
            {
                audioSource.clip = q.audioClip;
                audioSource.Play();
            }
            else
            {
                audioSource.Stop();
            }
        }

        feedbackPanel.SetActive(false);
    }

    public void OnAnswerClicked(bool answer)
    {
        TFQuestionData q = questions[currentQuestionIndex];

        if (answer == q.correctAnswer)
        {
            feedbackText.text = "Correct!";
            score++;
        }
        else
        {
            feedbackText.text = "Not correct";
        }

        feedbackPanel.SetActive(true);
        UpdateScoreText();

        Invoke(nameof(NextQuestion), 1.5f);
    }

    void NextQuestion()
    {
        currentQuestionIndex++;
        if (currentQuestionIndex >= questions.Count)
        {
            PlayerPrefs.SetInt("QuizScore", score);
            SceneManager.LoadScene("Level3_WordGame");
        }
        else
        {
            ShowQuestion();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
