using UnityEngine;
using UnityEngine.SceneManagement;

public class load : MonoBehaviour
{
    // Method to load the "entrance" scene
    public void LoadEntranceScene()
    {
        // Load the "entrance" scene
        SceneManager.LoadScene("entrance");
        
        // Play the click sound effect
        AudioManager.Instance.PlaySFX("click");
        
        // Play the theme music and start from the middle
        AudioManager.Instance.musicSource.time = AudioManager.Instance.musicSource.clip.length / 4;
    }

    // Method to load the "options" scene
    public void LoadOptionsScene()
    {
        // Load the "options" scene
        SceneManager.LoadScene("options");
        
        // Play the click sound effect
        AudioManager.Instance.PlaySFX("click");
    }

    // Method to load the "sample" scene
    public void LoadSampleScene()
    {
        // Load the "s" scene (assuming "s" is a placeholder for "SampleScene")
        SceneManager.LoadScene("s");
        
        // Play the click sound effect
        AudioManager.Instance.PlaySFX("click");
        
        // Stop the currently playing music
        AudioManager.Instance.musicSource.Stop();
        
        // Play the sad music and start from the middle
        AudioManager.Instance.PlayMusic("sad");
        AudioManager.Instance.musicSource.time = AudioManager.Instance.musicSource.clip.length / 2;
    }

    // Method to quit the game
    public void QuitGame()
    {
        // Play the click sound effect
        AudioManager.Instance.PlaySFX("click");
        
        // Quit the application
        Application.Quit(); 
    }
}
