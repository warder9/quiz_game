using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Level3Manager : MonoBehaviour
{
    // UI Elements
    public TextMeshProUGUI scrambledWordText;
    public TMP_InputField userInputField;
    public TextMeshProUGUI feedbackText;
    public GameObject feedbackPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI clueText;  // For displaying the clue

    // Game Data
    private List<WordData> wordDataList = new List<WordData>();  // List of word data with clues
    private int currentWordIndex = 0;
    private int score = 0;

    void Start()
    {
        // Initialize the word data list with words, clues, and their scrambled versions
        wordDataList.Add(new WordData("the eagle", "khabib's nickname"));
        wordDataList.Add(new WordData("the notorious", "conor's nickname"));
        wordDataList.Add(new WordData("the last stylebender", "israle's nickname"));
        wordDataList.Add(new WordData("bones", "jon's nickname"));
        wordDataList.Add(new WordData("rush", "george's nickname"));
        wordDataList.Add(new WordData("the baddest man on the planet", "stipe's nickname"));
        wordDataList.Add(new WordData("the lioness", "amanda's nickname"));
        wordDataList.Add(new WordData("dc", "daniel's nickname"));
        wordDataList.Add(new WordData("stockton's own", "nate's nickname"));
        wordDataList.Add(new WordData("el cucuy", "tony's nickname"));
        // Load the previous score if there is any from previous levels
        score = PlayerPrefs.GetInt("CurrentScore", 0);

        ShowWord();
    }

    void ShowWord()
    {
        // Display the current scrambled word and clue
        WordData currentWordData = wordDataList[currentWordIndex];
        scrambledWordText.text = ScrambleWord(currentWordData.word);  // Scramble the word to show
        clueText.text = "Clue: " + currentWordData.clue;  // Display the clue
    }

    public void CheckAnswer()
    {
        // Get the input from the user
        string userAnswer = userInputField.text.Trim().ToLower();

        // Get the correct word from the current WordData
        string correctAnswer = wordDataList[currentWordIndex].word.ToLower();

        // Check if the answer is correct
        if (string.IsNullOrEmpty(userAnswer))
        {
            feedbackText.text = "Please enter an answer!";
            feedbackText.color = Color.yellow;  // Provide feedback for empty input
        }
        else if (userAnswer == correctAnswer)
        {
            feedbackText.text = "Correct!";
            feedbackText.color = Color.green;
            score++;
        }
        else
        {
            feedbackText.text = "Try again!";
            feedbackText.color = Color.red;
        }

        // Show feedback panel
        feedbackPanel.SetActive(true);
        UpdateScoreText();

        // Save the updated score after each correct answer
        PlayerPrefs.SetInt("CurrentScore", score);

        // Wait a moment before going to the next word
        Invoke(nameof(NextWord), 1.5f);
    }

    void NextWord()
    {
        currentWordIndex++;

        // Check if there are more words
        if (currentWordIndex >= wordDataList.Count)
        {
            // If all words are answered, end the game and show the score
            PlayerPrefs.SetInt("FinalScore", score);  // Save the final score to display on GameOverScreen
            SceneManager.LoadScene("GameOverScene");  // Replace with your Game Over scene
        }
        else
        {
            // Otherwise, show the next word
            ShowWord();
            feedbackPanel.SetActive(false);
            userInputField.text = "";  // Clear the input field for the next question
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }

    string ScrambleWord(string word)
    {
        // Convert the word into a character array
        char[] characters = word.ToCharArray();

        // Shuffle the characters randomly
        System.Random rng = new System.Random();
        int n = characters.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            char value = characters[k];
            characters[k] = characters[n];
            characters[n] = value;
        }

        // Return the scrambled word as a string
        return new string(characters);
    }
}

// WordData class to store words and their corresponding clues
[System.Serializable]
public class WordData
{
    public string word;  // The correct word
    public string clue;  // The clue for the word

    // Constructor to initialize WordData
    public WordData(string word, string clue)
    {
        this.word = word;
        this.clue = clue;
    }
}
