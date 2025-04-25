using UnityEngine;

[System.Serializable]
public class QuestionData
{
    public string question;                  // The question text
    public string[] options = new string[4]; // The 4 multiple choice options
    public int correctOptionIndex;           // Index of the correct option (0-3)
    public Sprite image;                     // Optional image for the question
    public AudioClip audioClip;              // Optional audio clip for the question
}
