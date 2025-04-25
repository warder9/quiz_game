using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    // Reference to the buttons (assign in the inspector)
    public Button startButton;
    public Button levelSelectionButton;
    public Button exitButton;

    void Start()
    {
        // Add listeners to the buttons
        startButton.onClick.AddListener(StartGame);
        
        exitButton.onClick.AddListener(ExitGame);
    }

    // Start the game by loading the first level
    public void StartGame()
    {
        SceneManager.LoadScene("Level1_MCQ");  // Replace with your first level scene name
    }

    // Go to level selection menu
    void GoToLevelSelection()
    {
        SceneManager.LoadScene("LevelSelection");  // Replace with your level selection scene name
    }

    // Exit the game
    public void ExitGame()
    {
        // If running in the editor, just stop playing
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();  // Exits the game in a built application
#endif
    }
}
