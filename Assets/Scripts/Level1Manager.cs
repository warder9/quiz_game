using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Level1Manager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text questionText;
    public TMP_Text feedbackText;
    public TMP_Text scoreText;
    public Image questionImage;
    public Button nextButton;
    public AudioSource audioSource;
    public Button[] optionButtons;
    public TMP_Text[] optionButtonTexts; // Assign via inspector

    [Header("Questions")]
    public List<QuestionData> questions = new List<QuestionData>();

    private int currentQuestionIndex = 0;
    private int score = 0;
    private bool answered = false;

    void Start()
    {
        // Load the previous score from PlayerPrefs if there's any
        score = PlayerPrefs.GetInt("CurrentScore", 0);

        feedbackText.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        ShuffleQuestions();
        DisplayQuestion();
        UpdateScoreUI();
    }

    void ShuffleQuestions()
    {
        for (int i = 0; i < questions.Count; i++)
        {
            QuestionData temp = questions[i];
            int randomIndex = Random.Range(i, questions.Count);
            questions[i] = questions[randomIndex];
        }
    }

    void DisplayQuestion()
    {
        answered = false;
        feedbackText.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);

        QuestionData q = questions[currentQuestionIndex];
        questionText.text = q.question;

        // Set options
        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].interactable = true;
            optionButtonTexts[i].text = q.options[i];
            int index = i; // to avoid closure issue
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => CheckAnswer(index));
        }

        // Set image and audio (optional)
        if (questionImage != null)
        {
            questionImage.gameObject.SetActive(q.image != null);
            if (q.image != null)
            {
                questionImage.sprite = q.image;
            }
        }

        if (q.audioClip != null)
        {
            audioSource.clip = q.audioClip;
            audioSource.Play();
        }
    }

    void CheckAnswer(int selectedIndex)
    {
        if (answered) return;

        answered = true;
        QuestionData q = questions[currentQuestionIndex];
        bool isCorrect = selectedIndex == q.correctOptionIndex;

        if (isCorrect)
        {
            score += 10;
            feedbackText.text = "✅ Correct!";
            feedbackText.color = Color.green;
        }
        else
        {
            feedbackText.text = "❌ Try Again!";
            feedbackText.color = Color.red;
        }

        UpdateScoreUI();
        feedbackText.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);

        // Disable buttons after answering
        foreach (var btn in optionButtons)
            btn.interactable = false;
    }

    public void OnNextButtonClicked()
    {
        currentQuestionIndex++;

        if (currentQuestionIndex >= questions.Count)
        {
            // Save the score to PlayerPrefs for use in the next level or GameOver screen
            PlayerPrefs.SetInt("CurrentScore", score);
            PlayerPrefs.SetInt("FinalScore", score);  // Save the final score
            SceneManager.LoadScene("Level2_TrueFalse");  
        }
        else
        {
            DisplayQuestion();
        }
    }

    void UpdateScoreUI()
    {
        scoreText.text = $"Score: {score}";
    }
}
