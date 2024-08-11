using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // "entrance" sahnesini yükler
    public void LoadEntranceScene()
    {
        SceneManager.LoadScene("entrance");
    }

    // "options" sahnesini yükler
    public void LoadOptionsScene()
    {
        SceneManager.LoadScene("options");
    }

    public void LoadSampleScene()
    {
        SceneManager.LoadScene("s");
    }


    // Oyunu kapatır
    public void QuitGame()
    {
        Application.Quit(); 
    }
}
